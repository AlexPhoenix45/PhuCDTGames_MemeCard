using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data")]
public class Card : ScriptableObject
{
    public Sprite sprite;
    public CardType cardType;
    public EmotionalType emotionalType;
}

public enum CardType
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
}

public enum EmotionalType
{
    Embarrassment, 
    Amusement, 
    Surprise, 
    Frustration, 
    Fear
}