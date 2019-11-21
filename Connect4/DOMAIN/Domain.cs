namespace DOMAIN
{
    public class SaveGame
    {
        public int SaveGameId { get; set; }

        public string Name { get; set; } = default!;

        public string GameObjectJson { get; set; } = default!;
    }

    public class GameConfig
    {
        public int GameConfigId { get; set; }

        public string GameConfigJson { get; set; } = default!;
    }
}