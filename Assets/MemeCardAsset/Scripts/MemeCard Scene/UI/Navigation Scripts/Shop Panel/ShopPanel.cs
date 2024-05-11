using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;

    #region Card Pack Shop
    [Header("Card Pack Shop")]
    public BuyButton commonPack_BuyButton;
    public BuyButton rarePack_BuyButton;
    public BuyButton epicPack_BuyButton;
    #endregion

    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        UIManager.tweeningID = gameObject.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic().id;

        Image r = background.GetComponent<Image>();
        if (r.color.a <= 0)
        {
            UIManager.tweeningID = LeanTween.value(background, 0, 1, .25f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            }).setOnComplete(() =>
            {
            }).id;
        }

        //Set Price
        commonPack_BuyButton.SetButton(0, "1000", 0);
        rarePack_BuyButton.SetButton(0, "5000", 1);
        epicPack_BuyButton.SetButton(0, "10000", 2);
    }
}
