using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Notepade2
{
    class Program
    {
        static void Main(string[] args)
        {
            Notebook notebook = new Notebook();
            notebook.LoadFromTextFile();
            while (true)
            {
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("\t1. Добавить запись");
                Console.WriteLine("\t2. Удалить запись");
                Console.WriteLine("\t3. Редактировать запись");
                Console.WriteLine("\t4. Вывести все записи в алфавитном порядке");
                Console.WriteLine("\t5. Найти контакт");
                Console.WriteLine("\t6. Выход");
                Console.WriteLine(new string('-', 60));
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        notebook.AddNewRecord();
                        break;
                    case "2":
                        notebook.DeleteRecord();
                        break;
                    case "3":
                        notebook.EditRecord();
                        break;
                    case "4":
                        notebook.SortRecord();
                        break;
                    case "5":
                        notebook.SearchRecord();
                        break;
                    case "6":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
