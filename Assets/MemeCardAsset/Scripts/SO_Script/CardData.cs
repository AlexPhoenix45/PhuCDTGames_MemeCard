using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data")]
public class CardData : ScriptableObject
{
    public Material memeMaterial;
    public Sprite memeImage;
    public VideoClip memeGif;
    public RarityType rarityType;
    public EmotionalType playingCardEmotionalType;
}

public enum RarityType
{
    Common,
    Rare,
    Epic,
}

public enum EmotionalType
{
    Laugh,
    Cool,
    Surprise,
    Angry,
    Cry,
    Sus,
}