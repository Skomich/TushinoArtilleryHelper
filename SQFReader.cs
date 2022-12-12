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
        INT,
        STRING,
        ARRAY,
        MAP
    }

    abstract class SQFVar
    {
        private SQF_TYPE_VAR varType = SQF_TYPE_VAR.INT;

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

    class SQFVarInt : SQFVar
    {
        private int varInt;

        public SQFVarInt(SQF_TYPE_VAR varType, int iVar) : base(varType, iVar)
        {}

        public override Object GetVar()
        {
            return varInt;
        }

        protected override void SetVar(Object var)
        {
            varInt = (int)var;
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

            List<String> blocks = ParseFile(buffer);
            ParseBlocks(blocks);

            return;
        }

        private List<String> ParseFile(String str)
        {
            List<String> blocks = new List<String>();
            char[] chars = str.ToCharArray();
            string CurrentBlock = "";
            foreach(var ch in chars)
            {
                if (ch == '\n' || ch == ' ' || ch == '\t' || ch == 0x00 ||
                    ch == ';' || ch == ',' || ch == '.' || ch == '\'')
                {
                    blocks.Add(CurrentBlock);
                    if (ch == '\n' || ch == ';' || ch == ',' || ch == '.')
                        blocks.Add(ch + "");
                    CurrentBlock = "";
                    continue;
                }
                CurrentBlock += ch;
            }

            return blocks;
        }

        private void ParseBlocks(List<String> blocks)
        {

            bool isWaitComment = false;
            bool isVar = false;
            bool isWaitInitVar = false;

            String NameVar = "";
            String ValueVar = "";

            Dictionary<String, SQFVar> sqf_vars = new Dictionary<String, SQFVar>();

            // Надо что-то вроде map<string, class(string,int,map)>
            foreach(var block in blocks)
            {
                // Скипаем все комменты
                if (block == "//")
                    isWaitComment = true;
                if (block == "\n" && isWaitComment)
                    isWaitComment = false;
                if (isWaitComment)
                    continue;

                // Ключевые слова, которые нам нафиг не нужны
                if (block == "private" || block == "protected" ||
                    block == "public" || block == "static")
                {
                    isVar = true;
                    continue;
                }

                if(block == "=" && isVar)
                {
                    isWaitInitVar = true;
                    isVar = false;
                    continue;
                }

                if (isVar)
                {
                    NameVar += block;
                }

                if (block == ";" && isWaitInitVar)
                {
                    // need go add in map
                    SQFVar var = CreateVar(SQF_TYPE_VAR.STRING, ValueVar);
                    sqf_vars.Add(NameVar, var);
                    isVar = false;
                    isWaitInitVar = false;
                }

                if (isWaitInitVar)
                {
                    ValueVar += block;
                }
            }
        }

        // obj may be null
        private SQFVar CreateVar(SQF_TYPE_VAR type, Object obj)
        {
            SQFVar var;
            switch(type)
            {
                case SQF_TYPE_VAR.INT:
                    {
                        if(obj == null)
                            var = new SQFVarInt(type, 0);
                        else
                            var = new SQFVarInt(type, (int)obj);
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
