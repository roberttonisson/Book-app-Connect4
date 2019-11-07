﻿using System;
using System.Linq;
using DAL;
using DOMAIN;
using Newtonsoft.Json;

namespace GameEngine
{
    public static class GameConfigHandler
    {
        private const string FileName = "gamesettings.json";
        private const string DefaultSaveName = "defaultSave.json";

        public static void SaveConfig(GameSettings settings)
        {
            using (var ctx = new AppDatabaseContext())
            {
                if (!ctx.GameConfig.Any())
                {
                    var config = new GameConfig
                        {GameConfigJson = JsonConvert.SerializeObject(settings, Formatting.Indented)};
                    ctx.GameConfig.Add(config);
                }
                else
                {
                    var config = ctx.GameConfig.First();
                    config.GameConfigJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
                }

                ctx.SaveChanges();
            }
        }

        public static GameSettings LoadConfig(string fileName = FileName)
        {
            using (var ctx = new AppDatabaseContext())
            {
                if (ctx.GameConfig.Any())
                {
                    return JsonConvert.DeserializeObject<GameSettings>(ctx.GameConfig.First().GameConfigJson);
                }

                return new GameSettings();
            }
        }


        public static bool Save(Game game)
        {
            using (var ctx = new AppDatabaseContext())
            {
                var (name, overwrite, canceled) = AskFileName(ctx, null, "X");
                if (canceled) return true;
                if (!overwrite)
                {
                    var saveGame = new SaveGame
                        {Name = name, GameObjectJson = JsonConvert.SerializeObject(game, Formatting.Indented)};
                    ctx.SaveGames.Add(saveGame);
                }
                else
                {
                    var saveGame = ctx.SaveGames.First(n => n.Name == name);
                    saveGame.GameObjectJson = JsonConvert.SerializeObject(game, Formatting.Indented);
                }

                ctx.SaveChanges();
            }

            return false;
        }

        public static Game LoadGame(string saveName = DefaultSaveName)
        {
            using (var ctx = new AppDatabaseContext())
            {
                var res = JsonConvert.DeserializeObject<Game>(ctx.SaveGames.First(n => n.Name == saveName)
                    .GameObjectJson);
                return res;
            }
        }

        public static (string name, bool overwrite, bool canceled) AskFileName(AppDatabaseContext ctx,
            int? cancelIntValue = null, string cancelStrValue = "")
        {
            var valid = false;
            var userInput = "";
            var overwrite = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Please enter your preferred Save name.");
                if (cancelIntValue.HasValue || !string.IsNullOrWhiteSpace(cancelStrValue))
                {
                    Console.WriteLine($"To go back to the game enter: {cancelIntValue}" +
                                      $"{(cancelIntValue.HasValue && !string.IsNullOrWhiteSpace(cancelStrValue) ? " or " : "")}" +
                                      $"{cancelStrValue}");
                }
                Console.WriteLine(">");
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Save name can not be empty.");
                    continue;
                }

                if (userInput.ToUpper().Trim() == cancelStrValue) return ("", false, true);

                if (ctx.SaveGames.Any(n => n.Name == userInput))
                {
                    Console.WriteLine(
                        "That name already exist. Would you like to overwrite?(Enter Y to agree. N to not overwrite. B to go back.)");
                    Console.WriteLine(">");
                    var agreement = Console.ReadLine();
                    if (agreement != null && agreement.ToUpper().Trim() == "Y")
                    {
                        overwrite = true;
                    }
                    else
                    {
                        continue;
                    }
                }


                valid = true;
            } while (!valid);

            return (userInput, overwrite, false);
        }
    }
}