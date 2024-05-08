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
    [Header("Image/Gif")]
    public GameObject memeImageContainer;
    public Image memeImage;
    public VideoPlayer memeGifVideoPlayer;

    [Header("Upgrade")]
    //Piece Upgrade
    public GameObject pieceUpgradePanel;
    public GameObject pieceLeft;
    public TextMeshProUGUI pieceText;
    public TextMeshProUGUI moneyText;
    //Ads Upgrade
    public GameObject adsUpgradePanel;
    public RenderTexture renderTexture;
    public CardData cardData;


    public bool isForPackage = false;
    public void SetCollectionCard(CardData cardData)
    {
        this.cardData = cardData;
        if (!isForPackage)
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
                    memeImageContainer.SetActive(true);
                    memeImage.sprite = cardData.memeImage;
                    memeGifVideoPlayer.gameObject.SetActive(false);
                }
                else if (cardData.rarityType == RarityType.Rare)
                {
                    commonCard.SetActive(false);
                    rareCard.SetActive(true);
                    epicCard.SetActive(false);
                    commonEmotion.SetActive(false);
                    rareEmotion.SetActive(true);
                    epicEmotion.SetActive(false);
                    memeImageContainer.SetActive(true);
                    memeImage.sprite = cardData.memeImage;
                    memeGifVideoPlayer.gameObject.SetActive(false);
                }
                else if (cardData.rarityType == RarityType.Epic)
                {
                    commonCard.SetActive(false);
                    rareCard.SetActive(false);
                    epicCard.SetActive(true);
                    commonEmotion.SetActive(false);
                    rareEmotion.SetActive(false);
                    epicEmotion.SetActive(true);

                    if (cardData.memeGif != null)
                    {
                        memeImageContainer.SetActive(false);
                        memeGifVideoPlayer.gameObject.SetActive(true);
                        memeGifVideoPlayer.clip = cardData.memeGif;
                        RenderTexture rt = Instantiate(renderTexture, memeGifVideoPlayer.transform);
                        memeGifVideoPlayer.GetComponent<RawImage>().texture = rt;
                        memeGifVideoPlayer.targetTexture = rt;
                    }
                    else
                    {
                        memeImageContainer.SetActive(true);
                        memeImage.sprite = cardData.memeImage;
                        memeGifVideoPlayer.gameObject.SetActive(false);
                    }
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
        else
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
                    memeImageContainer.SetActive(true);
                    memeImage.sprite = cardData.memeImage;
                    memeGifVideoPlayer.gameObject.SetActive(false);
                }
                else if (cardData.rarityType == RarityType.Rare)
                {
                    commonCard.SetActive(false);
                    rareCard.SetActive(true);
                    epicCard.SetActive(false);
                    commonEmotion.SetActive(false);
                    rareEmotion.SetActive(true);
                    epicEmotion.SetActive(false);
                    memeImageContainer.SetActive(true);
                    memeImage.sprite = cardData.memeImage;
                    memeGifVideoPlayer.gameObject.SetActive(false);
                }
                else if (cardData.rarityType == RarityType.Epic)
                {
                    commonCard.SetActive(false);
                    rareCard.SetActive(false);
                    epicCard.SetActive(true);
                    commonEmotion.SetActive(false);
                    rareEmotion.SetActive(false);
                    epicEmotion.SetActive(true);

                    if (cardData.memeGif != null)
                    {
                        memeImageContainer.SetActive(false);
                        memeGifVideoPlayer.gameObject.SetActive(true);
                        memeGifVideoPlayer.clip = cardData.memeGif;
                        RenderTexture rt = Instantiate(renderTexture, memeGifVideoPlayer.transform);
                        memeGifVideoPlayer.GetComponent<RawImage>().texture = rt;
                        memeGifVideoPlayer.targetTexture = rt;
                    }
                    else
                    {
                        memeImageContainer.SetActive(true);
                        memeImage.sprite = cardData.memeImage;
                        memeGifVideoPlayer.gameObject.SetActive(false);
                    }
                }
            }

        }
    }

    public void OnClick_UpgradeButton()
    {
        print("UPGRADE CLICKED");
    }
}
