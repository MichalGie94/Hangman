using System;

namespace Wisielec
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] countries_and_capitals = System.IO.File.ReadAllLines(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "countries_and_capitals.txt"));
            String[] countries = new string[countries_and_capitals.Length];
            String[] capitals = new string[countries_and_capitals.Length];
            
            for (int i = 0; i<countries_and_capitals.Length; i++)
            {
                String[] temp = countries_and_capitals[i].Split(" | ");
                countries[i] = temp[0];
                capitals[i] = temp[1];
            }

            String word;
            Random r = new Random();

            Play();

            void DrawHangman(int i)
            {
                switch (i)
                {
                    case 9:
                        Console.WriteLine();
                        break;
                    case 8:
                        Console.WriteLine();
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 7:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|       /|");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|       /|\\");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 1:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|       /|\\");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|       /");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                    case 0:
                        Console.WriteLine("\t_ _ _ _ _");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|        O");
                        Console.WriteLine("\t|       /|\\");
                        Console.WriteLine("\t|        |");
                        Console.WriteLine("\t|       / \\");
                        Console.WriteLine("\t|");
                        Console.WriteLine();
                        break;
                }
            }

            void PlayAgain()
            {
                Console.Write("Do You want to play again? (y/n) ");
                string again = Console.ReadLine();
                if(again == "y")
                {
                    Play();
                }
                if(again == "n")
                {
                    Environment.Exit(0);
                }
            }

            void HighScore(int guessses, DateTime time)
            {
                Console.Write("Enter Your Name: ");
                String name = Console.ReadLine();
                TimeSpan elapsed = DateTime.Now - time;
                Console.WriteLine("It took You " + guessses + " guesses and " + elapsed + " seconds to guess the word.\nYour score has been saved to 'Highscores.txt'.");
                System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Highscores.txt"), name + " | " + DateTime.UtcNow + " | " + elapsed.TotalSeconds + " | " + guessses + " | " + word);
            }

            void Play()
            {
                int select = r.Next(0, countries_and_capitals.Length);
                int guesses = 0;
                word = capitals[select];
                Console.WriteLine(word);

                String hiddenWord = "";
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == ' ') hiddenWord += " ";
                    else hiddenWord += "_";
                }

                for (int i = 0; i < word.Length; i++)
                {
                    Console.Write(hiddenWord[i] + " ");
                }
                Console.WriteLine();

                int lifes = 9;
                string usedLetters = "";
                string guess = "";
                DateTime time = DateTime.Now;

                while (true)
                {
                    if(lifes <= 5) Console.WriteLine("\nHINT: Capital of " + countries[select] + "\nWrong letters so far: " + usedLetters);
                    Console.WriteLine("Lifes: " + lifes);
                    Console.Write("Do You want to guess a letter or the whole word? (letter/word) ");
                    guess = Console.ReadLine();
                    guesses++;

                    if(guess == "word")
                    {
                        Console.Write("What's the word? ");
                        guess = Console.ReadLine();
                        if(guess == word)
                        {
                            DrawHangman(lifes);
                            Console.WriteLine("Congratulations! You'va guessed the word.");
                            HighScore(guesses, time);
                            PlayAgain();
                        }
                        else
                        {
                            lifes = lifes - 2;
                            DrawHangman(lifes);
                            Console.WriteLine("Sorry, that's not the word.");
                        }
                    }
                    if (guess == "letter")
                    {
                        Console.Write("Guess the letter: ");
                        string l = Console.ReadLine();
                        char letter = 'x';

                        if (l != "")
                        {
                            letter = Convert.ToChar(l);

                            if (word.Contains(letter) || word.Contains(char.ToUpper(letter)) || word.Contains(char.ToLower(letter)))
                            {
                                string newGuessedWord = "";
                                for (int i = 0; i < word.Length; i++)
                                {
                                    if (char.IsUpper(letter))
                                    {
                                        if (word[i] == letter) newGuessedWord += letter;
                                        else if (word[i] == char.ToLower(letter)) newGuessedWord += char.ToLower(letter);
                                        else newGuessedWord += hiddenWord[i];
                                    }
                                    else if (char.IsLower(letter))
                                    {
                                        if (word[i] == letter) newGuessedWord += letter;
                                        else if (word[i] == char.ToUpper(letter)) newGuessedWord += char.ToUpper(letter);
                                        else newGuessedWord += hiddenWord[i];
                                    }
                                }
                                hiddenWord = newGuessedWord;
                                DrawHangman(lifes);
                            }
                            else
                            {
                                lifes--;
                                usedLetters += char.ToUpper(letter) + " ";
                                DrawHangman(lifes);
                            }
                        }
                    }
                    for (int i = 0; i < hiddenWord.Length; i++)
                    {
                        Console.Write(hiddenWord[i] + " ");
                    }
                    Console.WriteLine();
                    if (lifes <= 0)
                    {
                        Console.Write("Sorry, You lose.\nThe capital of " + countries[select] + " is " + word + ".\n");
                        PlayAgain();
                    }
                    if (!hiddenWord.Contains("_"))
                    {
                        Console.WriteLine("CONGRATULATIONS!");
                        HighScore(guesses, time);
                        PlayAgain();
                    }
                }
            }
        }
    }
}