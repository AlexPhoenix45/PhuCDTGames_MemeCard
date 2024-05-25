using Assets.Core.Scripts.Core.Managers;
using I2.Loc;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;

public class CardPack : MonoBehaviour
{
    private readonly Vector3 cardPackSpawnPos = new Vector3(104.228f, -23.31f, 5.27f);
    private SkinnedMeshRenderer packTearPart;
    private SkinnedMeshRenderer packBodyPart;

    [Header("3D Card Parameters")]
    public GameObject packPrefabs;
    public Material commonMat;
    public Material rareMat;
    public Material epicMat;
    public PlayingCard[] collectedCards;
    public ParticleSystem shiningParticles;

    //Pack Transform must not in Canvas
    public Transform packTransform;

    [Header("2D/UI Card Parameters")]
    public Image background;
    public GameObject glowingFxs;
    public GameObject newCardText;
    public CollectionCard[] uiCard;
    public Button receiveButton;
    public Button morePackButton;
    public GameObject transition;
    //Turn off others panel
    public GameObject navPanel;
    public GameObject headerPanel;

    private int packRariry;
    private GameObject tempPack;
    private List<CardData> collectedCardDatas = new List<CardData>();
    public bool moreEpic;
    public bool bonused;

    private void OnEnable()
    {
        packTearPart = packPrefabs.transform.Find("2").GetComponent<SkinnedMeshRenderer>();
        packBodyPart = packPrefabs.transform.Find("3").GetComponent<SkinnedMeshRenderer>();

        //Call this method from GameManager
        EventController.GeneratePackCards += GenerateCardDataPackage;
        EventController.GetLastCardEvent += ShowNewCard;
        EventController.NextCardCheck += NextCardCheck;
        EventController.OpenPack += OpenPack;
        EventController.SpawnPack += ShowPack;
    }

    public int rarity;
    [Button]
    public void Show()
    {
        ShowPack(rarity, moreEpic, bonused);
    }

    private void ShowPack(int rarity, bool moreEpic, bool bonused)
    {
        EventController.OnTurnDirectTableCam();
        if (!bonused)
        {
            StartCoroutine(waitToTransition());
            IEnumerator waitToTransition()
            {
                transition.SetActive(true);
                yield return new WaitForSeconds(3f);

                foreach (PlayingCard card in collectedCards)
                {
                    card.gameObject.SetActive(true);
                    card.hasClick = true;
                }

                EventController.OnGetCardData();

                transition.SetActive(false);
            }
            //Reset card data pack
            collectedCardDatas = new List<CardData>();
            this.moreEpic = moreEpic;
            this.bonused = bonused;

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

            shiningParticles.gameObject.SetActive(true);

            if (navPanel.activeSelf && headerPanel.activeSelf)
            {
                LeanTween.value(1, 0, 1f).setOnStart(() =>
                {
                    navPanel.gameObject.SetActive(true);
                    headerPanel.gameObject.SetActive(true);

                    navPanel.GetComponent<CanvasGroup>().alpha = 1;
                    headerPanel.GetComponent<CanvasGroup>().alpha = 1;
                }).setOnUpdate((float value) =>
                {
                    navPanel.GetComponent<CanvasGroup>().alpha = value;
                    headerPanel.GetComponent<CanvasGroup>().alpha = value;
                }).setOnComplete(() =>
                {
                    navPanel.GetComponent<CanvasGroup>().alpha = 0;
                    headerPanel.GetComponent<CanvasGroup>().alpha = 0;

                    navPanel.gameObject.SetActive(false);
                    headerPanel.gameObject.SetActive(true);
                });
            }
        }
        else
        {
            StartCoroutine(waitToTransition());
            IEnumerator waitToTransition()
            {
                transition.SetActive(true);

                yield return new WaitForSeconds(2);
                foreach (PlayingCard card in collectedCards)
                {
                    card.gameObject.SetActive(true);
                    card.hasClick = true;
                }
                //Reset card data pack
                collectedCardDatas = new List<CardData>();
                this.moreEpic = moreEpic;
                this.bonused = bonused;

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

                shiningParticles.gameObject.SetActive(true);

                if (navPanel.activeSelf && headerPanel.activeSelf)
                {
                    LeanTween.value(1, 0, 1f).setOnStart(() =>
                    {
                        navPanel.gameObject.SetActive(true);
                        headerPanel.gameObject.SetActive(true);

                        navPanel.GetComponent<CanvasGroup>().alpha = 1;
                        headerPanel.GetComponent<CanvasGroup>().alpha = 1;
                    }).setOnUpdate((float value) =>
                    {
                        navPanel.GetComponent<CanvasGroup>().alpha = value;
                        headerPanel.GetComponent<CanvasGroup>().alpha = value;
                    }).setOnComplete(() =>
                    {
                        navPanel.GetComponent<CanvasGroup>().alpha = 0;
                        headerPanel.GetComponent<CanvasGroup>().alpha = 0;

                        navPanel.gameObject.SetActive(false);
                        headerPanel.gameObject.SetActive(true);
                    });
                }

                EventController.OnGetCardData();

                yield return new WaitForSeconds(1f);
                transition.SetActive(false);
            }
        }
    }

