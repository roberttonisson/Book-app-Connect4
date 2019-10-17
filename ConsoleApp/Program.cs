using System;
using System.Collections.Generic;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace icd0008_2019f
{
    class Program
    {
        private static GameSettings _settings = default!;
        private static Game _game = default!;
        private static bool _loadGame;
        private static SavedGames _savedGames = default!;

        private static void Main(string[] args)
        {
            Console.Clear();

            _settings = GameConfigHandler.LoadConfig();
            _savedGames = GameConfigHandler.LoadSavedGames();

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
            GameConfigHandler.SaveConfig(_settings);

            return "";
        }

        static string LoadGame()
        {
            Console.Clear();
            Console.WriteLine("Select your save file number.");
            Console.WriteLine("_________________________________________");
            for (int i = 0; i < _savedGames.savedGames.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) + " " + _savedGames.savedGames[i].Substring(0, _savedGames.savedGames[i].Length - 5));
            }

            var savefileIdx = 0;
            var correctInput = false;
            do
            {
                Console.WriteLine(">");
                var consoleLine = Console.ReadLine();
                if (int.TryParse(consoleLine, out var userInt))
                {
                    if (userInt > _savedGames.savedGames.Count || userInt < 1)
                    {
                        Console.WriteLine("Invalid save file number. Select correct one.");
                        continue;
                    }

                    savefileIdx = userInt - 1;
                    correctInput = true;
                }
            } while (!correctInput);

            _game = GameConfigHandler.LoadGame(_savedGames.savedGames[savefileIdx]);
            _loadGame = true;
            return TestGame();
        }


        private static string TestGame()
        {
            if (!_loadGame)
            {
                _game = new Game(_settings);
            }

            var currentMoves = 0;

            var done = false;
            do

            {
                Console.Clear();
                GameUI.PrintBoard(_game);

                var (result, wasCanceled) = GetUserIntInput(
                    ("Which column would you like to out your piece?(1-" + _game.BoardWidth + ")"),
                    1, _game.BoardWidth, null, "X", true, "S");
                if (wasCanceled)
                {
                    done = true;
                    continue;
                }

                _game.Move(result);
                currentMoves += 1;
                done = _game.BoardHeight * _game.BoardWidth <= currentMoves;
            } while (!done);

            _loadGame = false;
            Console.WriteLine("Game over!");
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
                    Console.WriteLine($"To cancel input enter: {cancelIntValue}" +
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
                    GameConfigHandler.AddSavedGame(_savedGames, _game);
                    _savedGames = GameConfigHandler.LoadSavedGames();
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

                    if (inGameInputs && _game.ColumnStatus[userInt] >= _game.BoardWidth)
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