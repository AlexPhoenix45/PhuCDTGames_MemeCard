using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CollectionCard : MonoBehaviour
{
    [Header("Rarity")]
    public GameObject commonCard;
    public GameObject rareCard;
    public GameObject epicCard;

    [Header("Emotions")]
    public GameObject commonEmotion;
    public GameObject rareEmotion;
    public GameObject epicEmotion;

    public GameObject susEmotion;
    public GameObject cryEmotion;
    public GameObject angryEmotion;
    public GameObject surpriseEmotion;
    public GameObject coolEmotion;
    public GameObject laughEmotion;
    [Header("Image")]
    public Image memeImage;

    [Header("Upgrade")]
    //Piece Upgrade
    public GameObject pieceUpgradePanel;
    public GameObject pieceLeft;
    public TextMeshProUGUI pieceText;
    public TextMeshProUGUI moneyText;
    //Ads Upgrade
    public GameObject adsUpgradePanel;

    public void SetCollectionCard(CardData cardData)
    {
        //Rarity Filter
        {
            if (cardData.rarityType == RarityType.Common)
            {
                commonCard.SetActive(true);
                rareCard.SetActive(false);
                epicCard.SetActive(false);
                commonEmotion.SetActive(true);
                rareEmotion.SetActive(false);
                epicEmotion.SetActive(false);
                memeImage.sprite = cardData.memeImage;
                memeImage.GetComponent<Image>().enabled = true;
                memeImage.GetComponent<VideoPlayer>().enabled = false;
            }
            else if (cardData.rarityType == RarityType.Rare)
            {
                commonCard.SetActive(false);
                rareCard.SetActive(true);
                epicCard.SetActive(false);
                commonEmotion.SetActive(false);
                rareEmotion.SetActive(true);
                epicEmotion.SetActive(false);
                memeImage.sprite = cardData.memeImage;
                memeImage.GetComponent<Image>().enabled = true;
                memeImage.GetComponent<VideoPlayer>().enabled = false;
            }
            else if (cardData.rarityType == RarityType.Epic)
            {
                commonCard.SetActive(false);
                rareCard.SetActive(false);
                epicCard.SetActive(true);
                commonEmotion.SetActive(false);
                rareEmotion.SetActive(false);
                epicEmotion.SetActive(true);
                memeImage.GetComponent<Image>().enabled = false;
                memeImage.GetComponent<VideoPlayer>().enabled = true;
                memeImage.GetComponent<VideoPlayer>().clip = cardData.memeGif;
            }
        }

        //Emotion Filter
        {
            if (cardData.playingCardEmotionalType == EmotionalType.Laugh)
            {
                susEmotion.SetActive(false);
                cryEmotion.SetActive(false);
                angryEmotion.SetActive(false);
                surpriseEmotion.SetActive(false);
                coolEmotion.SetActive(false);
                laughEmotion.SetActive(true);
            }
            else if (cardData.playingCardEmotionalType == EmotionalType.Sus)
            {
                susEmotion.SetActive(true);
                cryEmotion.SetActive(false);
                angryEmotion.SetActive(false);
                surpriseEmotion.SetActive(false);
                coolEmotion.SetActive(false);
                laughEmotion.SetActive(false);
            }
            else if (cardData.playingCardEmotionalType == EmotionalType.Cry)
            {
                susEmotion.SetActive(false);
                cryEmotion.SetActive(true);
                angryEmotion.SetActive(false);
                surpriseEmotion.SetActive(false);
                coolEmotion.SetActive(false);
                laughEmotion.SetActive(false);
            }
            else if (cardData.playingCardEmotionalType == EmotionalType.Angry)
            {
                susEmotion.SetActive(false);
                cryEmotion.SetActive(false);
                angryEmotion.SetActive(true);
                surpriseEmotion.SetActive(false);
                coolEmotion.SetActive(false);
                laughEmotion.SetActive(false);
            }
            else if (cardData.playingCardEmotionalType == EmotionalType.Surprise)
            {
                susEmotion.SetActive(false);
                cryEmotion.SetActive(false);
                angryEmotion.SetActive(false);
                surpriseEmotion.SetActive(true);
                coolEmotion.SetActive(false);
                laughEmotion.SetActive(false);
            }
            else if (cardData.playingCardEmotionalType == EmotionalType.Cool)
            {
                susEmotion.SetActive(false);
                cryEmotion.SetActive(false);
                angryEmotion.SetActive(false);
                surpriseEmotion.SetActive(false);
                coolEmotion.SetActive(true);
                laughEmotion.SetActive(false);
            }
        }

    }
}
