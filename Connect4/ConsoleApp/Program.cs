using System;
using System.Collections.Generic;
using ConsoleUI;
using DAL;
using GameEngine;
using MenuSystem;
using Microsoft.EntityFrameworkCore;


namespace icd0008_2019f
{
    class Program
    {
        private static GameSettings _settings = default!;
        private static Game _game = default!;
        private static bool _loadGame;
        private static SavedGamesList _savedGamesList = default!;
        private static DbContextOptions _dbOptions = default!;

        private static void Main(string[] args)
        {
            Console.Clear();

            _dbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;

            _settings = GameConfigHandler.LoadConfig(_dbOptions);

            Console.WriteLine($"Hello {_settings.GameName}!");

            var gameMenu = new Menu(1)
            {
                Title = "Start a new game of Connect 4",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Computer starts",
                            CommandToExecute = ComputerStarts
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Human starts",
                            CommandToExecute = HumanStarts
                        }
                    },
                    {
                        "3", new MenuItem()
                        {
                            Title = "Human against Human",
                            CommandToExecute = HumanAgainstHuman
                        }
                    },
                }
            };

            var menu0 = new Menu(0)
            {
                Title = "Connect 4 Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "S", new MenuItem()
                        {
                            Title = "Start a new game",
                            CommandToExecute = gameMenu.Run
                        }
                    },
                    {
                        "J", new MenuItem()
                        {
                            Title = "Set defaults for game (save to JSON)",
                            CommandToExecute = SaveSettings
                        }
                    },
                    {
                        "L", new MenuItem()
                        {
                            Title = "Load game",
                            CommandToExecute = LoadGame
                        }
                    }
                }
            };


            menu0.Run();
        }

        static string SaveSettings()
        {
            Console.Clear();

            var boardWidth = 0;
            var boardHeight = 0;
            var userCanceled = false;

            (boardWidth, userCanceled) = GetUserIntInput("Enter board width, min 4.", 4, 100, 0);
            if (userCanceled) return "";

            (boardHeight, userCanceled) = GetUserIntInput("Enter board height, min 4", 4, 100, 0);
            if (userCanceled) return "";

            _settings.BoardHeight = boardHeight;
            _settings.BoardWidth = boardWidth;
            GameConfigHandler.SaveConfig(_settings, _dbOptions);

            return "";
        }

        static string LoadGame()
        {
            _savedGamesList = new SavedGamesList();
            Console.Clear();
            var ctx = new AppDatabaseContext(_dbOptions);
            using (ctx)
            {
                foreach (var save in ctx.SaveGames)
                {
                    _savedGamesList.savedGames.Add(save.Name);
                }
            }

            for (int i = 0; i < _savedGamesList.savedGames.Count; i++)
            {
                Console.WriteLine((i + 1) + " " + _savedGamesList.savedGames[i]);
            }

            var userInput = GetUserIntInput("Select your save file number.", 1, _savedGamesList.savedGames.Count, null,
                "X");
            if (userInput.wasCanceled) return "";

            _game = GameConfigHandler.LoadGame(_dbOptions, _savedGamesList.savedGames[userInput.result - 1]);
            _loadGame = true;
            return RunGame();
        }


        private static string HumanAgainstHuman()
        {
            if (!_loadGame)
            {
                _game = new Game(_settings, false, false);
            }

            return RunGame();
        }

        private static string ComputerStarts()
        {
            if (!_loadGame)
            {
                _game = new Game(_settings, true, true);
            }

            return RunGame();
        }

        private static string HumanStarts()
        {
            if (!_loadGame)
            {
                _game = new Game(_settings, true);
            }

            return RunGame();
        }

        static string RunGame()
        {
            var currentMoves = 0;
            var done = false;
            var win = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(_game);

                if (_game._computerPlays && _game._playerZeroMove)
                {
                    if (_game.ComputerMove())
                    {
                        win = true;
                        done = true;
                        continue;
                    }
                }
                else
                {
                    var (result, wasCanceled) = GetUserIntInput(
                        ("Which column would you like to out your piece?(1-" + _game.BoardWidth + ")"),
                        1, _game.BoardWidth, null, "X", true, "S");
                    if (wasCanceled)
                    {
                        win = true;
                        done = true;
                        continue;
                    }

                    if (_game.Move(result))
                    {
                        done = true;
                        continue;
                    }
                }

                currentMoves += 1;
                done = _game.BoardHeight * _game.BoardWidth <= currentMoves;
            } while (!done);

            Console.Clear();
            GameUI.PrintBoard(_game);
            _loadGame = false;
            if (win)
            {
                var cell = !_game._playerZeroMove ? CellState.O : CellState.X;
                Console.WriteLine($"To save your game enter: {cell.ToString()}");
            }

            Console.WriteLine("Game over! Press any key to continue.");
            Console.ReadLine();
            Console.WriteLine();
            return "M";
        }

        static (int result, bool wasCanceled) GetUserIntInput(string prompt, int min, int max,
            int? cancelIntValue = null, string cancelStrValue = "", bool inGameInputs = false,
            string saveGameValue = "")
        {
            do
            {
                Console.WriteLine(prompt);
                if (cancelIntValue.HasValue || !string.IsNullOrWhiteSpace(cancelStrValue))
                {
                    Console.WriteLine($"To exit enter: {cancelIntValue}" +
                                      $"{(cancelIntValue.HasValue && !string.IsNullOrWhiteSpace(cancelStrValue) ? " or " : "")}" +
                                      $"{cancelStrValue}");
                }

                if (!string.IsNullOrWhiteSpace(saveGameValue))
                {
                    Console.WriteLine($"To save your game enter: {saveGameValue}");
                }

                Console.Write(">");
                var consoleLine = Console.ReadLine()?.ToUpper().Trim();

                if (consoleLine == cancelStrValue) return (0, true);
                if (consoleLine == saveGameValue)
                {
                    var canceled = GameConfigHandler.Save(_game, _dbOptions);
                    if (true)
                    {
                        _loadGame = true;
                        RunGame();
                    }

                    return (0, true);
                }

                if (int.TryParse(consoleLine, out var userInt))
                {
                    if (userInt == cancelIntValue) return (0, true);

                    if (min > userInt || max < userInt)
                    {
                        Console.WriteLine("Invalid input, please select correct number.");
                        continue;
                    }
                    if (inGameInputs && _game.ColumnStatus[userInt] >= _game.BoardHeight)
                    {
                        Console.WriteLine("Column is full, select another one.");
                        continue;
                    }

                    return userInt == cancelIntValue ? (userInt, true) : (userInt, false);
                }

                Console.WriteLine($"'{consoleLine}' cant be converted to int value!");
            } while (true);
        }
    }
}