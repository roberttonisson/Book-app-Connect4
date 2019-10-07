using System;
using System.Collections.Generic;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace icd0008_2019f
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("Hello Game!");


            var gameMenu = new Menu(1)
            {
                Title = "Start a new game of Tic-Tac-Toe",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Computer starts",
                            CommandToExecute = TestGame
                        }
                    },
                    {
                        "A", new MenuItem()
                        {
                            Title = "Alien starts",
                            CommandToExecute = null
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Human starts",
                            CommandToExecute = null
                        }
                    },
                    {
                        "3", new MenuItem()
                        {
                            Title = "Human against Human",
                            CommandToExecute = null
                        }
                    },
                }
            };

            var menu0 = new Menu(0)
            {
                Title = "Tic Tac Toe Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "S", new MenuItem()
                        {
                            Title = "Start game",
                            CommandToExecute = gameMenu.Run
                        }
                    }
                }
            };


            menu0.Run();
        }

        static string TestGame()
        {
            var game = new Game(7, 7);

            var done = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);

                var userXint = -1;
                var userYint = -1;
                do
                {
                    Console.WriteLine("Give me Y value!");
                    Console.Write(">");
                    var userY = Console.ReadLine();

                    if (!int.TryParse(userY, out userYint))
                    {
                        Console.WriteLine($"{userY} is not a number!");
                    }
                } while (userYint < 0);

                do
                {
                    Console.WriteLine("Give me X value!");
                    Console.Write(">");
                    var userX = Console.ReadLine();

                    if (!int.TryParse(userX, out userXint))
                    {
                        Console.WriteLine($"{userX} is not a number!");
                    }
                } while (userXint < 0);

                game.Move(userYint, userXint);

                done = userYint == 0 && userXint == 0;
            } while (!done);


            return "GAME OVER!!";
        }
    }
}