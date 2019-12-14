namespace DOMAIN
{
    public class SaveGame
    {
        public int SaveGameId { get; set; }

        public string Name { get; set; } = default!;

        public string GameObjectJson { get; set; } = default!;
    }
}