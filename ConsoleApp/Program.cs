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
                Title = "Start a new game of Connect 4",
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
                        "2", new MenuItem()
                        {
                            Title = "Human starts",
                            CommandToExecute = TestGame
                        }
                    },
                    {
                        "3", new MenuItem()
                        {
                            Title = "Human against Human",
                            CommandToExecute = TestGame
                        }
                    },
                }
            };

            var menu0 = new Menu(0)
            {
                Title = "Connecct 4 Main Menu",
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
            var game = new Game(8, 10);

            var done = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);

                var rowNum = -1;
                var currentMoves = 0;
                do
                {
                    Console.WriteLine("Which row would you like to out your piece?(1-" + game.BoardWidth + ")");
                    Console.Write(">");
                    var selectedRow = Console.ReadLine();

                    if (!int.TryParse(selectedRow, out rowNum) || game.RowStatus[rowNum] >= game.BoardHeight)
                    {
                        Console.WriteLine($"{selectedRow} is not a correct row or is already full!");
                        rowNum = -1;
                    }
                    
                } while (rowNum < 1);

                game.Move(rowNum);
                currentMoves++;
                done = game.BoardHeight * game.BoardWidth <= currentMoves;
            } while (!done);


            return "GAME OVER!!";
        }
    }
}