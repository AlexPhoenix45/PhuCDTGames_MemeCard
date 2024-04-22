using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using System;
using UnityEngine.Rendering;

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

    #region Question Attributes (Card Battle)
    [Header("Question Attributes")]
    public GameObject questionHolder;
    public TextMeshProUGUI questionText;
    public RectTransform questionPos;
    public RectTransform questionHiddenPos;
    #endregion

    #region Point Slider Attributes (Card Battle)
    public Slider pointSlider;
    public TextMeshProUGUI playerPointText;
    public TextMeshProUGUI opponentPointText;

    public CanvasGroup pointSliderAlpha;
    #endregion

    #region End Game Attributes (Card Battle)
    public GameObject endGamePanel;
    public GameObject loseImage;
    public GameObject winImage;
    public Button homeButton;
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

        EventController.ExecutingPointToUI += PointExecuterUI;
        EventController.ShowPointSlider += ShowPointSlider;
        EventController.HidePointSlider += HidePointSlider;

        EventController.CardBattleGameOver += CardBattle_GameOver;
    }

    #region Navigation
    /// <summary>
    /// Call this for showing which Tab is active in Navigation Tab
    /// </summary>
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

    /// <summary>
    /// Show Nav Footer
    /// </summary>
    public void ShowNavigationButton()
    {
        navigationButtons.GetComponent<RectTransform>().position = navHidePos.position;
        navigationButtons.GetComponent<RectTransform>().transform.LeanMove(navShowPos.position, .75f);
        ShowNavigationItemPanel();
    }

    /// <summary>
    /// Hide Nav Footer
    /// </summary>
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

    #region Question (Card Battle)
    /// <summary>
    /// Show UI question card
    /// </summary>
    public void ShowQuestion(int roundCount)
    {
        questionHolder.SetActive(true);
        questionHolder.GetComponent<RectTransform>().position = questionHiddenPos.position;
        questionHolder.GetComponent<RectTransform>().transform.LeanMove(questionPos.position, .5f).setOnComplete(() =>
        {
            EventController.OnCardReadyToPlay();
            if (roundCount >= 2)
            {
                EventController.OnCardBattleNextTurn();
            }
            EventController.OnShowPointSlider();
        });
    }

    /// <summary>
    /// Hide UI question card
    /// </summary>
    public void HideQuestion()
    {
        questionHolder.GetComponent<RectTransform>().transform.LeanMove(questionHiddenPos.position, .5f);
        EventController.OnHidePointSlider();
    }

    /// <summary>
    /// Set question UI card
    /// </summary>
    /// <param name="questDat">Pass question data here</param>
    private void SetQuestion(QuestionData questDat)
    {
        questionText.text = questDat.question;
    }

    #endregion

    #region Point Slider (Card Battle)
    /// <summary>
    /// Point slider UI update
    /// </summary>
    /// <param name="arg0"></param>
    private void PointExecuterUI(int playerPoint, int opponentPoint)
    {
        playerPointText.text = playerPoint.ToString();
        opponentPointText.text = opponentPoint.ToString();

        if (playerPoint == 0)
        {
            if (opponentPoint == 0)
            {
                SliderPointUpdate(pointSlider.value, 50);
            }
            else if (opponentPoint != 0)
            {
                SliderPointUpdate(pointSlider.value, 20);
            }
        }
        else if (playerPoint != 0)
        {
            if (opponentPoint == 0)
            {
                SliderPointUpdate(pointSlider.value, 80);
            }
            else if (opponentPoint != 0)
            {
                if (playerPoint < opponentPoint)
                {
                    SliderPointUpdate(pointSlider.value, 35);
                }
                else if (playerPoint > opponentPoint)
                {
                    SliderPointUpdate(pointSlider.value, 65);
                }
                else
                {
                    SliderPointUpdate(pointSlider.value, 50);
                }
            }
        }

        void SliderPointUpdate(float valueStart, int valueEnd)
        {
            float duration = 1f;
            IEnumerator ChangeSpeed()
            {
                float elapsed = 0.0f;
                while (elapsed < duration)
                {
                    pointSlider.value = (int)Mathf.Lerp(valueStart, valueEnd, elapsed / duration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                pointSlider.value = valueEnd;
            }
            StartCoroutine(ChangeSpeed());
        }
    }

    /// <summary>
    /// Show point slider for some games
    /// </summary>
    private void ShowPointSlider()
    {
        pointSliderAlpha.gameObject.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate((float value) =>
        {
            pointSliderAlpha.alpha = value;
        });
    }

    /// <summary>
    /// Hide point slider
    /// </summary>
    private void HidePointSlider()
    {
        LeanTween.value(1, 0, 1f).setOnUpdate((float value) => {
            pointSliderAlpha.alpha = value;
        }).setOnComplete(() =>
        {
            pointSliderAlpha.gameObject.SetActive(false);
        });
    }
    #endregion

    #region Game Over (Card Battle)
    /// <summary>
    /// Call this when game is over
    /// </summary>
    /// <param name="isPlayerWin"></param>
    private void CardBattle_GameOver(bool isPlayerWin)
    {
        HideQuestion();
        HidePointSlider();
        ShowGameOver_CardBattle(isPlayerWin);
    }

    private void ShowGameOver_CardBattle(bool isPlayerWin)
    {
        endGamePanel.SetActive(true);
        winImage.SetActive(false);
        loseImage.SetActive(false);
        homeButton.gameObject.SetActive(false);
        homeButton.interactable = false;

        LeanTween.value(0, 1, .5f).setOnUpdate((float value) =>
        {
            endGamePanel.GetComponent<Image>().color = new Vector4(1, 1, 1, value);
        }).setOnComplete(() =>
        {
            if (isPlayerWin)
            {
                loseImage.SetActive(false);
                winImage.SetActive(true);
                winImage.transform.localScale = Vector3.zero;
                winImage.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic().setOnComplete(() =>
                {
                    homeButton.gameObject.SetActive(true);
                    LeanTween.value(0, 1, .5f).setOnUpdate((float value) =>
                    {
                        homeButton.GetComponent<Image>().color = new Vector4(1, 1, 1, value);
                        homeButton.interactable = true;
                    });
                });
            }
            else
            {
                loseImage.SetActive(true);
                winImage.SetActive(false);
                loseImage.transform.localScale = Vector3.zero;
                loseImage.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic().setOnComplete(() =>
                {
                    homeButton.gameObject.SetActive(true);
                    LeanTween.value(0, 1, .5f).setOnUpdate((float value) =>
                    {
                        homeButton.GetComponent<Image>().color = new Vector4(1, 1, 1, value);
                        homeButton.interactable = true;
                    });
                }); ;
            }
        });
    }

    /// <summary>
    /// Method for home button in End Game panel
    /// </summary>
    public void OnClick_HomeButton_CardBattle()
    {
        endGamePanel.SetActive(false);
        EventController.OnTurnRoomCam();

        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);
            while (FindObjectOfType<CameraMovement>().isBlending)
            {
                yield return null;
            }
            ShowNavigationButton();
        }
        StartCoroutine(wait());
    }
    #endregion
    private void OnDisable()
    {
        EventController.ShowQuestion -= ShowQuestion;
        EventController.SetQuestion -= SetQuestion;
        EventController.HideQuestion -= HideQuestion;

        EventController.ShowNavButtons -= ShowNavigationButton;
        EventController.HideNavButtons -= HideNavigationButton;

        EventController.ExecutingPointToUI -= PointExecuterUI;
        EventController.ShowPointSlider -= ShowPointSlider;
    }
}
