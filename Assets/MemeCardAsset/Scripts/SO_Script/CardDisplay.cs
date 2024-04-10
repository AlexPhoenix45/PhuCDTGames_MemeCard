using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public Image cardType_Color;

    public void ShowCard()
    {
        cardImage.sprite = cardData.sprite;

        if (cardData.cardType == CardType.Common)
        {
            cardType_Color.color = new Color(0.6886792f, 0.6886792f, 0.6886792f, 1);
        }
        else if (cardData.cardType == CardType.Uncommon)
        {
            cardType_Color.color = new Color(0.1529412f, 0.682353f, 0.3764706f, 1);
        }
        else if (cardData.cardType == CardType.Rare)
        {
            cardType_Color.color = new Color(0.1411765f, 0.4431373f, 0.6392157f, 1);
        }
        else if (cardData.cardType == CardType.Epic)
        {
            cardType_Color.color = new Color(0.4901961f, 0.2352941f, 0.5960785f, 1);
        }
        else if (cardData.cardType == CardType.Legendary)
        {
            cardType_Color.color = new Color(0.945098f, 0.7686275f, 0.05882353f, 1);
        }
    }
}
