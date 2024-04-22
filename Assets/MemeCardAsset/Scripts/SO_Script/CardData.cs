using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data")]
public class CardData : ScriptableObject
{
    public Material memeImage;
    public VideoClip memeGif;
    public CardType cardType;
    public EmotionalType playingCardEmotionalType;
}

public enum CardType
{
    Common,
    Rare,
    Epic,
}

public enum EmotionalType
{
    Laugh,
    Cool,
    Suprise,
    Angry,
    Cry,
    Sus,
}