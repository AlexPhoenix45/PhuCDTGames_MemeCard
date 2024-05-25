using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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

    private GameObject currentPage;
    private int currentLayer = 9999;

    public bool isFirstTime = true;


    #region Drag Controller Variables
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    #endregion

    private void Awake()
    {
        gameObject.transform.localScale = Vector3.zero; //Need to change this after complete book pages
        currentLayer = 9999;
    }
    private void OnEnable()
    {
        EventController.OnTurnCollectionCam();
        //gameObject.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();

        Image r = background.GetComponent<Image>();
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        if (r.color.a <= 0)
        {
            UIManager.tweeningID = LeanTween.value(background, 0, 1, 1f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
                gameObject.GetComponent<CanvasGroup>().alpha = val;
            }).setOnComplete(() =>
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }).id;
        }
        else
        {
            UIManager.tweeningID = LeanTween.value(0, 1, 1f).setOnUpdate((float val) =>
            {
                gameObject.GetComponent<CanvasGroup>().alpha = val;
            }).setOnComplete(() =>
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }).id;
        }

        //Set default variable for book display
        activeBookmark = ActiveBookmark.Laugh;
        currentItemIndex = 0;
        isFirstTime = true;

        DisplayFirstPage();
    }

    private void OnDisable()
    {
        Destroy(currentPage);
        EventController.OnTurnRoomCam();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if (endTouchPos.x < startTouchPos.x)
            {
                DisplayNextPage();
            }
            else if (endTouchPos.x > startTouchPos.x)
            {
                DisplayPrevPage();
            }
        }
    }

    [Button]
    public void DisplayFirstPage()
    {
        //Set default variable for book display
        currentItemIndex = 0;
        EventController.OnGenerateCardDataPackage();

        if (isFirstTime)
        {
            currentPage = Instantiate(bookPage, bookTransform);
            currentPage.GetComponent<Canvas>().sortingOrder = currentLayer;
            currentLayer--;
            isFirstTime = false;


            if (activeBookmark == ActiveBookmark.Laugh)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                    tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.laughCardList[i]);
                }
                currentItemIndex += 9;
            }
            else if (activeBookmark == ActiveBookmark.Angry)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                    tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.angryCardList[i]);
                }
                currentItemIndex += 9;
            }
            else if (activeBookmark == ActiveBookmark.Sus)
            {

                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                    tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.susCardList[i]);
                }
                currentItemIndex += 9;
            }
            else if (activeBookmark == ActiveBookmark.Cry)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                    tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.cryCardList[i]);
                }
                currentItemIndex += 9;
            }
            else if (activeBookmark == ActiveBookmark.Surprise)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                    tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.surpriseCardList[i]);
                }
                currentItemIndex += 9;
            }
            else if (activeBookmark == ActiveBookmark.Cool)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                GameObject tempCollectionCard = Instantiate(collectionCard, currentPage.transform);
                tempCollectionCard.GetComponent<CollectionCard>().SetCollectionCard(GameManager.coolCardList[i]);
            }
            currentItemIndex += 9;
        }

        }
        else
        {
            GameObject tempBookPage = Instantiate(bookPage, bookTransform);
            PageFlip(false, tempBookPage);


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
    }

    [Button]
    public void DisplayNextPage()
    {
        if (activeBookmark == ActiveBookmark.Laugh)
        {
            if (currentItemIndex > GameManager.laughCardList.Count)
            {
                return;
            }
        }
        else if (activeBookmark == ActiveBookmark.Angry)
        {
            if (currentItemIndex > GameManager.laughCardList.Count)
            {
                return;
            }
        }
        else if (activeBookmark == ActiveBookmark.Sus)
        {
            if (currentItemIndex > GameManager.laughCardList.Count)
            {
                return;
            }
        }
        else if (activeBookmark == ActiveBookmark.Cry)
        {
            if (currentItemIndex > GameManager.cryCardList.Count)
            {
                return;
            }
        }
        else if (activeBookmark == ActiveBookmark.Surprise)
        {
            if (currentItemIndex > GameManager.surpriseCardList.Count)
            {
                return;
            }
        }
        else if (activeBookmark == ActiveBookmark.Cool)
        {
            if (currentItemIndex > GameManager.coolCardList.Count)
            {
                return;
            }
        }

        GameObject tempBookPage = Instantiate(bookPage, bookTransform);
        PageFlip(true, tempBookPage);

        if (activeBookmark == ActiveBookmark.Laugh)
        {
            if (currentItemIndex <= GameManager.laughCardList.Count)
            {
                for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
                {
                    if (i < GameManager.laughCardList.Count)
                    {
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

    [Button]
    public void DisplayPrevPage()
    {
        if (currentItemIndex == 9)
        {
            return;
        }
        
        GameObject tempBookPage = Instantiate(bookPage, bookTransform);
        PageFlip(false, tempBookPage);

        currentItemIndex -= 18;
        if (activeBookmark == ActiveBookmark.Laugh)
        {
            for (int i = currentItemIndex; i < currentItemIndex + 9; i++)
            {
                if (i < GameManager.laughCardList.Count)
                {
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
        else if (activeBookmark == ActiveBookmark.Angry)
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
        else if (activeBookmark == ActiveBookmark.Sus)
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
        else if (activeBookmark == ActiveBookmark.Cry)
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
        else if (activeBookmark == ActiveBookmark.Surprise)
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
        else if (activeBookmark == ActiveBookmark.Cool)
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

    public void PageFlip(bool toNextPage, GameObject tempPage)
    {
        if (toNextPage)
        {
            tempPage.GetComponent<Canvas>().sortingOrder = currentLayer;
            currentLayer--;
            currentPage.transform.rotation = Quaternion.Euler(currentPage.transform.rotation.x, 0, currentPage.transform.rotation.z);
            UIManager.tweeningID = LeanTween.value(0, -90, 1f).setOnUpdate((float value) =>
            {
                currentPage.transform.rotation = Quaternion.Euler(currentPage.transform.rotation.x, value, currentPage.transform.rotation.z);
            }).setOnComplete(() =>
            {
                Destroy(currentPage);
                currentPage = tempPage;
            }).id;
        }
        else
        {
            currentLayer += 2;
            tempPage.GetComponent<Canvas>().sortingOrder = currentLayer;
            currentLayer--;
            tempPage.transform.rotation = Quaternion.Euler(tempPage.transform.rotation.x, 90, tempPage.transform.rotation.z);
            UIManager.tweeningID = LeanTween.value(90, 0, 1f).setOnUpdate((float value) =>
            {
                tempPage.transform.rotation = Quaternion.Euler(tempPage.transform.rotation.x, value, tempPage.transform.rotation.z);
            }).setOnComplete(() =>
            {
                Destroy(currentPage);
                currentPage = tempPage;
            }).id;
        }
    }

    #region Bookmarks OnClick 
    public void OnClick_LaughEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Laugh)
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
    }
    public void OnClick_AngryEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Angry)
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
    }
    public void OnClick_SusEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Sus)
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
    }
    public void OnClick_CryEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Cry)
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
    }
    public void OnClick_SurpriseEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Surprise)
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
    }
    public void OnClick_CoolEmotion()
    {
        EventController.OnSFXPlay_ButtonClick();

        if (activeBookmark != ActiveBookmark.Cool)
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
    #endregion
}