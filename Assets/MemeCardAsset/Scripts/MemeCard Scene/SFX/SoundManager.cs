using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Speaker")]
    private AudioSource speaker;
    [Header("Laugh")]
    public AudioClip laugh1;
    public AudioClip laugh2;
    public AudioClip laugh3;
    [Header("Sad")]
    public AudioClip sad1;
    public AudioClip sad2;
    [Header("Others")]
    public AudioClip buttonClick;
    public AudioClip epicPack;
    public AudioClip findGame;
    public AudioClip moneyReceive;
    private void OnEnable()
    {
        speaker = GetComponent<AudioSource>();
        EventController.SFXPlay_Laugh += PlayLaugh;
        EventController.SFXPlay_Sad += PlaySad;
        EventController.SFXPlay_ButtonClick += PlayButtonClick;
        EventController.SFXPlay_ChooseOpponent += PlayChooseOpponent;
        EventController.SFXPlay_EpicPack += PlayEpicPack;
        EventController.SFXPlay_MoneyReceive += PlayMoneyReceive;
    }

    private void PlayMoneyReceive()
    {
        if (SettingPanel.soundToggle)
        {
            speaker.PlayOneShot(moneyReceive);
        }
    }

    private void PlayEpicPack()
    {
        if (SettingPanel.soundToggle)
        {
            speaker.PlayOneShot(epicPack);
        }
    }

    private void PlayChooseOpponent()
    {
        if (SettingPanel.soundToggle)
        {
            speaker.PlayOneShot(findGame);
        }
    }

    private void PlayButtonClick()
    {
        if (SettingPanel.soundToggle)
        {
            speaker.PlayOneShot(buttonClick);
        }
    }

    private void PlaySad()
    {
        if (SettingPanel.soundToggle)
        {
            int randomIndex = UnityEngine.Random.Range(0, 2);
            if (randomIndex == 0)
            {
                speaker.PlayOneShot(sad1);
            }
            else
            {
                speaker.PlayOneShot(sad2);
            }
        }
    }

    public void PlayLaugh()
    {
        if (SettingPanel.soundToggle)
        {
            int randomIndex = UnityEngine.Random.Range(0, 3);
            if (randomIndex == 0)
            {
                speaker.PlayOneShot(laugh1);
            }
            else if (randomIndex == 1)
            {
                speaker.PlayOneShot(laugh2);
            }
            else
            {
                speaker.PlayOneShot(laugh3);
            }
        }
    }
}
