using LightWeightFramework.Components.Service;
using Newtonsoft.Json;
using UnityEngine;

namespace Mario.Services.SaveData
{
    public interface ISaveDataService: IService
    {
        void Save<TSerializedDto>(TSerializedDto serializedDto) where TSerializedDto : ISerializedDto;
        TSerializedDto Get<TSerializedDto>(string id) where TSerializedDto : ISerializedDto;
        bool HasKey(string saveDataKey);
    }
    
    public class SaveDataService: Service, ISaveDataService
    {
        public void Save<TSerializedDto>(TSerializedDto serializedDto) where TSerializedDto : ISerializedDto
        {
            string json = JsonConvert.SerializeObject(serializedDto);
            PlayerPrefs.SetString(serializedDto.Id, json);
            PlayerPrefs.Save();
        }

        public TSerializedDto Get<TSerializedDto>(string id)where TSerializedDto : ISerializedDto
        {
            string json = PlayerPrefs.GetString(id);
            TSerializedDto serializedDto = JsonConvert.DeserializeObject<TSerializedDto>(json);
            return serializedDto;
        }

        public bool HasKey(string saveDataKey)
        {
            return PlayerPrefs.HasKey(saveDataKey);
        }
    }
}