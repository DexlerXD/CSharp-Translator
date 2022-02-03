using System;
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
                string trimmed = String.Concat(content[i].Where(c => !Char.IsWhiteSpace(c))); 
                //Создаём строку, которая явялется строкой из массива контента без пробелов
                result[i] = TranslateContent(firstLang, secondLang, trimmed);
                Console.WriteLine(content[i] + " -> " + result[i]);
                //Выполняем перевод строки и выводим результат на экран
            }

            File.WriteAllLines("output.txt", result);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            //Записываем результат перевода в файл и ждём нажатия от пользователя
        }

        public static string TranslateContent(string firstLang, string secondLang, string content)
        {
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

            result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
            return result;
            //Результат перевода вычленяем из полученного результата с сайта и возвращаем его
        }
    }
}