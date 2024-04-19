using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    #region Navigation Attributes
    [Header("Button")]
    [Header("Navigation")]
    public RectTransform navShowPos;
    public RectTransform navHidePos;
    public GameObject navigationButtons;

    public Button shopBtn;
    public Button collectionBtn;
    public Button playBtn;
    public Button missionBtn;
    public Button settingBtn;

    public GameObject shopBtn_Selected_Background; 
    public GameObject collectionBtn_Selected_Background;
    public GameObject playBtn_Selected_Background;
    public GameObject missionBtn_Selected_Background;
    public GameObject settingBtn_Selected_Background;

    public GameObject shopBtn_Selected_Icon;
    public GameObject collectionBtn_Selected_Icon;
    public GameObject playBtn_Selected_Icon;
    public GameObject missionBtn_Selected_Icon;
    public GameObject settingBtn_Selected_Icon;

    private bool shopBtnSelected_Toggle = false;
    private bool collectionBtnSelected_Toggle = false;
    private bool playBtnSelected_Toggle = true;
    private bool missionBtnSelected_Toggle = false;
    private bool settingBtnSelected_Toggle = false;

    [Header("Panel")]
    public GameObject shopPnl;
    public GameObject collectionPnl;
    public GameObject playPnl;
    public GameObject missionPnl;
    public GameObject settingPnl;
    #endregion

    #region Question Attributes
    [Header("Question Attributes")]
    public GameObject questionHolder;
    public TextMeshProUGUI questionText;
    public RectTransform questionPos;
    public RectTransform questionHiddenPos;
    #endregion

    #region Point Slider Attributes
    public Slider pointSlider;
    public TextMeshProUGUI playerPoint;
    public TextMeshProUGUI opponentPoint;
    #endregion

    private void Start()
    {
        ShowNavigationButton(); //Show Navigation Buttons and Tabs which is active
    }

    private void OnEnable()
    {
        //Event Subscribe
        EventController.ShowQuestion += ShowQuestion;
        EventController.HideQuestion += HideQuestion;
        EventController.SetQuestion += SetQuestion;

        EventController.ShowNavButtons += ShowNavigationButton;
        EventController.HideNavButtons += HideNavigationButton;
    }

    #region Navigation
    //Call this for showing which Tab is active in Navigation Tab
    //
    [Button]
    public void ShowNavigationItemPanel()
    {
        if (shopBtnSelected_Toggle)
        {
            ShowShopPanel();
        }
        else if (collectionBtnSelected_Toggle)
        {
            ShowCollectionPanel();
        }
        else if (playBtnSelected_Toggle)
        {
            ShowPlayPanel();
        }
        else if (missionBtnSelected_Toggle)
        {
            ShowMissionPanel();
        }
        else if (settingBtnSelected_Toggle)
        {
            ShowSettingPanel();
        }
    }

    public void ShowNavigationButton()
    {
        navigationButtons.GetComponent<RectTransform>().position = navHidePos.position;
        navigationButtons.GetComponent<RectTransform>().transform.LeanMove(navShowPos.position, .75f);
        ShowNavigationItemPanel();
    }

    public void HideNavigationButton()
    {
        navigationButtons.GetComponent<RectTransform>().position = navShowPos.position;
        navigationButtons.GetComponent<RectTransform>().transform.LeanMove(navHidePos.position, .75f);
        ShowNavigationItemPanel();
    }

    //Button Call
    //
    public void OnClick_ShopNav()
    {
        print("aaa");
        if (shopBtnSelected_Toggle)
        {
            return;
        }
        shopBtnSelected_Toggle = !shopBtnSelected_Toggle;

        if (collectionBtnSelected_Toggle)
        {
            collectionBtnSelected_Toggle = !collectionBtnSelected_Toggle;
            UnSelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
        }
        else if (playBtnSelected_Toggle)
        {
            playBtnSelected_Toggle = !playBtnSelected_Toggle;
            UnSelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
        }
        else if (missionBtnSelected_Toggle)
        {
            missionBtnSelected_Toggle = !missionBtnSelected_Toggle;
            UnSelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
        }
        else if (settingBtnSelected_Toggle)
        {
            settingBtnSelected_Toggle = !settingBtnSelected_Toggle;
            UnSelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
        }
        ShowNavigationItemPanel();
    }

    public void OnClick_CollectionNav()
    {
        if (collectionBtnSelected_Toggle)
        {
            return;
        }
        collectionBtnSelected_Toggle = !collectionBtnSelected_Toggle;

        if (shopBtnSelected_Toggle)
        {
            shopBtnSelected_Toggle = !shopBtnSelected_Toggle;
            UnSelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
        }
        else if (playBtnSelected_Toggle)
        {
            playBtnSelected_Toggle = !playBtnSelected_Toggle;
            UnSelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
        }
        else if (missionBtnSelected_Toggle)
        {
            missionBtnSelected_Toggle = !missionBtnSelected_Toggle;
            UnSelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
        }
        else if (settingBtnSelected_Toggle)
        {
            settingBtnSelected_Toggle = !settingBtnSelected_Toggle;
            UnSelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
        }
        ShowNavigationItemPanel();
    }

    public void OnClick_PlayNav()
    {
        //Navigationing
        if (playBtnSelected_Toggle)
        {
            return;
        }
        playBtnSelected_Toggle = !playBtnSelected_Toggle;

        if (collectionBtnSelected_Toggle)
        {
            collectionBtnSelected_Toggle = !collectionBtnSelected_Toggle;
            UnSelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
        }
        else if (shopBtnSelected_Toggle)
        {
            shopBtnSelected_Toggle = !shopBtnSelected_Toggle;
            UnSelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
        }
        else if (missionBtnSelected_Toggle)
        {
            missionBtnSelected_Toggle = !missionBtnSelected_Toggle;
            UnSelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
        }
        else if (settingBtnSelected_Toggle)
        {
            settingBtnSelected_Toggle = !settingBtnSelected_Toggle;
            UnSelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
        }
        ShowNavigationItemPanel();
    }

    public void OnClick_MissionNav()
    {
        if (missionBtnSelected_Toggle)
        {
            return;
        }
        missionBtnSelected_Toggle = !missionBtnSelected_Toggle;

        if (collectionBtnSelected_Toggle)
        {
            collectionBtnSelected_Toggle = !collectionBtnSelected_Toggle;
            UnSelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
        }
        else if (playBtnSelected_Toggle)
        {
            playBtnSelected_Toggle = !playBtnSelected_Toggle;
            UnSelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
        }
        else if (shopBtnSelected_Toggle)
        {
            shopBtnSelected_Toggle = !shopBtnSelected_Toggle;
            UnSelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
        }
        else if (settingBtnSelected_Toggle)
        {
            settingBtnSelected_Toggle = !settingBtnSelected_Toggle;
            UnSelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
        }
        ShowNavigationItemPanel();
    }

    public void OnClick_SettingNav()
    {
        if (settingBtnSelected_Toggle)
        {
            return;
        }
        settingBtnSelected_Toggle = !settingBtnSelected_Toggle;

        if (collectionBtnSelected_Toggle)
        {
            collectionBtnSelected_Toggle = !collectionBtnSelected_Toggle;
            UnSelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
        }
        else if (playBtnSelected_Toggle)
        {
            playBtnSelected_Toggle = !playBtnSelected_Toggle;
            UnSelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
        }
        else if (missionBtnSelected_Toggle)
        {
            missionBtnSelected_Toggle = !missionBtnSelected_Toggle;
            UnSelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
        }
        else if (shopBtnSelected_Toggle)
        {
            shopBtnSelected_Toggle = !shopBtnSelected_Toggle;
            UnSelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
        }
        ShowNavigationItemPanel();
    }

    //Show UI Panel of Nav Button
    //
    public void ShowShopPanel()
    {
        SelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
    }

    public void ShowCollectionPanel()
    {
        SelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
    }

    public void ShowPlayPanel()
    {
        SelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
    }

    public void ShowMissionPanel()
    {
        SelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
    }

    public void ShowSettingPanel()
    {
        SelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
    }

    //Other Methods
    //
    /// <summary>
    /// Call this when Unselecting a Button
    /// </summary>
    /// <param name="selectedBackground">This also being a Gameobject</param>
    /// <param name="selectedIcon"></param>
    /// <param name="selectedPanel"></param>
    public void UnSelectedButton(GameObject selectedBackground, GameObject selectedIcon, GameObject selectedPanel)
    {
        selectedBackground.SetActive(false);

        selectedIcon.GetComponent<RectTransform>().LeanMoveY(0, .1f).setOnComplete(() =>
        {
            Image r = selectedBackground.GetComponent<Image>();
            LeanTween.value(gameObject, 1, 0, .5f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
        });

        IEnumerator panelWait()
        {
            selectedPanel.transform.LeanScale(Vector3.zero, .25f).setEaseOutElastic();
            yield return new WaitForSeconds(.25f);
            selectedPanel.SetActive(false);
        }
        StartCoroutine(panelWait());
    }

    /// <summary>
    /// Call this when Selecting a Button
    /// </summary>
    /// <param name="unSelectedBackground">This also being a Gameobject</param>
    /// <param name="unSelectedIcon"></param>
    /// <param name="unSelectedPanel"></param>
    public void SelectedButton(GameObject unSelectedBackground, GameObject unSelectedIcon, GameObject unSelectedPanel)
    {
        unSelectedBackground.SetActive(true);

        Image r = unSelectedBackground.GetComponent<Image>();
        LeanTween.value(gameObject, 0, 1, .25f).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

        unSelectedIcon.GetComponent<RectTransform>().LeanMoveY(60f, .25f).setEaseInOutCubic().setOnStart(() => 
        { 
            shopBtn.interactable = false; 
            collectionBtn.interactable = false;
            playBtn.interactable = false;
            missionBtn.interactable = false;
            settingBtn.interactable = false;
        }).setOnComplete(() => 
        {
            IEnumerator wait()
            {
                yield return new WaitForSeconds(.5f); //0.75 second delay, avoid spamming buttons
                shopBtn.interactable = true;
                collectionBtn.interactable = true;
                playBtn.interactable = true;
                missionBtn.interactable = true;
                settingBtn.interactable = true;
            }
            StartCoroutine(wait());
        });

        unSelectedPanel.SetActive(true);
    }
    #endregion

    #region Question
    public void ShowQuestion()
    {
        questionHolder.SetActive(true);
        questionHolder.GetComponent<RectTransform>().position = questionHiddenPos.position;
        questionHolder.GetComponent<RectTransform>().transform.LeanMove(questionPos.position, .5f).setOnComplete(() =>
        {
            EventController.OnCardReadyToPlay();
        });
    }

    public void HideQuestion()
    {
        questionHolder.GetComponent<RectTransform>().transform.LeanMove(questionHiddenPos.position, .5f);
    }

    private void SetQuestion(QuestionData questDat)
    {
        questionText.text = questDat.question;
    }

    #endregion

    #region Point Slider

    #endregion
    private void OnDisable()
    {
        EventController.ShowQuestion -= ShowQuestion;
        EventController.SetQuestion -= SetQuestion;
        EventController.HideQuestion -= HideQuestion;

        EventController.ShowNavButtons += ShowNavigationButton;
        EventController.HideNavButtons += HideNavigationButton;
    }
}
