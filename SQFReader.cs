using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Diagnostics.Contracts;


/*
 * 
 * Тут треш и угар, просто не трогаем ничего.
 * 
 * P.S.:
 *  Начал переделывать паринг блочков,
 *  дабы можно было просто загрузить все в файлик и всо.
 * 
 * P.P.S:
 *  Переделал. Теперь нормально все сам парсит и не падает
 *  от каждого коммента или рандомного символа.
 * 
 */

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
        [JsonInclude]
        public SQF_TYPE_VAR varType = SQF_TYPE_VAR.DOUBLE;

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
        [JsonInclude]
        public double varDouble;

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
        [JsonInclude]
        public String varStr;

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
        [JsonInclude]
        public List<SQFVar> varArr;

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
        [JsonInclude]
        public Dictionary<String, SQFVar> varMap;

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


            /*
             * 
             * Когда нибудь я доделаю это:
             *      Делаем json-полиморфные классы, которые сами будут строчить свой json-блок
             *      А потом весь Dictionary projectiles преобразовывается в json
             *      и перекидывается в файл .json рядом с .exe, при этом удаляя
             *      сам sqf файл. Получается что sqf файл будет использоваться редко
             *      и только для обновления json файлика. Тогда должна подняться скорость
             *      и вот это вот все, но пока хилимся-живем
             *
             *

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
             
            string json = "{";

            foreach (String block in projectiles.Keys)
            {
                json += JsonSerializer.Serialize(block);
                json += JsonSerializer.Serialize(projectiles[block], options);
            }

            json+= "}";
            */


            if (projectiles == null)
                return;

            // Десеализуем массив снарядов в обьекты ProjectileBase
            ProjectileDeserialisation(projectiles);

            return;
        }

        private string SetProjectileOF(int num)
        {
            string result = "";

            result += "ОФ, ";
            result += num;
            result += "-й";

            return result;
        }

        private string SetProjectileOF(string num)
        {
            string result = "";

            result += "ОФ, ";
            result += num;

            return result;
        }

        private void ProjectileDeserialisation(Dictionary<String, SQFVarList> projectiles)
        {

            foreach (var projectile in projectiles)
            {
                // Имя СНАРЯДА
                string ProjectileName = projectile.Key;

                if (ProjectileName == "")
                {
                    error = SQF_READ_ERROR.PARSING_ERROR;
                }

                ProjectileBase proj = new ProjectileBase();
                String GunName = "";
                string RealNameProjectile = "";
                // false - USSR; true - NATO
                bool isScaleNATO = false;
                // Для расчета среднего времени полета снаряда
                double SumTime = 0;

                // Для проверки строки M--- ----...
                bool isArcNext = false;

                // m119
                if (ProjectileName.Contains("bn_105mm"))
                {
                    GunName = "M119";
                    isScaleNATO = true;


                    if (ProjectileName.Contains("_OF_"))
                    {
                        RealNameProjectile += SetProjectileOF(Int32.Parse
                            (ProjectileName.Substring(ProjectileName.Length - 1)));
                    }
                    else
                    {
                        RealNameProjectile += "Кум,спец.";
                    }
                }
                else if (ProjectileName.Contains("bn_122mm"))
                {
                    GunName = "Д-30";


                    if (ProjectileName.Contains("_OF_"))
                    {
                        int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));
                        if (num == 6)
                            RealNameProjectile += SetProjectileOF("уменьш.");
                        else if (num == 7)
                            RealNameProjectile += SetProjectileOF("полный");
                        else
                            RealNameProjectile += SetProjectileOF(num - 6);
                    }
                    else
                    {
                        RealNameProjectile += "Кум,спец.";
                    }
                }
                else if(ProjectileName.Contains("bn_82mm") && !ProjectileName.Contains("_cas"))
                {
                    GunName = "2Б14";

                    int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));
                    
                    if(num == 4)
                    {
                        RealNameProjectile += SetProjectileOF("дальнобойный");
                    }
                    else if (num == 0)
                    {
                        RealNameProjectile += SetProjectileOF("основной");
                    }
                    else
                    {
                        RealNameProjectile += SetProjectileOF(num);
                    }
                }
                else if(ProjectileName.Contains("bn_82mm") && ProjectileName.Contains("_cas"))
                {
                    GunName = "2Б9";

                    if(ProjectileName.Contains("bn_82mm"))
                    {
                        RealNameProjectile += SetProjectileOF(1);
                    }
                    else
                    {
                        RealNameProjectile += SetProjectileOF("дальнобойный");
                    }
                }
                else if(ProjectileName.Contains("BN_rhs_mag_og9v"))
                {
                    GunName = "СПГ-9";

                    RealNameProjectile += "ОГ-9";
                }
                else if(ProjectileName.Contains("tu_mag_type63"))
                {
                    GunName = "Type 63";

                    RealNameProjectile += "Type 63-2 HE";
                }
                else if(ProjectileName.Contains("bn_120mm"))
                {
                    GunName = "120mm";

                    int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));

                    if (num == 6)
                        RealNameProjectile += SetProjectileOF("дальнобойный");
                    else
                        RealNameProjectile += SetProjectileOF(num + 1);
                }
                else if(ProjectileName.Contains("bn_60mm"))
                {
                    GunName = "M224";
                    isScaleNATO = true;

                    int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));

                    RealNameProjectile += SetProjectileOF(num);
                }
                else if(ProjectileName.Contains("rhs_mag_HE_2a33"))
                {
                    GunName = "2С3";

                    int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));

                    if (num == 7)
                        RealNameProjectile += SetProjectileOF("полный");
                    else
                        RealNameProjectile += SetProjectileOF(7 - num);
                }
                else if(ProjectileName.Contains("rhs_mag_40Rnd_122mm_rocketsClose"))
                {
                    GunName = "БМ-21";

                    if (ProjectileName.Contains("_TM"))
                        RealNameProjectile += "РС(Тормоз М)";
                    else if (ProjectileName.Contains("_TB"))
                        RealNameProjectile += "РС(Тормоз Б)";
                    else
                        RealNameProjectile += "РС";
                }
                else if(ProjectileName.Contains("RHS_mag_VOG30_30"))
                {
                    GunName = "АГС-30";

                    RealNameProjectile += "ВОГ-30";
                }
                else if(ProjectileName.Contains("bn_81mm"))
                {
                    GunName = "M252";
                    isScaleNATO = true;

                    int num = Int32.Parse(ProjectileName.Substring(ProjectileName.Length - 1));

                    RealNameProjectile += SetProjectileOF(num);
                }
                else
                {
                    GunName = "Unknown";
                    isScaleNATO = false;
                    RealNameProjectile += "Unknown " + ProjectileName;
                }

                if (GunList.GetGun(GunName) == null)
                    GunList.GetInstance().Add(GunName, new GunBase());

                if (proj.Name == null)
                    proj.Name = RealNameProjectile;

                if (GunList.GetGun(GunName).Name == null)
                    GunList.GetGun(GunName).Name = GunName;

                GunList.GetGun(GunName).isScaleNATO = isScaleNATO;


                for(int i = 0; i < projectile.Value.GetLength(); i++)
                {
                    SQFVarList TableStroke = (SQFVarList) projectile.Value.GetObject(i);
                    if (TableStroke.GetLength() < 15)
                    {
                        error = SQF_READ_ERROR.PARSING_ERROR;
                        return;
                    }

                    // Проверка на строку с M--- ---- ...
                    // Если такая есть - выставляем возможность стрелять навесом
                    if(TableStroke.GetObject(0).varType == SQF_TYPE_VAR.STRING)
                    {
                        isArcNext = true;
                        proj.CanArc = true;
                        continue;
                    }
                    double range = (double)TableStroke.GetObject(0).GetVar();

                    if (range < GunList.GetGun(GunName).MinRange || GunList.GetGun(GunName).MinRange == 0)
                        GunList.GetGun(GunName).MinRange = range;
                    if (range > GunList.GetGun(GunName).MaxRange)
                        GunList.GetGun(GunName).MaxRange = range;

                    if (!isArcNext)
                    {
                        if (range < proj.MinRange)
                            proj.MinRange = range;
                        if (range > proj.MaxRange)
                            proj.MaxRange = range;
                    } else
                    {
                        if (range < proj.MinRangeArc)
                            proj.MinRangeArc = range;
                        if (range > proj.MaxRangeArc)
                            proj.MaxRangeArc = range;
                    }

                    // Создаем и сразу заполняем Дальность (м) в item
                    TableItem item = new TableItem(range);


                    /*
                     * 
                     * По хорошему тут надо начать заполнять Min/Max Range,
                     * Min/Max RangeArc и еще какую-то быструю инфу,
                     * которую мы можем понять из этих данных, но пока так.
                     * 
                     */

                    // Заполняем значения для Прицел (ед) с такой дальностью (до 7 значений, зависят от погоды)
                    if (TableStroke.GetObject(1).GetType() == SQF_TYPE_VAR.ARRAY)
                    {
                        if(((SQFVarList)TableStroke.GetObject(1)).GetLength() < 7)
                        {
                            error = SQF_READ_ERROR.PARSING_ERROR;
                            return;
                        }

                        for(int j = 0; j < 7; j++)
                        {
                            item.ScopeValue[j] = (double)((SQFVarList)TableStroke.GetObject(1)).GetObject(j).GetVar();
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 7; j++)
                            item.ScopeValue[j] = (double)TableStroke.GetObject(1).GetVar();
                    }

                    // Заполняем Изменение дальности на 1 единицу (м)
                    item.RangePerUnit = (double) TableStroke.GetObject(2).GetVar();
                    // Заполняем Изменение прицела на 50м дальности (ед)
                    item.RangeOffset = (double)TableStroke.GetObject(3).GetVar();
                    // Заполняем Изменение прицела на 100м высоты (ед)
                    item.HeightOffset = (double)TableStroke.GetObject(4).GetVar();
                    // Заполняем Деривация (сдвиг горизонтальной наводки) (ед)
                    item.Derivation = (double)TableStroke.GetObject(5).GetVar();

                    // Если хотя-бы один снаряд имеет деривацию,
                    // то ставим в свойствах оружия и снаряда
                    if (item.Derivation < 0)
                    {
                        GunList.GetGun(GunName).DerivationExist = true;
                        proj.DerivationExist = true;
                    }

                    // Заполняем Боковой ветер (сдвиг горизонтальной наводки) (м/с)
                    item.SideWind = (double)TableStroke.GetObject(6).GetVar();
                    // Заполняем Продольный ветер (м/с)
                    item.LongitudinalWind = (double)TableStroke.GetObject(7).GetVar();
                    // Заполняем Температура воздуха на 10^C (15^)
                    item.AirTemperature = (double)TableStroke.GetObject(8).GetVar();
                    // Заполняем Давление возд. на 10hPa (1013,25)
                    item.AirPress = (double)TableStroke.GetObject(9).GetVar();
                    // Заполняем Плотность воздуха на 1% (ниже) (1,221кг/м3)
                    item.AirDensityDown = (double)TableStroke.GetObject(10).GetVar();
                    // Заполняем Плотность воздуха на 1% (выше) (1,221кг/м3)
                    item.AirDensityUp = (double)TableStroke.GetObject(11).GetVar();
                    // Заполняем Время полета (с)
                    item.TimeOfFlight = (double)TableStroke.GetObject(12).GetVar();
                    SumTime += item.TimeOfFlight;
                    // Заполняем Вероятность боковая (м)
                    item.SideProbability = (double)TableStroke.GetObject(13).GetVar();
                    // Заполняем Вероятность на дальность (м)
                    item.RangeProbability = (double)TableStroke.GetObject(14).GetVar();

                    if (!isArcNext)
                        proj.AddItemInTable(item);
                    else
                        proj.AddItemInArcTable(item);
                }

                proj.TimeAverage = SumTime / projectile.Value.GetLength();

                bool isSkip = false;

                // Проверяем на случай повторения снарядов (случай как с БМ-21)
                foreach (var p in GunList.GetGun(GunName).projectiles)
                    if (p.Name == proj.Name)
                        isSkip = true;

                if(!isSkip)
                    GunList.GetGun(GunName).projectiles.Add(proj);

            }
        }

        private List<String> ParseFile(String str)
        {
            List<String> blocks = new List<String>();
            char[] chars = str.ToCharArray();
            string CurrentBlock = "";
            foreach (var ch in chars)
            {
                if (ch == '\n' || ch == ';' || ch == '(' || ch == ')' || ch == ',' ||
                    ch == 0x00 || ch == '.' || ch == '[' || ch == ']' || ch == ' ' ||
                    ch == '\'' || ch == '"' || ch == '{' || ch == '}' || ch == '\t')
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
                if (ch == '\n' || ch == ';' || ch == ',' || ch == '.' || ch == '{' ||
                    ch == '\'' || ch == '"' || ch == '[' || ch == ']' || ch == '}' ||
                    ch == ':')
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
            
            // Для разбора массивов
            int iDepthLevel = 0;
            
            // Для считывания элементов массивов
            bool isElementValueStart = false;
            String strCurrentValue = "";
            SQFVarList tmp_lvl2 = null;

            // Итоговый массив с данными по снарядам
            // Его нужно будет десереализовать
            Dictionary<String, SQFVarList> sqf_vars = new Dictionary<String, SQFVarList>();

            // Надо что-то вроде map<string, class(string,int,map)>
            foreach(var block in blocks)
            {
                // Скипаем все комменты
                if (block == "//")
                {
                    isWaitComment = true;
                    continue;
                }
                if (isWaitComment && block == "\n")
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
                    }
                    continue;
                }

                // На случай, если case для нескольких блоков (не более 3)
                if (block == "||")
                {
                    iCurrentName++;
                    continue;
                }

                if (isProjectileNameStrart)
                {
                    ProjectileName[iCurrentName] += block;
                    continue;
                }

                // На блоке default можно уже и заканчивать парсинг
                if (block == "default")
                    break;

                // Разбор блока [] для всего массива projectile.
                // Лучше заполнять сразу как массив с названием ProjectileName,
                // у которого просто будем учитывать вложенность для [ и ].
                // Таким образом должен уйти от лишнего кода.
                if (block == "[")
                {
                    if (ProjectileName[0] != "")
                    {
                        iDepthLevel++;
                        if (iDepthLevel == 2)
                            tmp_lvl2 = (SQFVarList)CreateVar(SQF_TYPE_VAR.ARRAY, null);
                        if (iDepthLevel == 3)
                            tmp_lvl2.AddObject((SQFVarList)CreateVar(SQF_TYPE_VAR.ARRAY, null));
                    }
                }
                
                // Пока-что просто снижаем уровень вложенности.
                if(block == "]")
                {
                    iDepthLevel--;

                    if(iDepthLevel < 0)
                    {
                        error = SQF_READ_ERROR.PARSING_ERROR;
                        return null;
                    }

                    if (iDepthLevel == 0)
                    {
                        iCurrentName = 0;
                        tmp_lvl2 = null;
                        for(int i = 0; i < ProjectileName.Length; i++)
                            ProjectileName[i] = "";
                    }

                    if(iDepthLevel == 1)
                    {
                        for(int i = 0; i <= iCurrentName; i++)
                        {
                            sqf_vars[ProjectileName[i]].AddObject(tmp_lvl2);
                        }

                        tmp_lvl2 = null;
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
                        if (iDepthLevel == 2)
                        {
                            if (isString)
                                tmp_lvl2.AddObject(CreateVar(SQF_TYPE_VAR.STRING, strCurrentValue));
                            else
                                tmp_lvl2.AddObject(CreateVar(SQF_TYPE_VAR.DOUBLE, value));
                        }
                        else if (iDepthLevel == 3)
                            ((SQFVarList)(tmp_lvl2.GetLastObject())).AddObject(CreateVar(SQF_TYPE_VAR.DOUBLE, value));

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
