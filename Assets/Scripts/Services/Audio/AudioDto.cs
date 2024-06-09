using Mario.Services.SaveData;

namespace Mario.Services
{
    public class AudioDto: ISerializedDto
    {
        public AudioSettings SoundSettings { get; private set; }
        public AudioSettings MusicSettings { get; private set; }
        public string Id { get; }
        
        public AudioDto(string id, AudioSettings soundSettings, AudioSettings musicSettings)
        {
            Id = id;
            SoundSettings = soundSettings;
            MusicSettings = musicSettings;
        }
    }
}