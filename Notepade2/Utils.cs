using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notepade2
{
    internal static class Utils
    {
        public static string CheckRegex(string inputString)
        {
            string phoneNumberCheck = @"(^+\d{1,2})?(((\d{3}))|(-?\d{3}-)|(\d{3}))((\d{3}-\d{4})|(\d{3}-\d\d-\d\d)|(\d{7})|(\d{3}-\d-\d{3}))";
            do
            {
                if (!Regex.IsMatch(inputString, phoneNumberCheck))
                {
                    Console.Write("Некорректный формат номера телефона. Попробуйте еще раз:  ");
                    inputString = Console.ReadLine();
                }
            } while (!Regex.IsMatch(inputString, phoneNumberCheck));
            return inputString;
        }

        public static string RequestData(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }

        public static string RequestYesOrNo(string msg)
        {
            Console.WriteLine(new string('-', 60));
            Console.Write(msg);
 
            return Console.ReadLine();
        }
        public static string CheckNote(string inputString)
        {
            if (inputString.Length > 100)
            {
                Console.WriteLine("Заметка более 100 символов, будут добавлены только первые 100 символов.");
                inputString = inputString.Remove(100);
                return inputString;
            }
            return inputString;
        }
        public static void SetHeadlineOrEnding(string inputString)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(inputString);

        }
        public static void PressKeyToContinue()
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
