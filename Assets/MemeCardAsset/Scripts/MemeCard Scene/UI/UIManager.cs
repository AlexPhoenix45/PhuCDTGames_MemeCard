using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using System;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    #region Navigation Attributes
    [Header("Button")]
    [Header("Navigation")]
    public RectTransform navShowPos;
    public RectTransform navHidePos;
    public GameObject navigationButtons;

    public UnityEngine.UI.Button shopBtn;
    public UnityEngine.UI.Button collectionBtn;
    public UnityEngine.UI.Button playBtn;
    public UnityEngine.UI.Button missionBtn;
    public UnityEngine.UI.Button settingBtn;

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

    #region Header
    [Header("Header Attributes")]
    public GameObject header;
    public GameObject header_CoinContainer;
    public TextMeshProUGUI header_CoinText;
    #endregion

    #region Question Attributes (Card Battle)
    [Header("Question (Card Battle) Attributes")]
    public GameObject questionHolder;
    public TextMeshProUGUI questionText;
    public RectTransform questionPos;
    public RectTransform questionHiddenPos;
    #endregion

    #region Point Slider Attributes (Card Battle)
    [Header("Point Slider (Card Battle) Attributes")]
    public Slider pointSlider;

    public TextMeshProUGUI pointSlider_playerPointText;
    public TextMeshProUGUI pointSlider_opponentPointText;
    public TextMeshProUGUI pointSlider_opponentNameText;

    public GameObject pointSlider_playerFxs;
    public GameObject pointSlider_opponentFxs;

    public CanvasGroup pointSliderAlpha;
    #endregion

    #region Choosing Opponent (Card Battle)
    [Header("Choosing Opponent (Card Battle) Attributes")]
    public GameObject choosingOpponent_Main;

    public GameObject choosingOpponent_Text;
    //public Animator choosingOpponent_Text_Anim;

    //Mid
    public GameObject choosingOpponent_MidOpponent;
    public Image choosingOpponent_MidOpponent_Image;
    public TextMeshProUGUI choosingOpponent_MidOpponent_Text;
    public TextMeshProUGUI choosingOpponent_MidOpponent_Level;
    //Left
    public GameObject choosingOpponent_LeftOpponent;
    public Image choosingOpponent_LeftOpponent_Image;
    public TextMeshProUGUI choosingOpponent_LeftOpponent_Text;
    public TextMeshProUGUI choosingOpponent_LeftOpponent_Level;
    //Right
    public GameObject choosingOpponent_RightOpponent;
    public Image choosingOpponent_RightOpponent_Image;
    public TextMeshProUGUI choosingOpponent_RightOpponent_Text;
    public TextMeshProUGUI choosingOpponent_RightOpponent_Level;

    public OpponentData[] opponentDatas;
    #endregion

    #region Game Over Attributes (Card Battle)
    [Header("End Game (Card Battle) Attributes")]
    public GameObject losePanel;
    public GameObject winPanel;


    #endregion

    #region Multiplier Bar
    [Header("Multiplier Bar")]
    public GameObject multiplierBar_cursor;
    private readonly int multiplierBar_minBarPosY = -470;
    private readonly int multiplierBar_maxBarPosY = 470;
    private bool multiplierBar_rtlMove;
    private bool multiplierBar_isMoving;
    private int multiplierBar_currentValue;
    private bool multiplierBar_stop = false;
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

        EventController.ChooseOpponent += ChoosingOpponent;

        EventController.SetPlayerCoin += UpdateHeaderCoin;
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
        ShowHeader();
    }

    public void ShowCollectionPanel()
    {
        SelectedButton(collectionBtn_Selected_Background, collectionBtn_Selected_Icon, collectionPnl);
        ShowHeader();
    }

    public void ShowPlayPanel()
    {
        SelectedButton(playBtn_Selected_Background, playBtn_Selected_Icon, playPnl);
        EventController.OnSpawnGameOnTable();
        ShowHeader();
    }

    public void ShowMissionPanel()
    {
        SelectedButton(missionBtn_Selected_Background, missionBtn_Selected_Icon, missionPnl);
        ShowHeader();
    }

    public void ShowSettingPanel()
    {
        SelectedButton(settingBtn_Selected_Background, settingBtn_Selected_Icon, settingPnl);
        HideHeader();
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

    #region Header
    private void ShowHeader()
    {
        if (header.GetComponent<CanvasGroup>().alpha != 1)
        {
            header.GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.value(0, 1, .5f).setOnUpdate((float value) =>
            {
                header.GetComponent<CanvasGroup>().alpha = value;
            }).setOnComplete(() =>
            {
                header.GetComponent<CanvasGroup>().alpha = 1;
            });
        }
    }


    private void HideHeader()
    {
        if (header.GetComponent<CanvasGroup>().alpha != 0)
        {
            header.GetComponent<CanvasGroup>().alpha = 1;
            LeanTween.value(1, 0, .5f).setOnUpdate((float value) =>
            {
                header.GetComponent<CanvasGroup>().alpha = value;
            }).setOnComplete(() =>
            {
                header.GetComponent<CanvasGroup>().alpha = 0;
            });
        }
    }
    private void UpdateHeaderCoin(PlayerData playerData)
    {
        LeanTween.value(int.Parse(header_CoinText.text), playerData.currentCoin, 1f).setOnUpdate((float value) =>
        {
            header_CoinText.text = ((int)value).ToString();
        });
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
    /// <param name="questData">Pass question data here</param>
    private void SetQuestion(QuestionData questData)
    {
        //questionText.GetComponent<I2.Loc.Localize>().SetTerm(questData.question);
        string quest = I2.Loc.LocalizationManager.GetTranslation(questData.question);
        questionText.text = quest;
        print(questData.question);
        //print(quest);
    }

    #endregion

    #region Point Slider (Card Battle)
    /// <summary>
    /// Point slider UI update
    /// </summary>
    /// <param name="arg0"></param>
    private void PointExecuterUI(int playerPoint, int opponentPoint)
    {
        //Enable Fxs
        if (pointSlider_playerPointText.text != playerPoint.ToString())
        {
            pointSlider_playerFxs.SetActive(true);
            pointSlider_opponentFxs.SetActive(false);
        }
        else if (pointSlider_opponentPointText.text != opponentPoint.ToString())
        {
            pointSlider_playerFxs.SetActive(false);
            pointSlider_opponentFxs.SetActive(true);
        }

        //Point Smooth Transition
        {
            LeanTween.value(int.Parse(pointSlider_playerPointText.text), playerPoint, 1f).setEaseInOutCirc().setOnUpdate((float value) =>
            {
                int finalValue = (int)value;
                pointSlider_playerPointText.text = finalValue.ToString();
            }).setOnComplete(() =>
            {
                pointSlider_playerPointText.text = playerPoint.ToString();
                //SetActive Fxs after point transition
                if (playerPoint < opponentPoint)
                {
                    pointSlider_playerFxs.SetActive(false);
                    pointSlider_opponentFxs.SetActive(true);
                }
                else if (playerPoint == opponentPoint)
                {
                    pointSlider_playerFxs.SetActive(true);
                    pointSlider_opponentFxs.SetActive(true);
                }
            });

            LeanTween.value(int.Parse(pointSlider_opponentPointText.text), opponentPoint, 1f).setEaseInOutCirc().setOnUpdate((float value) =>
            {
                int finalValue = (int)value;
                pointSlider_opponentPointText.text = finalValue.ToString();
            }).setOnComplete(() =>
            {
                pointSlider_opponentPointText.text = opponentPoint.ToString();
                //SetActive Fxs after point transition
                if (opponentPoint < playerPoint)
                {
                    pointSlider_playerFxs.SetActive(true);
                    pointSlider_opponentFxs.SetActive(true);
                }
                else if (playerPoint == opponentPoint)
                {
                    pointSlider_playerFxs.SetActive(true);
                    pointSlider_opponentFxs.SetActive(true);
                }
            });
        }

        if (playerPoint == 0)
        {
            if (opponentPoint == 0)
            {
                SliderPointUpdate();
            }
            else if (opponentPoint != 0)
            {
                SliderPointUpdate();
            }
        }
        else if (playerPoint != 0)
        {
            if (opponentPoint == 0)
            {
                SliderPointUpdate();
            }
            else if (opponentPoint != 0)
            {
                if (playerPoint < opponentPoint)
                {
                    SliderPointUpdate();
                }
                else if (playerPoint > opponentPoint)
                {
                    SliderPointUpdate();
                }
                else
                {
                    SliderPointUpdate();
                }
            }
        }

        void SliderPointUpdate()
        {
            float duration = 1f;

            float valueEnd;
            float valueStart = pointSlider.value;

            if (playerPoint > opponentPoint)
            {
                valueEnd = 50 + ((playerPoint - opponentPoint) / 2) > 80 ? valueEnd = 80 : valueEnd = 50 + ((playerPoint - opponentPoint) / 2);
            }
            else if (playerPoint < opponentPoint)
            {
                valueEnd = (opponentPoint - playerPoint) > 30 ? valueEnd = 30 : valueEnd = 50 - (opponentPoint - playerPoint);
            }
            else
            {
                valueEnd = 50;
            }

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

    #region Choosing Opponent (Card Battle)
    private void ChoosingOpponent()
    {
        choosingOpponent_Main.SetActive(true);
        choosingOpponent_Text.SetActive(true);
        choosingOpponent_MidOpponent.SetActive(true);
        choosingOpponent_LeftOpponent.SetActive(true);
        choosingOpponent_RightOpponent.SetActive(true);

        choosingOpponent_MidOpponent.GetComponent<CanvasGroup>().alpha = 0;
        choosingOpponent_LeftOpponent.GetComponent<CanvasGroup>().alpha = 0;
        choosingOpponent_RightOpponent.GetComponent<CanvasGroup>().alpha = 0;

        LeanTween.value(0, 1f, 1f).setOnUpdate((float value) =>
        {
            choosingOpponent_MidOpponent.GetComponent<CanvasGroup>().alpha = value;
            choosingOpponent_LeftOpponent.GetComponent<CanvasGroup>().alpha = value;
            choosingOpponent_RightOpponent.GetComponent<CanvasGroup>().alpha = value;
        });

        float time = 4;
        float timeElapsed = 0;
        float playerLvlRange = (float)PlayerDataStorage.Instance.data.currentLvl / 10;

        int selectedOpponentLvl = 0;

        IEnumerator chooseOpponent()
        {
            OpponentData tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
            choosingOpponent_RightOpponent_Image.sprite = tempOpponent.opponentImage;
            choosingOpponent_RightOpponent_Text.text = tempOpponent.opponentName;
            choosingOpponent_RightOpponent_Level.text = RandomOpponentLevel().ToString();
           
            tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
            choosingOpponent_MidOpponent_Image.sprite = tempOpponent.opponentImage;
            choosingOpponent_MidOpponent_Text.text = tempOpponent.opponentName;
            choosingOpponent_MidOpponent_Level.text = RandomOpponentLevel().ToString();
           
            tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
            choosingOpponent_LeftOpponent_Image.sprite = tempOpponent.opponentImage;
            choosingOpponent_LeftOpponent_Text.text = tempOpponent.opponentName;
            choosingOpponent_LeftOpponent_Level.text = RandomOpponentLevel().ToString();
           
            while (timeElapsed < time)
            {
                if (timeElapsed < 1.8f)
                {
                    choosingOpponent_LeftOpponent_Image.sprite = choosingOpponent_MidOpponent_Image.sprite;
                    choosingOpponent_LeftOpponent_Text.text = choosingOpponent_MidOpponent_Text.text;
                    choosingOpponent_LeftOpponent_Level.text = choosingOpponent_MidOpponent_Level.text;

                    choosingOpponent_MidOpponent_Image.sprite = choosingOpponent_RightOpponent_Image.sprite;
                    choosingOpponent_MidOpponent_Text.text = choosingOpponent_RightOpponent_Text.text;
                    choosingOpponent_MidOpponent_Level.text = choosingOpponent_RightOpponent_Level.text;

                    tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
                    choosingOpponent_RightOpponent_Image.sprite = tempOpponent.opponentImage;
                    choosingOpponent_RightOpponent_Text.text = tempOpponent.opponentName;
                    choosingOpponent_RightOpponent_Level.text = RandomOpponentLevel().ToString();
                   
                    yield return new WaitForSeconds(.1f);
                    timeElapsed += .1f;
                }
                else if (timeElapsed < 2.5f)
                {
                    choosingOpponent_LeftOpponent_Image.sprite = choosingOpponent_MidOpponent_Image.sprite;
                    choosingOpponent_LeftOpponent_Text.text = choosingOpponent_MidOpponent_Text.text;
                    choosingOpponent_LeftOpponent_Level.text = choosingOpponent_MidOpponent_Level.text;

                    choosingOpponent_MidOpponent_Image.sprite = choosingOpponent_RightOpponent_Image.sprite;
                    choosingOpponent_MidOpponent_Text.text = choosingOpponent_RightOpponent_Text.text;
                    choosingOpponent_MidOpponent_Level.text = choosingOpponent_RightOpponent_Level.text;

                    tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
                    choosingOpponent_RightOpponent_Image.sprite = tempOpponent.opponentImage;
                    choosingOpponent_RightOpponent_Text.text = tempOpponent.opponentName;
                    choosingOpponent_RightOpponent_Level.text = RandomOpponentLevel().ToString();
                   
                    yield return new WaitForSeconds(.2f);
                    timeElapsed += .2f;
                }
                else
                {
                    choosingOpponent_LeftOpponent_Image.sprite = choosingOpponent_MidOpponent_Image.sprite;
                    choosingOpponent_LeftOpponent_Text.text = choosingOpponent_MidOpponent_Text.text;
                    choosingOpponent_LeftOpponent_Level.text = choosingOpponent_MidOpponent_Level.text;

                    choosingOpponent_MidOpponent_Image.sprite = choosingOpponent_RightOpponent_Image.sprite;
                    choosingOpponent_MidOpponent_Text.text = choosingOpponent_RightOpponent_Text.text;
                    choosingOpponent_MidOpponent_Level.text = choosingOpponent_RightOpponent_Level.text;
                    selectedOpponentLvl = int.Parse(choosingOpponent_RightOpponent_Level.text);

                    tempOpponent = opponentDatas[UnityEngine.Random.Range(0, opponentDatas.Length)];
                    choosingOpponent_RightOpponent_Image.sprite = tempOpponent.opponentImage;
                    choosingOpponent_RightOpponent_Text.text = tempOpponent.opponentName;
                    choosingOpponent_RightOpponent_Level.text = RandomOpponentLevel().ToString();
                   
                    yield return new WaitForSeconds(.3f);
                    timeElapsed += .3f;
                }
            }

            LeanTween.value(1, 1.5f, 1f).setOnUpdate((float value) =>
            {
                choosingOpponent_MidOpponent.transform.localScale = new Vector3(value, value, value);
                pointSlider_opponentNameText.text = choosingOpponent_MidOpponent_Text.text;
            });

            LeanTween.value(1, 0, 1f).setOnUpdate((float value) =>
            {
                choosingOpponent_LeftOpponent.transform.localScale = new Vector3(value, value, value);
                choosingOpponent_LeftOpponent.GetComponent<CanvasGroup>().alpha = value;

                choosingOpponent_RightOpponent.transform.localScale = new Vector3(value, value, value);
                choosingOpponent_RightOpponent.GetComponent<CanvasGroup>().alpha = value;
            }).setOnComplete(() =>
            {
                choosingOpponent_LeftOpponent.SetActive(false);
                choosingOpponent_RightOpponent.SetActive(false);
            });

            yield return new WaitForSeconds(3f);
            choosingOpponent_Main.SetActive(false);
            EventController.OnDrawStartingCard(selectedOpponentLvl);
            //print(selectedOpponentLvl);
            Restart();
        }
        StartCoroutine(chooseOpponent());

        void Restart()
        {
            choosingOpponent_MidOpponent.transform.localScale = Vector3.one;
            choosingOpponent_RightOpponent.transform.localScale = new Vector3(.8f, .8f, .8f);
            choosingOpponent_LeftOpponent.transform.localScale = new Vector3(.8f, .8f, .8f);

            choosingOpponent_RightOpponent.GetComponent<CanvasGroup>().alpha = 1.0f;
            choosingOpponent_LeftOpponent.GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        int RandomOpponentLevel()
        {
            if ((float)PlayerDataStorage.Instance.data.currentLvl % 5 != 0)
            {
                int lvlReturn = ((int)UnityEngine.Random.Range(10 * Mathf.Floor(playerLvlRange), 10 * Mathf.Ceil(playerLvlRange)));
                do
                {
                    lvlReturn = ((int)UnityEngine.Random.Range(10 * Mathf.Floor(playerLvlRange), 10 * Mathf.Ceil(playerLvlRange)));
                }
                while (lvlReturn % 5 == 0);

                return lvlReturn;
            }
            else
            {
                return PlayerDataStorage.Instance.data.currentLvl;
            }
        }
    }

    #endregion

    #region Game Over - Reset Variables (Card Battle)
    /// <summary>
    /// Call this when game is over
    /// </summary>
    /// <param name="isPlayerWin"></param>
    private void CardBattle_GameOver(bool isPlayerWin)
    {
        HideQuestion();
        HidePointSlider();
        HideHeader();
        ShowGameOver_CardBattle(isPlayerWin);

        pointSlider_playerPointText.text = "0";
        pointSlider_opponentPointText.text = "0";

        pointSlider.value = 50;
    }

    //Game Over Panel
    private void ShowGameOver_CardBattle(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            losePanel.SetActive(false);
            winPanel.SetActive(true);
            MultiplierBar_Start();
            EventController.OnAddPlayerLevel(1);
        }
        else
        {
            losePanel.SetActive(true);
            winPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Method for home button in End Game panel
    /// </summary>
    public void OnClick_HomeButton_CardBattle()
    {
        EventController.OnTurnRoomCam();

        losePanel.SetActive(false);
        winPanel.SetActive(false);

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

    public void OnClick_ClaimButton_CardBattle()
    {
        if (multiplierBar_currentValue >= -470 && multiplierBar_currentValue < -290)
        {
            EventController.OnAddPlayerCoin(2 * (int)(100 * Mathf.Ceil(PlayerDataStorage.Instance.data.currentLvl / 5.0f)));
        }
        else if (multiplierBar_currentValue >= -290 && multiplierBar_currentValue < -85)
        {
            EventController.OnAddPlayerCoin(4 * (int)(100 * Mathf.Ceil(PlayerDataStorage.Instance.data.currentLvl / 5.0f)));
        }
        else if (multiplierBar_currentValue >= -85 && multiplierBar_currentValue < 105)
        {
            EventController.OnAddPlayerCoin(8 * (int)(100 * Mathf.Ceil(PlayerDataStorage.Instance.data.currentLvl / 5.0f)));
        }
        else if (multiplierBar_currentValue >= 105 && multiplierBar_currentValue < 305)
        {
            EventController.OnAddPlayerCoin(4 * (int)(100 * Mathf.Ceil(PlayerDataStorage.Instance.data.currentLvl / 5.0f)));
        }
        else if (multiplierBar_currentValue >= 305 && multiplierBar_currentValue < 470)
        {
            EventController.OnAddPlayerCoin(2 * (int)(100 * Mathf.Ceil(PlayerDataStorage.Instance.data.currentLvl / 5.0f)));
        }

        multiplierBar_stop = true;
        OnClick_HomeButton_CardBattle(); //This is where to call ads
    }
    #endregion

    #region Multiplier Bar
    [Button]
    private void MultiplierBar_Start()
    {
        multiplierBar_rtlMove = true;
        multiplierBar_currentValue = 0;
        multiplierBar_stop = false;
        StartCoroutine(move());
        IEnumerator move()
        {
            do
            {
                if (multiplierBar_rtlMove && !multiplierBar_isMoving)
                {
                    LeanTween.value(multiplierBar_minBarPosY, multiplierBar_maxBarPosY, .7f).setOnUpdate((float value) =>
                    {
                        multiplierBar_cursor.GetComponent<RectTransform>().localPosition = new Vector3(value, multiplierBar_cursor.GetComponent<RectTransform>().localPosition.y, multiplierBar_cursor.GetComponent<RectTransform>().localPosition.z);
                        multiplierBar_isMoving = true;
                        multiplierBar_currentValue = (int)value;
                    }).setOnComplete(() =>
                    {
                        multiplierBar_rtlMove = false;
                        multiplierBar_isMoving = false;
                    });
                }
                else if (!multiplierBar_rtlMove && !multiplierBar_isMoving)
                {
                    LeanTween.value(multiplierBar_maxBarPosY, multiplierBar_minBarPosY, .7f).setOnUpdate((float value) =>
                    {
                        multiplierBar_cursor.GetComponent<RectTransform>().localPosition = new Vector3(value, multiplierBar_cursor.GetComponent<RectTransform>().localPosition.y, multiplierBar_cursor.GetComponent<RectTransform>().localPosition.z);
                        multiplierBar_isMoving = true;
                        multiplierBar_currentValue = (int)value;
                    }).setOnComplete(() => 
                    {
                        multiplierBar_rtlMove = true;
                        multiplierBar_isMoving = false;
                    });
                }

                yield return null;
            }
            while (!multiplierBar_stop);
        }
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
