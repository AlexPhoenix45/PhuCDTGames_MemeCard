using I2.Loc;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardPack : MonoBehaviour
{
    private readonly Vector3 cardPackSpawnPos = new Vector3(104.228f, -23.31f, 5.27f);
    private SkinnedMeshRenderer packTearPart;
    private SkinnedMeshRenderer packBodyPart;

    public GameObject packPrefabs;
    public Material commonMat;
    public Material rareMat;
    public Material epicMat;
    public PlayingCard[] collectedCards;
    public ParticleSystem shiningParticles;

    //Pack Transform must not in Canvas
    public Transform packTransform;

    private int packRariry;
    private GameObject tempPack;
    private List<CardData> collectedCardDatas = new List<CardData>();

    private void OnEnable()
    {
        EventController.OnTurnDirectTableCam();
        packTearPart = packPrefabs.transform.Find("2").GetComponent<SkinnedMeshRenderer>();
        packBodyPart = packPrefabs.transform.Find("3").GetComponent<SkinnedMeshRenderer>();

        //Call this method from GameManager
        EventController.GeneratePackCards += GenerateCardDataPackage;
        EventController.GetLastCardEvent += ShowNewCard;
    }

    public int rarity;
    [Button]
    public void Show()
    {
        ShowPack(rarity);
    }

    private void ShowPack(int rarity)
    {
        //Reset card data pack
        collectedCardDatas = new List<CardData>();

        packRariry = rarity;
        if (rarity == 0)
        {
            packTearPart.material = commonMat;
            packBodyPart.material = commonMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, packTransform);
        }
        else if (rarity == 1)
        {
            packTearPart.material = rareMat;
            packBodyPart.material = rareMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, packTransform);
        }
        else if (rarity == 2)
        {
            packTearPart.material = epicMat;
            packBodyPart.material = epicMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, packTransform);
        }

        foreach (PlayingCard card in collectedCards)
        {
            card.gameObject.SetActive(true);
        }
        shiningParticles.gameObject.SetActive(true);
        EventController.OnGetCardData();

        //Destroy pack after a while
        StartCoroutine(waitToDestroy());
        IEnumerator waitToDestroy()
        {
            yield return new WaitForSeconds(3.5f);
            Destroy(tempPack);
        }
    }
    
    [Button]
    public void OpenPack()
    {
        tempPack.GetComponent<Animator>().SetTrigger("OpenPack");
    }


    /// <summary>
    /// Generate 6 cards in a pack
    /// </summary>
    /// <param name="cardData">Pass in game card data</param>
    private void GenerateCardDataPackage(CardData[] cardData)
    {
        for (int i = 0; i < 6; i++)
        {
            int random = UnityEngine.Random.Range(0, 100);

            if (packRariry == 0) //Common Pack
            {
                if (random >= 0 && random < 80)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Common);
                    collectedCardDatas.Add(tempCard);
                }
                else if (random >= 80 && random < 95)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Rare);
                    collectedCardDatas.Add(tempCard);
                }
                else
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Epic);
                    collectedCardDatas.Add(tempCard);
                }
            }
            else if (packRariry == 1)
            {

                if (random >= 0 && random < 50)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Common);
                    collectedCardDatas.Add(tempCard);
                }
                else if (random >= 50 && random < 90)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Rare);
                    collectedCardDatas.Add(tempCard);
                }
                else
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Epic);
                    collectedCardDatas.Add(tempCard);
                }
            }
            else if (packRariry == 2)
            {

                if (random >= 0 && random < 40)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Common);
                    collectedCardDatas.Add(tempCard);
                }
                else if (random >= 40 && random < 80)
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Rare);
                    collectedCardDatas.Add(tempCard);
                }
                else
                {
                    CardData tempCard;
                    do
                    {
                        tempCard = cardData[UnityEngine.Random.Range(0, cardData.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Epic);
                    collectedCardDatas.Add(tempCard);
                }
            }
        }

        int tempIndex = 0;
        //Set common card
        foreach (CardData cardDatas in collectedCardDatas)
        {
            if (cardDatas.rarityType == RarityType.Common)
            {
                collectedCards[tempIndex].SetCard(cardDatas);
                tempIndex++;

                if (tempIndex == 5)
                {
                    collectedCards[tempIndex].isLastItem = true;
                }
            }
        }

        //Set rare card
        foreach (CardData cardDatas in collectedCardDatas)
        {
            if (cardDatas.rarityType == RarityType.Rare)
            {
                collectedCards[tempIndex].SetCard(cardDatas);
                tempIndex++;

                if (tempIndex == 5)
                {
                    collectedCards[tempIndex].isLastItem = true;
                }
            }
        }

        //Set epic card
        foreach (CardData cardDatas in collectedCardDatas)
        {
            if (cardDatas.rarityType == RarityType.Epic)
            {
                collectedCards[tempIndex].SetCard(cardDatas);
                tempIndex++;

                if (tempIndex == 5)
                {
                    collectedCards[tempIndex].isLastItem = true;
                }
            }
        }
    }

    /// <summary>
    /// Show UI New Card
    /// </summary>
    private void ShowNewCard()
    {

    }
}
