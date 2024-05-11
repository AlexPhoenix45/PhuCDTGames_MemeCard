using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;
    [Header("Day 1")]
    public GameObject day1_Actvive;
    public GameObject day1_Inactvive;
    public GameObject day1_Received;
    public GameObject day1_Fxs;
    [Header("Day 2")]
    public GameObject day2_Actvive;
    public GameObject day2_Inactvive;
    public GameObject day2_Received;
    public GameObject day2_Fxs;
    [Header("Day 3")]
    public GameObject day3_Actvive;
    public GameObject day3_Inactvive;
    public GameObject day3_Received;
    public GameObject day3_Fxs;
    [Header("Day 4")]
    public GameObject day4_Actvive;
    public GameObject day4_Inactvive;
    public GameObject day4_Received;
    public GameObject day4_Fxs;
    [Header("Day 5")]
    public GameObject day5_Actvive;
    public GameObject day5_Inactvive;
    public GameObject day5_Received;
    public GameObject day5_Fxs;
    [Header("Day 6")]
    public GameObject day6_Actvive;
    public GameObject day6_Inactvive;
    public GameObject day6_Received;
    public GameObject day6_Fxs;
    [Header("Day 7")]
    public GameObject day7_Actvive;
    public GameObject day7_Inactvive;
    public GameObject day7_Received;
    public GameObject day7_Fxs;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
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
            }).id;
        }
    }
}
