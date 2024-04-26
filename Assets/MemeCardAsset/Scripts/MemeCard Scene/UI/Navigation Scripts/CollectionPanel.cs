using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;
    [Header("Bookmark")]

    public GameObject laughBookmarkActive;
    public GameObject angryBookmarkActive;
    public GameObject susBookmarkActive;
    public GameObject cryBookmarkActive;
    public GameObject surpriseBookmarkActive;
    public GameObject coolBookmarkActive;

    public GameObject laughBookmarkInactive;
    public GameObject angryBookmarkInactive;
    public GameObject susBookmarkInactive;
    public GameObject cryBookmarkInactive;
    public GameObject surpriseBookmarkInactive;
    public GameObject coolBookmarkInactive;

    public int currentItemIndex;
    public enum ActiveBookmark
    {
        Laugh,
        Angry,
        Sus,
        Cry,
        Surprise,
        Cool,
    }

    public ActiveBookmark activeBookmark;

    [Header("Card Prefabs")]
    public GameObject collectionCard;
    public GameObject blankCard;
    public GameObject bookPage;
    public Transform bookTransform;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        EventController.OnTurnCollectionCam();
        //gameObject.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();

        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);
            Image r = background.GetComponent<Image>();
            if (r.color.a <= 0)
            {
                LeanTween.value(background, 0, 1, 1f).setOnUpdate((float val) =>
                {
                    Color c = r.color;
                    c.a = val;
                    r.color = c;
                }).setOnComplete(() =>
                {
                    gameObject.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();
                });
            }
        }
        StartCoroutine(wait());

        //Set default variable for book display
        activeBookmark = ActiveBookmark.Laugh;
        currentItemIndex = 0;

        DisplayFirstPage();
    }

    [Button]
    public void DisplayFirstPage()
    {
        //Set default variable for book display
        currentItemIndex = 0;

        GameObject tempBookPage = Instantiate(bookPage, bookTransform);
        EventController.OnGetCardCollection();

        if (activeBookmark == ActiveBookmark.Laugh)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.laughCardList[i]);
            }
            currentItemIndex += 9;
        }
        else if (activeBookmark == ActiveBookmark.Angry)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.angryCardList[i]);
            }
            currentItemIndex += 9;
        }
        else if (activeBookmark == ActiveBookmark.Sus)
        {

            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.susCardList[i]);
            }
            currentItemIndex += 9;
        }
        else if (activeBookmark == ActiveBookmark.Cry)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.cryCardList[i]);
            }
            currentItemIndex += 9;
        }
        else if (activeBookmark == ActiveBookmark.Surprise)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.surpriseCardList[i]);
            }
            currentItemIndex += 9;
        }
        else if (activeBookmark == ActiveBookmark.Cool)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.coolCardList[i]);
            }
            currentItemIndex += 9;
        }
    }

    [Button]
    public void DisplayNextPage()
    {
        GameObject tempBookPage = Instantiate(bookPage, bookTransform);
        if (activeBookmark == ActiveBookmark.Laugh)
        {
            if (currentItemIndex <= GameManager.laughCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.laughCardList.Count)
                    {
                        print("print");
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.laughCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
        else if (activeBookmark == ActiveBookmark.Angry)
        {
            if (currentItemIndex <= GameManager.angryCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.angryCardList.Count)
                    {
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.angryCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
        else if (activeBookmark == ActiveBookmark.Sus)
        {
            if (currentItemIndex <= GameManager.susCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.susCardList.Count)
                    {
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.susCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
        else if (activeBookmark == ActiveBookmark.Cry)
        {
            if (currentItemIndex <= GameManager.cryCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.cryCardList.Count)
                    {
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.cryCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
        else if (activeBookmark == ActiveBookmark.Surprise)
        {
            if (currentItemIndex <= GameManager.surpriseCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.surpriseCardList.Count)
                    {
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.surpriseCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
        else if (activeBookmark == ActiveBookmark.Cool)
        {
            if (currentItemIndex <= GameManager.coolCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.coolCardList.Count)
                    {
                        GameObject tempCollectionCard = Instantiate(collectionCard, tempBookPage.transform);
                        tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.coolCardList[i]);
                    }
                    else
                    {
                        Instantiate(blankCard, tempBookPage.transform);
                    }
                }
                currentItemIndex += 9;
            }
        }
    }

    public void OnClick_LaughEmotion()
    {
        laughBookmarkActive.SetActive(true);
        angryBookmarkActive.SetActive(false);
        susBookmarkActive.SetActive(false);
        cryBookmarkActive.SetActive(false);
        surpriseBookmarkActive.SetActive(false);
        coolBookmarkActive.SetActive(false);

        laughBookmarkInactive.SetActive(false);
        angryBookmarkInactive.SetActive(true);
        susBookmarkInactive.SetActive(true);
        cryBookmarkInactive.SetActive(true);
        surpriseBookmarkInactive.SetActive(true);
        coolBookmarkInactive.SetActive(true);

        activeBookmark = ActiveBookmark.Laugh;
        DisplayFirstPage();
    }
    public void OnClick_AngryEmotion()
    {
        laughBookmarkActive.SetActive(false);
        angryBookmarkActive.SetActive(true);
        susBookmarkActive.SetActive(false);
        cryBookmarkActive.SetActive(false);
        surpriseBookmarkActive.SetActive(false);
        coolBookmarkActive.SetActive(false);

        laughBookmarkInactive.SetActive(true);
        angryBookmarkInactive.SetActive(false);
        susBookmarkInactive.SetActive(true);
        cryBookmarkInactive.SetActive(true);
        surpriseBookmarkInactive.SetActive(true);
        coolBookmarkInactive.SetActive(true);

        activeBookmark = ActiveBookmark.Angry;
        DisplayFirstPage();
    }
    public void OnClick_SusEmotion()
    {
        laughBookmarkActive.SetActive(false);
        angryBookmarkActive.SetActive(false);
        susBookmarkActive.SetActive(true);
        cryBookmarkActive.SetActive(false);
        surpriseBookmarkActive.SetActive(false);
        coolBookmarkActive.SetActive(false);

        laughBookmarkInactive.SetActive(true);
        angryBookmarkInactive.SetActive(true);
        susBookmarkInactive.SetActive(false);
        cryBookmarkInactive.SetActive(true);
        surpriseBookmarkInactive.SetActive(true);
        coolBookmarkInactive.SetActive(true);

        activeBookmark = ActiveBookmark.Sus;
        DisplayFirstPage();
    }
    public void OnClick_CryEmotion()
    {
        laughBookmarkActive.SetActive(false);
        angryBookmarkActive.SetActive(false);
        susBookmarkActive.SetActive(false);
        cryBookmarkActive.SetActive(true);
        surpriseBookmarkActive.SetActive(false);
        coolBookmarkActive.SetActive(false);

        laughBookmarkInactive.SetActive(true);
        angryBookmarkInactive.SetActive(true);
        susBookmarkInactive.SetActive(true);
        cryBookmarkInactive.SetActive(false);
        surpriseBookmarkInactive.SetActive(true);
        coolBookmarkInactive.SetActive(true);

        activeBookmark = ActiveBookmark.Cry;
        DisplayFirstPage();
    }
    public void OnClick_SurpriseEmotion()
    {
        laughBookmarkActive.SetActive(false);
        angryBookmarkActive.SetActive(false);
        susBookmarkActive.SetActive(false);
        cryBookmarkActive.SetActive(false);
        surpriseBookmarkActive.SetActive(true);
        coolBookmarkActive.SetActive(false);

        laughBookmarkInactive.SetActive(true);
        angryBookmarkInactive.SetActive(true);
        susBookmarkInactive.SetActive(true);
        cryBookmarkInactive.SetActive(true);
        surpriseBookmarkInactive.SetActive(false);
        coolBookmarkInactive.SetActive(true);

        activeBookmark = ActiveBookmark.Surprise;
        DisplayFirstPage();
    }
    public void OnClick_CoolEmotion()
    {
        laughBookmarkActive.SetActive(false);
        angryBookmarkActive.SetActive(false);
        susBookmarkActive.SetActive(false);
        cryBookmarkActive.SetActive(false);
        surpriseBookmarkActive.SetActive(false);
        coolBookmarkActive.SetActive(true);

        laughBookmarkInactive.SetActive(true);
        angryBookmarkInactive.SetActive(true);
        susBookmarkInactive.SetActive(true);
        cryBookmarkInactive.SetActive(true);
        surpriseBookmarkInactive.SetActive(true);
        coolBookmarkInactive.SetActive(false);

        activeBookmark = ActiveBookmark.Cool;
        DisplayFirstPage();
    }
}