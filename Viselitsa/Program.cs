using System;
using System.Text;

namespace Viselitsa
{
    class Program
    {

        public static void Main (string[] args) {
            
            Engine game = new Engine();
            string word = game.GenerateWord();
            Console.WriteLine($"The word consists of {word.Length} letters");
            Console.WriteLine("Try to guess the word.");


            while (game.GameStatus == GameStatus.InProgress)
            {
                Console.WriteLine("Pick a letter.");
                char c = Console.ReadLine().ToCharArray()[0];

                string curState = game.GuessLetter(c);
                Console.WriteLine(curState);

                Console.WriteLine($"Remaining tries = {game.RemainingTries}");

                Console.WriteLine($"Tried letters: {game.TriedLetters}");


            }
            if (game.GameStatus == GameStatus.Lost)
            {
                Console.WriteLine("You are hanged!(");
                Console.WriteLine($"The word was : {game.Word}");
            }
            else if (game.GameStatus == GameStatus.Won)
            {
                Console.WriteLine("You won!");
            }
            Console.ReadLine();
        
        
        
        }
            



            
            
    


            

            

        

        }
    }

