using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ArtilleryHelper
{

    static class Program
    {

        private static string[] ProcessFile(string str)
        {
            string[] files = Directory.GetFiles(str, "*.sqf");
            return files;
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] files;
            if (File.Exists("./guns"))
            {
                files = ProcessFile("./guns");
                if (files.Length == 0)
                    return;

                if(files.Length > 0)
                {
                    MessageBox.Show("Ошибка чтения профилей.",
                        "Множество файлов .sqf в папке guns. Удалите или переместите старые версии файлов.", MessageBoxButtons.OK);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ChooseGun());
            }
            else
            {
                MessageBox.Show("Ошибка чтения профилей.", "Папки 'guns' не существует!", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
