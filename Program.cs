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

        private static String[] SearchFile(string str)
        {
            String[] files = Directory.GetFiles(str, "*.sqf");
            return files;
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String[] files;
            if (Directory.Exists("./guns/"))
            {
                // Достаем путь до файла
                files = SearchFile("./guns/");

                if (files.Length == 0)
                {
                    MessageBox.Show("Отсутствуют файлы .sqf",
                       "Ошибка чтения профилей.", MessageBoxButtons.OK);
                    return;
                }

                if(files.Length > 1)
                {
                    MessageBox.Show("Множество файлов .sqf в папке guns. Удалите или переместите старые версии файлов.",
                        "Ошибка чтения профилей.", MessageBoxButtons.OK);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Сначала надо разобрать файлик
                SQFReader sqf = new SQFReader(files.First());

                if (sqf.GetLastError() != SQF_READ_ERROR.SUCCESS)
                {
                    MessageBox.Show("" + sqf.GetLastError(), "Ошибка чтения sqf профиля", MessageBoxButtons.OK);
                    return;
                }

                Application.Run(new ChooseGun());
            }
            else
            {
                MessageBox.Show("Папки 'guns' не существует!", "Ошибка чтения профилей.", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
