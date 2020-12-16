using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Converter
{
    static class History
    {
        private static ArrayList history_list = new ArrayList();
        private static string nameFile = "history.txt";

        public static string NameFile
        {
            get { return nameFile; }
            set { if (value != "") nameFile = value; }
        }

        /// <summary>
        /// Конструктор. Создаёт новый объект списка истории, загружая данные из файла
        /// </summary>
        static History()
        {
            FileInfo file = new FileInfo(nameFile);
            if (!file.Exists) // Если файл не существует
                file.Create().Close(); // Создаем
            foreach (string line in File.ReadAllLines(nameFile, Encoding.UTF8))
                history_list.Add(line);
        }

        /// <summary>
        /// Очищает список истории, как в памяти, так и в файле.
        /// </summary>
        public static void EraseHistory()
        {
            history_list.Clear();
            FileInfo file = new FileInfo(nameFile);
            if (file.Exists) // Если файл существует
                file.Delete(); // Удаляем
        }

        /// <summary>
        /// Внесение данных в список
        /// </summary>
        /// <param name="old_data">Введенное пользователем число</param>
        /// <param name="p1">Система счисления введенного числа</param>
        /// <param name="p2">Система счисления результата</param>
        /// <param name="new_data">Результат конвертации</param>
        public static void AddToHistory(string old_data, byte p1, byte p2, string new_data)
        {
            string date = DateTime.Now.ToShortDateString(); // Получение даты
            string time = DateTime.Now.ToShortTimeString(); // Получение времени
            string str = old_data + " " + p1.ToString() + " " + p2.ToString() + " " + new_data + " " + date + " " + time;
            history_list.Add(str);
        }

        // Заполняет DataGridView данными из ArrayList.
        public static void FillData(DataGridView Data)
        {
            Data.Rows.Clear();
            if (history_list.Count > 0)
            {
                string[] A;
                int i = 0;
                foreach (string line in history_list)
                {
                    A = line.Split(' ');
                    Data.Rows.Add();
                    for (int j = 0; j < 6; j++) // загрузка данных в таблицу из 6 столбцов
                        Data[j, i].Value = A[j];
                    i++;
                }
            }
        }

        // Запись истории в файл
        public static void AddToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(nameFile, false, Encoding.Default))
                {
                    foreach (string line in history_list)
                        sw.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
     }


}

