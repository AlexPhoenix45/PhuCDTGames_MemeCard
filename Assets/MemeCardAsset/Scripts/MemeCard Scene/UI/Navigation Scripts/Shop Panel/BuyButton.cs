using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public GameObject coinPanel;
    public TextMeshProUGUI coinPanelText;
    public GameObject adsPanel;
    public GameObject iapPanel;
    public TextMeshProUGUI iapPanelText;

    //Use this to buy Card Pack in Shop (Fix this when update Shop)
    public int cardPackRarity;

    /// <summary>
    /// Call this to set button's purchase options
    /// </summary>
    /// <param name="panelIndex">0 = Coin Purchasing; 1 = Ads Purchasing; 2 = IAP Purchasing</param>
    /// <param name="price">Item's Price</param>
    public void SetButton(int panelIndex, string price, int cardPackRarity)
    {
        this.cardPackRarity = cardPackRarity;
        if (panelIndex == 0)
        {
            coinPanel.SetActive(true);
            adsPanel.SetActive(false);
            iapPanel.SetActive(false);

            coinPanelText.text = price;
        }
        else if (panelIndex == 1)
        {
            coinPanel.SetActive(false);
            adsPanel.SetActive(true);
            iapPanel.SetActive(false);
        }
        else if (panelIndex == 2)
        {
            coinPanel.SetActive(false);
            adsPanel.SetActive(false);
            iapPanel.SetActive(true);

            iapPanelText.text = price;
        }
    }

    public void OnClick_Buy_CardPack()
    {
        if (cardPackRarity == 0)
        {
            if (PlayerDataStorage.Instance.data.currentCoin - 1000 >= 0)
            {
                EventController.OnSpawnPack(0, false, false);
                EventController.OnAddPlayerCoin(-1000);
            }
        }
        else if (cardPackRarity == 1)
        {
            if (PlayerDataStorage.Instance.data.currentCoin - 5000 >= 0)
            {
                EventController.OnSpawnPack(1, false, false);
                EventController.OnAddPlayerCoin(-5000);
            }
        }
        else if (cardPackRarity == 2)
        {
            if (PlayerDataStorage.Instance.data.currentCoin - 10000 >= 0)
            {
                EventController.OnSpawnPack(2, false, false);
                EventController.OnAddPlayerCoin(-10000);
            }
        }
    }
}
