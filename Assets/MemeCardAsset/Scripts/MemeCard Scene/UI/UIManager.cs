using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    [Header("Navigation")]
    public Button shopBtn;
    public Button cardBtn;
    public Button playBtn;
    public Button missionBtn;
    public Button settingBtn;

    public GameObject shopBtn_Selected_Background; 
    public GameObject cardBtn_Selected_Background;
    public GameObject playBtn_Selected_Background;
    public GameObject missionBtn_Selected_Background;
    public GameObject settingBtn_Selected_Background;

    public GameObject shopBtn_Selected_Icon;
    public GameObject cardBtn_Selected_Icon;
    public GameObject playBtn_Selected_Icon;
    public GameObject missionBtn_Selected_Icon;
    public GameObject settingBtn_Selected_Icon;

    private bool shopBtnSelected_Toggle = false;
    private bool cardBtnSelected_Toggle = false;
    private bool playBtnSelected_Toggle = true;
    private bool missionBtnSelected_Toggle = false;
    private bool settingBtnSelected_Toggle = false;

    [Header("Panel")]
    public GameObject shopPnl;
    public GameObject cardPnl;
    public GameObject playPnl;
    public GameObject missionPnl;
    public GameObject settingPnl;

    [Header("VCam")]
    public GameObject roomCam;
    public GameObject tableCam;

    private void Start()
    {
        ShowNavigation(); //Show Navigation Tab which is active
    }

    #region Navigation
    //Call this for showing which Tab is active in Navigation Tab
    //
    [Button]
    public void ShowNavigation()
    {
        if (shopBtnSelected_Toggle)
        {
            ShowShopPanel();
        }
        else if (cardBtnSelected_Toggle)
        {
            ShowCardPanel();
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

    //Button Call
    //
    public void OnClick_Shop()
    {
        print("aaa");
        if (shopBtnSelected_Toggle)
        {
            return;
        }
        shopBtnSelected_Toggle = !shopBtnSelected_Toggle;

        if (cardBtnSelected_Toggle)
        {
            cardBtnSelected_Toggle = !cardBtnSelected_Toggle;
            UnSelectedButton(cardBtn_Selected_Background, cardBtn_Selected_Icon, cardPnl);
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
        ShowNavigation();
    }

    public void OnClick_Card()
    {
        if (cardBtnSelected_Toggle)
        {
            return;
        }
        cardBtnSelected_Toggle = !cardBtnSelected_Toggle;

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
        ShowNavigation();
    }

    public void OnClick_Play()
    {
        if (playBtnSelected_Toggle)
        {
            return;
        }
        playBtnSelected_Toggle = !playBtnSelected_Toggle;

        if (cardBtnSelected_Toggle)
        {
            cardBtnSelected_Toggle = !cardBtnSelected_Toggle;
            UnSelectedButton(cardBtn_Selected_Background, cardBtn_Selected_Icon, cardPnl);
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
        ShowNavigation();
    }

    public void OnClick_Mission()
    {
        if (missionBtnSelected_Toggle)
        {
            return;
        }
        missionBtnSelected_Toggle = !missionBtnSelected_Toggle;

        if (cardBtnSelected_Toggle)
        {
            cardBtnSelected_Toggle = !cardBtnSelected_Toggle;
            UnSelectedButton(cardBtn_Selected_Background, cardBtn_Selected_Icon, cardPnl);
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
        ShowNavigation();
    }

    public void OnClick_Setting()
    {
        if (settingBtnSelected_Toggle)
        {
            return;
        }
        settingBtnSelected_Toggle = !settingBtnSelected_Toggle;

        if (cardBtnSelected_Toggle)
        {
            cardBtnSelected_Toggle = !cardBtnSelected_Toggle;
            UnSelectedButton(cardBtn_Selected_Background, cardBtn_Selected_Icon, cardPnl);
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
        ShowNavigation();
    }

    //Show UI Panel of Nav Button
    //
    public void ShowShopPanel()
    {
        SelectedButton(shopBtn_Selected_Background, shopBtn_Selected_Icon, shopPnl);
    }

    public void ShowCardPanel()
    {
        SelectedButton(cardBtn_Selected_Background, cardBtn_Selected_Icon, cardPnl);
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

        if (unSelectedBackground != playBtn_Selected_Background)
        {
            Image r = unSelectedBackground.GetComponent<Image>();
            LeanTween.value(gameObject, 0, 1, .25f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
        }

        unSelectedIcon.GetComponent<RectTransform>().LeanMoveY(60f, .25f).setEaseInOutCubic().setOnStart(() => 
        { 
            shopBtn.interactable = false; 
            cardBtn.interactable = false;
            playBtn.interactable = false;
            missionBtn.interactable = false;
            settingBtn.interactable = false;
        }).setOnComplete(() => 
        {
            IEnumerator wait()
            {
                yield return new WaitForSeconds(.5f); //0.75 second delay, avoid spamming buttons
                shopBtn.interactable = true;
                cardBtn.interactable = true;
                playBtn.interactable = true;
                missionBtn.interactable = true;
                settingBtn.interactable = true;
            }
            StartCoroutine(wait());
        });

        unSelectedPanel.SetActive(true);
    }
    #endregion
}
