using NaughtyAttributes;
using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataStorage : MonoBehaviour
{
    public PlayerData data = new PlayerData();
    public string fileName;

    private void Start()
    {
        EventController.SavePlayerData += SaveToJson;
        EventController.LoadPlayerData += LoadFromJson;
    }

    [Button]
    public void SaveToJson()
    {
        string playerData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/" + fileName;
        print(filePath);
        System.IO.File.WriteAllText(filePath, playerData);
        print("Saving Complete!");
    }

    [Button]
    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        string playerData = System.IO.File.ReadAllText(filePath);

        data = JsonUtility.FromJson<PlayerData>(playerData);
        print("Load Complete!");
        EventController.OnLoadLevelSlider(data);
    }

    private void OnApplicationQuit()
    {
        EventController.SavePlayerData -= SaveToJson;
        EventController.LoadPlayerData -= LoadFromJson;
    }
}

[System.Serializable]
public class PlayerData
{
    public int currentMoney;
    public int currentLvl;
    public List<CardOwned> cardOwned = new List<CardOwned>();
}

[System.Serializable]
public class CardOwned
{
    public string cardName;
    public int cardRarity;
}