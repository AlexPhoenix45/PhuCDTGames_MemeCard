using NaughtyAttributes;
using NaughtyAttributes.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataStorage : MonoBehaviour
{
    public PlayerData data = new PlayerData();
    public string fileName;
    public static PlayerDataStorage Instance;
    public List<CardData> playerOwnedCard = new List<CardData>();

    private void Start()
    {
        EventController.SavePlayerData += SaveToJson;
        EventController.LoadPlayerData += LoadFromJson;

        //Player Coins
        EventController.AddPlayerCoin += AddPlayerCoin;
        EventController.AddPlayerLevel += AddPlayerLevel;

        if (Instance == null)
        {
            Instance = this;
        }
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
        EventController.OnSetLevelSlider(data);
        EventController.OnSetPlayerCoin(data);
        EventController.OnLoadPlayerOwnedCard();
    }

    public void AddPlayerCoin(int amount)
    {
        data.currentCoin += amount;
        SaveToJson();
    }

    public void AddPlayerLevel (int amount)
    {
        data.currentLvl += amount;
        SaveToJson();
    }

    private void OnApplicationQuit()
    {
        EventController.SavePlayerData -= SaveToJson;
        EventController.LoadPlayerData -= LoadFromJson;

        //Player Coins
        EventController.AddPlayerCoin -= AddPlayerCoin;
        EventController.AddPlayerLevel -= AddPlayerLevel;
    }
}

[System.Serializable]
public class PlayerData
{
    public int currentCoin;
    public int currentLvl;
    public List<CardOwned> cardOwned = new List<CardOwned>();
}

[System.Serializable]
public class CardOwned
{
    public string cardName;
    public int cardRarity;
}