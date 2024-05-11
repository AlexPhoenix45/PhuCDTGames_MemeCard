using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_CardPack : MonoBehaviour
{
    public BuyButton commonBuyBtn;
    public BuyButton rareBuyBtn;
    public BuyButton epicBuyBtn;

    private void OnEnable()
    {
        commonBuyBtn.SetButton(0, "1000", 0);    
        rareBuyBtn.SetButton(0, "5000" ,1);    
        epicBuyBtn.SetButton(0, "10000", 2);    
    }

}
