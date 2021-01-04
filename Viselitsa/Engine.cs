using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Viselitsa
{



    public class Engine


    {
        
        private readonly int allowedMisses;  // Тут хранится возможное количество ошибок
        private bool[] openIndexes;    // Какие буквы по каким индексам уже открыты
        private int triesCounter = 0;  //   Число попыток
        private string triedLetters;   // Буквы, которые уже попробовали
        public GameStatus GameStatus { get; private set; } = GameStatus.NotStarted;   //   Статус игры
        public string Word { get; private set; }   // поле
        public string TriedLetters  //    
        {
            get
            {
                var chars = triedLetters.ToCharArray();
                Array.Sort(chars);
                return new string(chars);

            }
        }
      
        public int RemainingTries { get

            {
                return allowedMisses - triesCounter;
            }
                
                
                
                }
        
      
        public Engine (int alowedMisses=6)
        {
            if (alowedMisses<5 || alowedMisses>8)
            {
                throw new ArgumentException("Number of allowed misses should be between 5 - 8");
            }

           
            this.allowedMisses = alowedMisses;  // сохраняем аргумент в поле

        }

        public string GenerateWord()
        {
            string[] words = File.ReadAllLines("WordsStockRus.txt");
            Random r = new Random(DateTime.Now.Millisecond);
            int randomIndex = r.Next(words.Length - 1);
            Word = words[randomIndex];
            openIndexes = new bool[Word.Length];
            GameStatus = GameStatus.InProgress;
            return Word;
        }



        public string GuessLetter (char letter)
        {
            if (triesCounter==allowedMisses)
            {
                throw new InvalidOperationException($"Exceeded the max misses number: {allowedMisses}");

            } 
            if (GameStatus!=GameStatus.InProgress)
            {
                throw new InvalidOperationException($"Inaproppriate state of the game : {GameStatus}");

            }
            bool openAny = false;
            string result = string.Empty;
            for (int i=0; i<Word.Length; i++)
            {
                if (Word[i] == letter)
                {
                    openIndexes[i] = true;
                    openAny= true;
                }
                if (openIndexes[i])
                {
                    result += Word[i];

                }
                else
                {
                    result += "-";

                }
            }
            if (!openAny) triesCounter++;
            triedLetters += letter;
            if (isWin())
            {
                GameStatus = GameStatus.Won;
            }
            else if (triesCounter==allowedMisses)
            
                GameStatus = GameStatus.Lost;
            
            return result;

           
        }
        private bool isWin()
        {
            foreach(var cur in openIndexes)
            {
                if (cur == false)
                {
                    return false;
                }
              
            }
            return true;
        }






        
            
        





    }

    


   



}