    [Button]
    public void OpenPack()
    {
        tempPack.GetComponent<Animator>().SetTrigger("OpenPack");

        tempPack.GetComponent<Collider>().enabled = false;

        //Destroy pack after a while
        StartCoroutine(waitToDestroy());
        IEnumerator waitToDestroy()
        {
            yield return new WaitForSeconds(1);
            foreach (PlayingCard card in collectedCards)
            {
                card.hasClick = false;
            }
            yield return new WaitForSeconds(2.5f);
            Destroy(tempPack);
        }
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

            //Sorting card (common - rare - epic)
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
        shiningParticles.gameObject.SetActive(false);
        glowingFxs.gameObject.SetActive(true);

        //Background Animation (alpha 0 - 1)
        LeanTween.value(0, 1, 1f).setOnStart(() =>
        {
            background.color = new Vector4(1, 1, 1, 0);
            background.gameObject.SetActive(true);
        }).setOnUpdate((float value) =>
        {
            background.color = new Vector4(1, 1, 1, value);
        }).setOnComplete(() =>
        {
            background.color = new Vector4(1, 1, 1, 1);
        });

        //New Card Text Animation
        LeanTween.value(0, 1, 1f).setOnStart(() =>
        {
            newCardText.transform.localScale = Vector3.zero;
            newCardText.SetActive(true);
        }).setOnUpdate((float value) =>
        {
            newCardText.transform.localScale = new Vector3(value, value, value);
        }).setOnComplete(() =>
        {
            newCardText.transform.localScale = Vector3.one;
        }).setEaseInBack();

        StartCoroutine(ShowCard());
        IEnumerator ShowCard()
        {
            for (int i = 0; i < 6; i++)
            {
                LeanTween.value(0, 1.5f, .25f).setOnStart(() =>
                {
                    uiCard[i].gameObject.transform.localScale = Vector3.zero;
                    uiCard[i].gameObject.SetActive(true);
                    uiCard[i].SetCollectionCard(collectedCardDatas[i]);
                }).setOnUpdate((float value) =>
                {
                    uiCard[i].gameObject.transform.localScale = new Vector3(value, value, value);
                }).setOnComplete(() =>
                {
                    uiCard[i].gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                });
                yield return new WaitForSeconds(.25f);
            }
            StartCoroutine(ShowButton());
        }

        IEnumerator ShowButton()
        {
            yield return new WaitForSeconds(1.5f);

            bool morePack = false;

            if (bonused)
            {
                morePack = false;
            }
            else
            {
                morePack = true;
            }

            //Collect Button Animation (alpha 0 - 1)
            LeanTween.value(0, 1, 1f).setOnStart(() =>
            {
                receiveButton.gameObject.SetActive(true);
                receiveButton.GetComponent<CanvasGroup>().alpha = 0f;
                if (morePack || moreEpic)
                {
                    morePackButton.gameObject.SetActive(true);
                    morePackButton.GetComponent<CanvasGroup>().alpha = 0f;
                }
            }).setOnUpdate((float value) =>
            {
                receiveButton.GetComponent<CanvasGroup>().alpha = value;
                if (morePack || moreEpic)
                {
                    morePackButton.GetComponent<CanvasGroup>().alpha = value;
                }
            }).setOnComplete(() =>
            {
                receiveButton.GetComponent<CanvasGroup>().alpha = 1f;

                if (morePack || moreEpic)
                {
                    morePackButton.GetComponent<CanvasGroup>().alpha = 1f;
                }
            });
        }
    }

    private void NextCardCheck(CardData currentCardData)
    {
        int tempIndex = -1;
        for (int i = 0; i < 6; i++)
        {
            if (collectedCards[i].cardData == currentCardData && i != 5)
            {
                tempIndex = i;
                if (collectedCards[i + 1].cardData.rarityType == RarityType.Epic)
                {
                    EventController.OnSFXPlay_EpicPack();
                }
            }
        }
    }

    public void OnClick_ReceiveButton()
    {
        glowingFxs.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        newCardText.gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            uiCard[i].gameObject.SetActive(false);
        }
        receiveButton.gameObject.SetActive(false);
        morePackButton.gameObject.SetActive(false);

        LeanTween.value(0, 1, .25f).setOnStart(() =>
        {
            navPanel.gameObject.SetActive(true);
            headerPanel.gameObject.SetActive(true);

            navPanel.GetComponent<CanvasGroup>().alpha = 0;
            headerPanel.GetComponent<CanvasGroup>().alpha = 0;
        }).setOnUpdate((float value) =>
        {
            navPanel.GetComponent<CanvasGroup>().alpha = value;
            headerPanel.GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(() =>
        {
            navPanel.GetComponent<CanvasGroup>().alpha = 1;
            headerPanel.GetComponent<CanvasGroup>().alpha = 1;
        });

        EventController.OnTurnRoomCam();
        EventController.OnShowNavButtons();
    }

    public void OnClick_BonusPack()
    {
        ServiceManager.ShowReward((bool watched) =>
        {
            if (watched)
            {
                glowingFxs.gameObject.SetActive(false);
                background.gameObject.SetActive(false);
                newCardText.gameObject.SetActive(false);
                for (int i = 0; i < 6; i++)
                {
                    uiCard[i].gameObject.SetActive(false);
                }
                receiveButton.gameObject.SetActive(false);
                morePackButton.gameObject.SetActive(false);
                if (moreEpic)
                {
                    ShowPack(2, false, true);
                }
                else
                {
                    ShowPack(packRariry, false, true);
                }
            }
            else
            {
                return;
            }
        });
    }
}
