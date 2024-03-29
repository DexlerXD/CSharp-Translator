﻿using System;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;

namespace Translate
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("input.txt");
            //Объявляем массив строк и добавляем в массив контент файла

            Console.WriteLine("Enter the language from which you want to translate");
            string firstLang = Console.ReadLine();

            Console.WriteLine("Enter the language you want to translate to");
            string secondLang = Console.ReadLine();
            //Запрашиваем у пользователя язык, с которого нужно переводить и язык, на который нужно переводить текст

            string[] result = new string[content.Length];
            //Объявляем массив с результатом перевода длиной в массим в контентом файла

            for (int i = 0; i < content.Length; i++)
            {
                result[i] = TranslateContent(firstLang, secondLang, content[i]);
                Console.WriteLine(content[i]);
                Console.WriteLine(result[i]);
                //Выполняем перевод строки и выводим результат на экран
            }

            File.WriteAllLines("output.txt", result);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            //Записываем результат перевода в файл и ждём нажатия от пользователя
        }

        public static string TranslateContent(string firstLang, string secondLang, string content)
        {
            string resultSentence = "";
            int startIndex = 1;
            int endIndex = 1;
            string tempSentence;

            string result;
            string url;

            url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                firstLang, secondLang, WebUtility.UrlEncode(content));
            //Делаем запрос по ссылке, вставляя в неё нужные нам элементы: языки для перевода и отформатированный контент

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            try
            {
                result = webClient.DownloadString(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return content;
            }
            //Пытаемся скачать результат нашего запроса. Если получилось, то результат становится скачанным переводом. Если не получилось - то выводим сообщение ошибки и возвращаем контент для перевода.

            int amountOfSentence = GetAmountOfSentence(content);

            for (int i = 0; i < amountOfSentence; i++)
            {
                startIndex = result.IndexOf("\"", endIndex, StringComparison.Ordinal) + 1;
                endIndex = result.IndexOf("\"", startIndex, StringComparison.Ordinal);
                tempSentence = result.Substring(startIndex, endIndex - startIndex);

                endIndex = result.IndexOf("]", startIndex, StringComparison.Ordinal);
                if (!isRealSentence(tempSentence))
                {
                    i--;
                    continue;
                }

                resultSentence += tempSentence;
            }

            return resultSentence;
            //Результат перевода вычленяем из полученного результата с сайта и возвращаем его
        }

        private static int GetAmountOfSentence(string content)
        {
            //Метод для возврата количества фраз
            int amountOfSentence = 0;

            foreach(char ch in content)
            {
                if (ch == '.' || ch == '!' || ch == '?')
                    amountOfSentence++;
            }
            //Метод считает каждый символ и при нахождении одного из трёх символов инкрементирует переменную

            return amountOfSentence;
        }

        private static bool isRealSentence(string sentence)
        {
            //Метод определения действительности предложения
            bool isReal = true;

            if (sentence.Length == 0)
                isReal = false;
            //Если предложение имеет длину 0, то переменная обращается в false

            if (!sentence.Contains(' '))
                isReal = false;
            //Если предложение не имеет кавычек, то переменная обращаеися в false

            return isReal;
        }
    }
}
