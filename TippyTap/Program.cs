using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TippyTap
{
    public class Program
    {
        public static List<string> WordCache = new List<string>();
        public static Random random = new Random();

        public static void Main(string[] argsArr)
        {
            LoadWordCache();

            List<string> args = new List<string>(argsArr);

            string arg = args.Count >= 1 ? args[0] : "";

            int numberOfWordsToType;
            if(arg == "")
            {
                numberOfWordsToType = 0;
            }
            else if(!int.TryParse(arg, out numberOfWordsToType))
            {
                Console.WriteLine("Invalid argument supplied.");
                return;
            }

            string wordsToTypeMessage = numberOfWordsToType == 0 ? "infinity" : numberOfWordsToType.ToString();
            Console.WriteLine($"Welcome to TippyTap! You have {wordsToTypeMessage} word(s) left to type. GO!");

            int attempts = 0;
            int remainingWordsToType = numberOfWordsToType;
            bool gameCompleted = false;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (!gameCompleted)
            {
                string randomWord = GetRandomWord();
                bool wordCompleted = false;
                while (!wordCompleted)
                {
                    Console.WriteLine(randomWord);
                    string userInput = Console.ReadLine();
                    wordCompleted = string.Equals(userInput, randomWord);
                    attempts++;
                }
                gameCompleted = --remainingWordsToType == 0;
            }
            stopWatch.Stop();

            TimeSpan timeElapsed = stopWatch.Elapsed;

            Console.WriteLine($"You have completed all {numberOfWordsToType} word(s)!");
            Console.WriteLine($"This took you {timeElapsed.TotalSeconds.ToString()} seconds!");
            Console.WriteLine($"You correctly typed {numberOfWordsToType} word(s) with {attempts} attempt(s). That is {((double)numberOfWordsToType/attempts).ToString("p2")} accuracy!");
            return;
        }

        private static string GetRandomWord()
        {
            int randomIndex = random.Next(WordCache.Count);
            return WordCache[randomIndex];
        }

        private static void LoadWordCache()
        {   
            try
            {
                //using hash set to avoid possible dupes
                HashSet<string> wordHashSet = new HashSet<string>();
                string appDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
                using (StreamReader r = new StreamReader( appDir + "\\words.json"))
                {
                    string wordsJson = r.ReadToEnd();

                    JObject job = JObject.Parse(wordsJson);

                    foreach (var prop in job.Properties())
                    {
                        wordHashSet.Add(prop.Name);
                    }
                }
                WordCache = wordHashSet.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred reading words file.");
                Environment.Exit(1);
            }
        }
    }
}
