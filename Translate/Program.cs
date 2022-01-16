using System;
using System.Net;
using System.Text;
using System.IO;

namespace Translate
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content;
            content = File.ReadAllLines("input.txt");

            Console.WriteLine("Enter the language from which you want to translate");
            string firstLang = Console.ReadLine();

            Console.WriteLine("Enter the language you want to translate to");
            string secondLang = Console.ReadLine();

            string[] result = new string[content.Length];

            for (int i = 0; i < content.Length; i++)
            {
                Console.WriteLine(content[i]);
                result[i] = TranslateContent(firstLang, secondLang, content[i]);
                Console.WriteLine(result[i]);
            }

            File.WriteAllLines("translate.txt", result);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static string TranslateContent(string firstLang, string secondLang, string content)
        {
            string result;
            string url;

            url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                firstLang, secondLang, WebUtility.UrlEncode(content));

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            try
            {
                result = webClient.DownloadString(url);
            }
            catch (Exception e)
            {
                return content;
            }

            Console.WriteLine("debug 1 " + result);
            result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
            Console.WriteLine("debug 2 " + result);
            return result;
        }
    }
}