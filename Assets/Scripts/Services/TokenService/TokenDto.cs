using Mario.Services.SaveData;

namespace Mario.Services.TokenService
{
    public class TokenDto: ISerializedDto
    {
        public int TokenAmount { get; private set; }
        public string Id { get; }
        public TokenDto(string id, int tokenAmount)
        {
            Id = id;
            TokenAmount = tokenAmount;
        }
    }
}