using Mario.Services.SaveData;

namespace Mario.Entities.Player
{
    public class PlayerDto: ISerializedDto
    {
        public string Id { get; private set; }
        public float Distance { get; private set; }
        public float RespawnCount { get; private set; }
        
        public PlayerDto(string id, float respawnCount, float distance)
        {
            Id = id;
            RespawnCount = respawnCount;
            Distance = distance;
        }
    }
}