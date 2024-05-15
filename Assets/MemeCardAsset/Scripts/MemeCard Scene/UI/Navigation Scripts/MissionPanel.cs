using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;

    public int LastDate;

    [Header("Day 1")]
    public int day1;
    public GameObject day1_Actvive;
    public GameObject day1_Inactvive;
    public GameObject day1_Received;
    public GameObject day1_Fxs;
    public Button day1_Btn;
    [Header("Day 2")]
    public int day2;
    public GameObject day2_Actvive;
    public GameObject day2_Inactvive;
    public GameObject day2_Received;
    public GameObject day2_Fxs;
    public Button day2_Btn;
    [Header("Day 3")]
    public int day3;
    public GameObject day3_Actvive;
    public GameObject day3_Inactvive;
    public GameObject day3_Received;
    public GameObject day3_Fxs;
    public Button day3_Btn;
    [Header("Day 4")]
    public int day4;
    public GameObject day4_Actvive;
    public GameObject day4_Inactvive;
    public GameObject day4_Received;
    public GameObject day4_Fxs;
    public Button day4_Btn;
    [Header("Day 5")]
    public int day5;
    public GameObject day5_Actvive;
    public GameObject day5_Inactvive;
    public GameObject day5_Received;
    public GameObject day5_Fxs;
    public Button day5_Btn;
    [Header("Day 6")]
    public int day6;
    public GameObject day6_Actvive;
    public GameObject day6_Inactvive;
    public GameObject day6_Received;
    public GameObject day6_Fxs;
    public Button day6_Btn;
    [Header("Day 7")]
    public int day7;
    public GameObject day7_Actvive;
    public GameObject day7_Inactvive;
    public GameObject day7_Received;
    public GameObject day7_Fxs;
    public Button day7_Btn;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;

        day1 = PlayerPrefs.GetInt("Day_1");
        day2 = PlayerPrefs.GetInt("Day_2");
        day3 = PlayerPrefs.GetInt("Day_3");
        day4 = PlayerPrefs.GetInt("Day_4");
        day5 = PlayerPrefs.GetInt("Day_5");
        day6 = PlayerPrefs.GetInt("Day_6");
        day7 = PlayerPrefs.GetInt("Day_7");
        LastDate = PlayerPrefs.GetInt("LastDate");

        Reward_SetUI();

        if (LastDate != System.DateTime.Now.Day)
        {
            if (day1 == 0)
            {
                day1 = 1;
            }
            else if (day2 == 0)
            {
                day2 = 1;
            }
            else if (day3 == 0)
            {
                day3 = 1;
            }
            else if (day4 == 0)
            {
                day4 = 1;
            }
            else if (day5 == 0)
            {
                day5 = 1;
            }
            else if (day6 == 0)
            {
                day6 = 1;
            }
            else if (day7 == 0)
            {
                day7 = 1;
            }

            Reward_SetUI();
        }
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

    public void Reward_SetUI()
    {
        //Day 1
        if (day1 == 0)
        {
            day1_Received.SetActive(false);
            day1_Actvive.SetActive(false);
            day1_Fxs.SetActive(false);
            day1_Inactvive.SetActive(true);
            day1_Btn.interactable = false;
        }
        if (day1 == 1)
        {
            day1_Received.SetActive(false);
            day1_Actvive.SetActive(true);
            day1_Fxs.SetActive(true);
            day1_Inactvive.SetActive(false);
            day1_Btn.interactable = true;
        }
        if (day1 == 2)
        {
            day1_Received.SetActive(true);
            day1_Actvive.SetActive(false);
            day1_Fxs.SetActive(false);
            day1_Inactvive.SetActive(false);
            day1_Btn.interactable = false;
        }

        //Day 2
        if (day2 == 0)
        {
            day2_Received.SetActive(false);
            day2_Actvive.SetActive(false);
            day2_Fxs.SetActive(false);
            day2_Inactvive.SetActive(true);
            day2_Btn.interactable = false;
        }
        if (day2 == 1)
        {
            day2_Received.SetActive(false);
            day2_Actvive.SetActive(true);
            day2_Fxs.SetActive(true);
            day2_Inactvive.SetActive(false);
            day2_Btn.interactable = true;
        }
        if (day2 == 2)
        {
            day2_Received.SetActive(true);
            day2_Actvive.SetActive(false);
            day2_Fxs.SetActive(false);
            day2_Inactvive.SetActive(false);
            day2_Btn.interactable = false;
        }

        //Day 3
        if (day3 == 0)
        {
            day3_Received.SetActive(false);
            day3_Actvive.SetActive(false);
            day3_Fxs.SetActive(false);
            day3_Inactvive.SetActive(true);
            day3_Btn.interactable = false;
        }
        if (day3 == 1)
        {
            day3_Received.SetActive(false);
            day3_Actvive.SetActive(true);
            day3_Fxs.SetActive(true);
            day3_Inactvive.SetActive(false);
            day3_Btn.interactable = true;
        }
        if (day3 == 2)
        {
            day3_Received.SetActive(true);
            day3_Actvive.SetActive(false);
            day3_Fxs.SetActive(false);
            day3_Inactvive.SetActive(false);
            day3_Btn.interactable = false;
        }

        //Day 4
        if (day4 == 0)
        {
            day4_Received.SetActive(false);
            day4_Actvive.SetActive(false);
            day4_Fxs.SetActive(false);
            day4_Inactvive.SetActive(true);
            day4_Btn.interactable = false;
        }
        if (day4 == 1)
        {
            day4_Received.SetActive(false);
            day4_Actvive.SetActive(true);
            day4_Fxs.SetActive(true);
            day4_Inactvive.SetActive(false);
            day4_Btn.interactable = true;
        }
        if (day4 == 2)
        {
            day4_Received.SetActive(true);
            day4_Actvive.SetActive(false);
            day4_Fxs.SetActive(false);
            day4_Inactvive.SetActive(false);
            day4_Btn.interactable = false;
        }

        //Day 5
        if (day5 == 0)
        {
            day5_Received.SetActive(false);
            day5_Actvive.SetActive(false);
            day5_Fxs.SetActive(false);
            day5_Inactvive.SetActive(true);
            day5_Btn.interactable = false;
        }
        if (day5 == 1)
        {
            day5_Received.SetActive(false);
            day5_Actvive.SetActive(true);
            day5_Fxs.SetActive(true);
            day5_Inactvive.SetActive(false);
            day5_Btn.interactable = true;
        }
        if (day5 == 2)
        {
            day5_Received.SetActive(true);
            day5_Actvive.SetActive(false);
            day5_Fxs.SetActive(false);
            day5_Inactvive.SetActive(false);
            day5_Btn.interactable = false;
        }

        //Day 6
        if (day6 == 0)
        {
            day6_Received.SetActive(false);
            day6_Actvive.SetActive(false);
            day6_Fxs.SetActive(false);
            day6_Inactvive.SetActive(true);
            day6_Btn.interactable = false;
        }
        if (day6 == 1)
        {
            day6_Received.SetActive(false);
            day6_Actvive.SetActive(true);
            day6_Fxs.SetActive(true);
            day6_Inactvive.SetActive(false);
            day6_Btn.interactable = true;
        }
        if (day6 == 2)
        {
            day6_Received.SetActive(true);
            day6_Actvive.SetActive(false);
            day6_Fxs.SetActive(false);
            day6_Inactvive.SetActive(false);
            day6_Btn.interactable = false;
        }

        //Day 7
        if (day7 == 0)
        {
            day7_Received.SetActive(false);
            day7_Actvive.SetActive(false);
            day7_Fxs.SetActive(false);
            day7_Inactvive.SetActive(true);
            day7_Btn.interactable = false;
        }
        if (day7 == 1)
        {
            day7_Received.SetActive(false);
            day7_Actvive.SetActive(true);
            day7_Fxs.SetActive(true);
            day7_Inactvive.SetActive(false);
            day7_Btn.interactable = true;
        }
        if (day7 == 2)
        {
            day7_Received.SetActive(true);
            day7_Actvive.SetActive(false);
            day7_Fxs.SetActive(false);
            day7_Inactvive.SetActive(false);
            day7_Btn.interactable = false;
        }
    }

    public void GetReward_1()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 1 <color> / 7");

        day1 = 2;
        PlayerPrefs.SetInt("Day_1", 2);

        //Reward Granted
        EventController.OnAddPlayerCoin(200);

        Reward_SetUI();
    }

    public void GetReward_2()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 2 <color> / 7");

        day2 = 2;
        PlayerPrefs.SetInt("Day_2", 2);

        //Reward Granted
        EventController.OnSpawnPack(0, false, false);

        Reward_SetUI();
    }

    public void GetReward_3()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 3 <color> / 7");

        day3 = 2;
        PlayerPrefs.SetInt("Day_3", 2);

        //Reward Granted
        EventController.OnAddPlayerCoin(300);

        Reward_SetUI();
    }

    public void GetReward_4()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 4 <color> / 7");

        day4 = 2;
        PlayerPrefs.SetInt("Day_4", 2);

        //Reward Granted
        EventController.OnAddPlayerCoin(500);

        Reward_SetUI();
    }

    public void GetReward_5()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 5 <color> / 7");

        day5 = 2;
        PlayerPrefs.SetInt("Day_5", 2);

        //Reward Granted
        EventController.OnAddPlayerCoin(1000);

        Reward_SetUI();
    }

    public void GetReward_6()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 6 <color> / 7");

        day6 = 2;
        PlayerPrefs.SetInt("Day_6", 2);

        //Reward Granted
        EventController.OnAddPlayerCoin(2000);

        Reward_SetUI();
    }

    public void GetReward_7()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        Console.BackgroundColor = ConsoleColor.Red;
        print("Daily Login Rewards<color=#f6e19c> 7 <color> / 7");

        day7 = 2;
        PlayerPrefs.SetInt("Day_7", 2);

        //Reward Granted
        EventController.OnSpawnPack(2, false, false);

        Reward_SetUI();
    }
    [Button]
    public void ResetReward()
    {
        PlayerPrefs.SetInt("Day_1", 0);
        PlayerPrefs.SetInt("Day_2", 0);
        PlayerPrefs.SetInt("Day_3", 0);
        PlayerPrefs.SetInt("Day_4", 0);
        PlayerPrefs.SetInt("Day_5", 0);
        PlayerPrefs.SetInt("Day_6", 0);
        PlayerPrefs.SetInt("Day_7", 0);
        PlayerPrefs.SetInt("LastDate", 0);
    }
}
