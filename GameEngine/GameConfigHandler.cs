using System;
using Newtonsoft.Json;

namespace GameEngine
{
    public static class GameConfigHandler
    {
        private const string FileName = "gamesettings.json";
        public const string DefaultSaveName = "defaultSave.json";
        public const string SavedGames = "SavedGames.json";

        public static void SaveConfig(GameSettings settings, string fileName = FileName)
        {
            using (var writer = System.IO.File.CreateText(fileName))
            {
                var jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented);
                writer.Write(jsonString);
            }
        }

        public static GameSettings LoadConfig(string fileName = FileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                var res = JsonConvert.DeserializeObject<GameSettings>(jsonString);
                return res;
            }

            return new GameSettings();
        }

        public static SavedGames LoadSavedGames(string fileName = SavedGames)
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                var res = JsonConvert.DeserializeObject<SavedGames>(jsonString);
                return res;
            }

            return new SavedGames();
        }

        public static void AddSavedGame(SavedGames currentSaves, Game game)
        {
            var file = AskFileName(currentSaves);
            if (!file.overwrite)
            {
                currentSaves.savedGames.Add(file.name + ".json");
            }

            using (var writer = System.IO.File.CreateText(SavedGames))
            {
                var jsonString = JsonConvert.SerializeObject(currentSaves, Formatting.Indented);
                writer.Write(jsonString);
            }

            SaveGame(game, file.name + ".json");
        }

        public static void SaveGame(Game game, string fileName = DefaultSaveName)
        {
            using (var writer = System.IO.File.CreateText(fileName))
            {
                var jsonString = JsonConvert.SerializeObject(game, Formatting.Indented);
                writer.Write(jsonString);
            }
        }

        public static Game LoadGame(string fileName = DefaultSaveName)
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                var res = JsonConvert.DeserializeObject<Game>(jsonString);
                return res;
            }

            return new Game(new GameSettings());
        }

        public static (string name, bool overwrite) AskFileName(SavedGames currentSaves)
        {
            var notEmpty = false;
            var userInput = "";
            var overwrite = false;
            do
            {
                Console.WriteLine("Please enter your preferred Save name.");
                Console.WriteLine(">");
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Save name can not be empty.");
                    continue;
                }

                if (currentSaves.savedGames.Contains(userInput + ".json"))
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

                notEmpty = true;
            } while (!notEmpty);

            return (userInput, overwrite);
        }
    }
}