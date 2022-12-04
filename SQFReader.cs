using System;
using System.Collections.Generic;
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

    internal class SQFReader
    {
        private SQF_READ_ERROR error = SQF_READ_ERROR.SUCCESS;
        public SQFReader(String FilePath)
        {
            if (FilePath.Length <= 0)
                return;

            StringBuilder buffer = new StringBuilder();

            try
            {
                StreamReader sr = new StreamReader(FilePath);
                String line = sr.ReadLine();

                while(line != null)
                {
                    buffer.Append(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            } catch (Exception ex)
            {
                error = SQF_READ_ERROR.FILE_NOT_AVAILABLE;
                return;
            }

            String GeneralString = buffer.ToString();

            parse(GeneralString);
            buffer.Clear();
        }

        private void parse(String str)
        {
            List<String> blocks = new List<String>();
            char[] chars = str.ToCharArray();
            string CurrentBlock = "";
            foreach(var ch in chars)
            {
                if (ch == 0x00 || ch == '\t' || ch == '\n' || ch == ' ')
                {
                    blocks.Add(CurrentBlock);
                    continue;
                }
                CurrentBlock += ch;
            }
        }

        public SQF_READ_ERROR GetLastError()
        {
            return error;
        }
    }
}
