using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtilleryHelper
{
    enum SQF_READ_ERROR
    {
        EMPTY_FILE_PATH,
        FILE_NOT_AVAILABLE,
        PARSING_ERROR,
        SUCCESS
    }

    enum SQF_TYPE_VAR
    {
        DOUBLE,
        STRING,
        ARRAY,
        MAP
    }

    abstract class SQFVar
    {
        private SQF_TYPE_VAR varType = SQF_TYPE_VAR.DOUBLE;

        public SQFVar(SQF_TYPE_VAR varType, Object var)
        {
            this.varType = varType;
            SetVar(var);
        }

        public abstract Object GetVar();

        protected abstract void SetVar(Object var);


        public SQF_TYPE_VAR GetType()
        {
            return varType;
        }
    }

    class SQFVarDouble : SQFVar
    {
        private double varDouble;

        public SQFVarDouble(SQF_TYPE_VAR varType, double iVar) : base(varType, iVar)
        {}

        public override Object GetVar()
        {
            return varDouble;
        }

        protected override void SetVar(Object var)
        {
            varDouble = (double)var;
        }
    }

    class SQFVarStr : SQFVar
    {
        private String varStr;

        public SQFVarStr(SQF_TYPE_VAR varType, String strVar) : base(varType, strVar)
        {}

        public override Object GetVar()
        {
            return varStr;
        }

        protected override void SetVar(object var)
        {
            varStr = (String)var;
        }
    }

    class SQFVarList : SQFVar
    {
        private List<SQFVar> varArr;

        public SQFVarList(SQF_TYPE_VAR varType, List<SQFVar> arrVar) : base(varType, arrVar)
        {}

        public override Object GetVar()
        {
            return varArr;
        }

        protected override void SetVar(object var)
        {
            varArr = (List<SQFVar>)var;
        }

        public int GetLength()
        {
            return varArr.Count;
        }

        public void AddObject(SQFVar obj)
        {
            varArr.Add(obj);
        }

        public SQFVar GetObject(int n)
        {
            return varArr[n];
        }

        public SQFVar GetLastObject()
        {
            return varArr.Last();
        }

        public void Clear()
        {
            varArr.Clear();
        }

        public void DelObject(SQFVar obj)
        {
            varArr.Remove(obj);
        }
    }

    class SQFVarMap : SQFVar
    {
        private Dictionary<String, SQFVar> varMap;

        public SQFVarMap(SQF_TYPE_VAR varType, Dictionary<String, SQFVar> arrVar) : base(varType, arrVar)
        {
            arrVar = new Dictionary<String, SQFVar>();
        }

        public override Object GetVar()
        {
            return varMap;
        }

        protected override void SetVar(object var)
        {
            varMap = (Dictionary<String, SQFVar>)var;
        }

        public Object GetObject(String key)
        {
            return varMap[key];
        }

        public void SetObject(String key, SQFVar obj)
        {
            varMap[key] = obj;
        }

        public int GetLength() { return varMap.Count; }

        public void AddObject(String str, SQFVar obj)
        {
            varMap.Add(str, obj);
        }

        public void Clear()
        {
            varMap.Clear();
        }

        public void DelObject(String key)
        {
            varMap.Remove(key);
        }

    }


    internal class SQFReader
    {
        private SQF_READ_ERROR error = SQF_READ_ERROR.SUCCESS;
        List<GunBase> guns= new List<GunBase>();
        
        public SQFReader(String FilePath)
        {
            if (FilePath.Length <= 0)
                return;

            String buffer = "";

            try
            {
                StreamReader sr = new StreamReader(FilePath);
                buffer = sr.ReadToEnd();
                sr.Close();
            } catch (Exception ex)
            {
                error = SQF_READ_ERROR.FILE_NOT_AVAILABLE;
                return;
            }

            // Читаем сначала по одному байту в блоки
            List<String> blocks = ParseFile(buffer);

            // Теперь эти блоки парсим в массив снарядов
            Dictionary<String, SQFVarList> projectiles = ParseBlocks(blocks);

            if (projectiles == null)
                return;

            // Десеализуем массив снарядов в обьекты ProjectileBase
            ProjectileDeserialisation(projectiles);

            return;
        }

        private void ProjectileDeserialisation(Dictionary<String, SQFVarList> projectiles)
        {
            foreach(var projectile in projectiles)
            {

            }
        }

        private List<String> ParseFile(String str)
        {
            List<String> blocks = new List<String>();
            char[] chars = str.ToCharArray();
            string CurrentBlock = "";
            foreach (var ch in chars)
            {
                if (ch == '\n' || ch == ' ' || ch == '\t' || ch == 0x00 ||
                    ch == ';' || ch == ',' || ch == '.' || ch == '\'' ||
                    ch == '[' || ch == ']' || ch == '(' || ch == ')' || ch == '"')
                    goto AddBlock;

                CurrentBlock += ch;

                if (CurrentBlock == "//")
                    goto AddBlock;

                if (CurrentBlock == "||")
                    goto AddBlock;

                continue;


            AddBlock:
                if(CurrentBlock != "")
                    blocks.Add(CurrentBlock);
                CurrentBlock = "";
                if (ch == '\n' || ch == ';' || ch == ',' || ch == '.' || ch == '\'' ||
                    ch == '"' || ch == '[' || ch == ']')
                    blocks.Add(ch + "");
            }

            return blocks;
        }

        private Dictionary<String, SQFVarList> ParseBlocks(List<String> blocks)
        {
            // Для скипа комментов
            bool isWaitComment = false;

            // Для чтения одного и более имен Projectile
            bool isProjectileNameStrart = false;
            String[] ProjectileName = new string[3] {"", "", ""};
            int iCurrentName = 0;
            bool isWaitNextName = false;
            
            // Для разбора массивов
            int iDepthLevel = 0;
            
            // Для считывания элементов массивов
            bool isElementValueStart = false;
            String strCurrentValue = "";
            SQFVarList tmp = null;

            // Итоговый массив с данными по снарядам
            // Его нужно будет десереализовать
            Dictionary<String, SQFVarList> sqf_vars = new Dictionary<String, SQFVarList>();

            string last_projectile_name = "";

            // Надо что-то вроде map<string, class(string,int,map)>
            foreach(var block in blocks)
            {
                // Скипаем все комменты
                if (block == "//")
                {
                    isWaitComment = true;
                    continue;
                }
                if (block == "\n" && isWaitComment)
                {
                    isWaitComment = false;
                    continue;
                }
                if (isWaitComment)
                    continue;

                // Разбор блока case (ProjectileName)
                if(block == "\"" && iDepthLevel == 0)
                {
                    if(!isProjectileNameStrart)
                    {
                        isProjectileNameStrart = true;
                    }
                    else
                    {
                        isProjectileNameStrart = false;

                        sqf_vars.Add(ProjectileName[iCurrentName],
                            (SQFVarList)CreateVar(SQF_TYPE_VAR.ARRAY, null));

                        if (isWaitNextName)
                        {
                            iCurrentName++;
                            isWaitNextName = false;
                        }
                    }
                    continue;
                }

                // На случай, если case для нескольких блоков (не более 3)
                if (block == "||")
                {
                    isWaitNextName = true;
                    continue;
                }

                if (isProjectileNameStrart)
                {
                    ProjectileName[iCurrentName] += block;
                    continue;
                }

                // Разбор блока [] для всего массива projectile.
                // Лучше заполнять сразу как массив с названием ProjectileName,
                // у которого просто будем учитывать вложенность для всех [ и ].
                // Таким образом должен уйти от лишнего кода.
                if(block == "[")
                {

                    iDepthLevel++;
                    tmp = (SQFVarList)CreateVar(SQF_TYPE_VAR.ARRAY, null);
                }
                
                // Пока-что просто снижаем уровень вложенности.
                // Возможно понадобиться вспоминать имя пред. массива.
                if(block == "]")
                {
                    iDepthLevel--;

                    // Что-то пошло не так, надо смотреть сам файл.
                    if (iDepthLevel < 0)
                    {
                        error = SQF_READ_ERROR.PARSING_ERROR;
                        return null;
                    }

                    if (iDepthLevel == 0)
                    {
                        isWaitNextName = false;
                        iCurrentName = 0;
                        tmp = null;
                        if (ProjectileName[0] != "")
                            last_projectile_name = ProjectileName[0];
                        for(int i = 0; i < ProjectileName.Length; i++)
                            ProjectileName[i] = "";
                    }

                    if(iDepthLevel > 1)
                    {
                        for(int i = 0; i < iCurrentName; i++)
                        {
                            sqf_vars[ProjectileName[i]].AddObject(tmp);
                        }

                        tmp = null;
                    }
                }

                // Заполнение массива projectile

                // Сначала определяем начало значения элемента
                if (block == "\"" && iDepthLevel > 0)
                {
                    if (!isElementValueStart)
                        isElementValueStart = true;
                    else
                    {
                        isElementValueStart = false;

                        double value = 0;
                        bool isString = false;

                        if (strCurrentValue == "0.0" || strCurrentValue == "-0.0")
                            strCurrentValue = "0";

                        strCurrentValue = strCurrentValue.Replace('.', ',');

                        if (strCurrentValue == "---" || strCurrentValue == "М---" || strCurrentValue == "---М" ||
                            strCurrentValue == "M---" || strCurrentValue == "---M")
                            isString = true;
                        else
                        {
                            try
                            {
                                value = Double.Parse(strCurrentValue);
                            }
                            catch (Exception e)
                            {
                                error = SQF_READ_ERROR.PARSING_ERROR;
                                return null;
                            }
                        }

                        // Дальше запись в массив
                        if (iDepthLevel > 1)
                            tmp.AddObject(CreateVar(SQF_TYPE_VAR.DOUBLE, value));
                        else
                            // Добавляем для всех текущих имен
                            for (int i = 0; i < iCurrentName; i++)
                            {
                                if(isString)
                                    sqf_vars[ProjectileName[i]].AddObject(CreateVar(SQF_TYPE_VAR.STRING, strCurrentValue));
                                else
                                    sqf_vars[ProjectileName[i]].AddObject(CreateVar(SQF_TYPE_VAR.DOUBLE, value));
                            }

                        strCurrentValue = "";
                    }
                    continue;
                }

                if(isElementValueStart)
                {

                    strCurrentValue += block;
                    if (block == "")
                        return null;
                }
            }

            return sqf_vars;
        }

        // obj may be null
        private SQFVar CreateVar(SQF_TYPE_VAR type, Object obj)
        {
            SQFVar var;
            switch(type)
            {
                case SQF_TYPE_VAR.DOUBLE:
                    {
                        if(obj == null)
                            var = new SQFVarDouble(type, 0);
                        else
                            var = new SQFVarDouble(type, (double)obj);
                        break;
                    }
                case SQF_TYPE_VAR.STRING:
                    {
                        if (obj == null)
                            var = new SQFVarStr(type, "");
                        else
                            var = new SQFVarStr(type, (string)obj);
                        break;
                    }
                    case SQF_TYPE_VAR.ARRAY:
                    {
                        if(obj == null)
                            var = new SQFVarList(type, new List<SQFVar>());
                        else
                            var = new SQFVarList(type, (List<SQFVar>)obj);
                        break;
                    }
                    case SQF_TYPE_VAR.MAP:
                    {
                        if (obj != null)
                            var = new SQFVarMap(type, new Dictionary<String, SQFVar>());
                        else
                            var = new SQFVarMap(type, (Dictionary<String, SQFVar>)obj);
                        break;
                    }
                default:
                    var = null;
                    break;
            }

            return var;
        }

        public SQF_READ_ERROR GetLastError()
        {
            return error;
        }
    }
}
