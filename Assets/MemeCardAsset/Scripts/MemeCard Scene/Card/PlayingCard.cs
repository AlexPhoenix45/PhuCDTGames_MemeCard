using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class PlayingCard : MonoBehaviour
{
    [HideInInspector]
    public Transform placePos;

    public MeshRenderer memeImage;
    public MeshRenderer memeBorder;
    public VideoPlayer videoPlayer;

    public Material commonMat;
    public Material rareMat;
    public Material epicMat;

    public CardData cardData;

    public bool isPlayerCard;

    public bool isPlayed = false;

    private bool readyToPlay = false;

    private void OnEnable()
    {
        EventController.CardReadyToPlay += setCardReadyToPlay;
        EventController.TurnTableCam += StartSecondTurn; //This is called for second turn
    }

    private void StartSecondTurn() //Turn two of playing card
    {
        if (gameObject.activeSelf && isPlayed && !isPlayerCard)
        {
            EventController.OnHideQuestion();
            IEnumerator wait()
            {
                yield return new WaitForSeconds(.5f);
                while (FindObjectOfType<CameraMovement>().isBlending)
                {
                    yield return null;
                }
                EventController.OnCardBattleTurnTwo();
            }
            StartCoroutine(wait());
        }
    }

    private void setCardReadyToPlay()
    {
        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);
            readyToPlay = true;
        }
        StartCoroutine(wait());
    }

    public void SetCard (CardData cardData)
    {
        this.cardData = cardData;
        if (cardData.cardType == CardType.Common)
        {
            memeBorder.material = commonMat;
            memeImage.material = cardData.memeImage;
        }
        else if (cardData.cardType == CardType.Rare)
        {
            memeBorder.material = rareMat;
            memeImage.material = cardData.memeImage;
        }
        else if (cardData.cardType == CardType.Epic)
        {
            memeBorder.material = epicMat;
            videoPlayer.clip = cardData.memeGif;
            videoPlayer.isLooping = true;
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// This method is for bot call, it is the same with OnMouseDown() method
    /// </summary>
    [Button]
    public void PlayThisCard()
    {
        if (readyToPlay)
        {
            isPlayed = true;
            //this is when player select and play a card
            EventController.OnExecutingPoint(this);
            transform.LeanMove(placePos.position, .5f);
            transform.LeanRotate(placePos.transform.eulerAngles, .5f);
            EventController.OnHideAndDrawCard(this.gameObject);
            IEnumerator waitToChangeCam()
            {
                yield return new WaitForSeconds(.5f);
                EventController.OnTurnAudienceCam();
            }
            StartCoroutine(waitToChangeCam());

            if (!isPlayerCard)
            {

            }
        }

    }

    /// <summary>
    /// This is for player click to play card
    /// </summary>
    private void OnMouseDown() 
    {
        if (readyToPlay && isPlayerCard)
        {
            isPlayed = true;
            //this is when player select and play a card
            EventController.OnExecutingPoint(this);
            transform.LeanMove(placePos.position, .5f);
            transform.LeanRotate(placePos.transform.eulerAngles, .5f);
            EventController.OnHideAndDrawCard(this.gameObject);
            IEnumerator waitToChangeCam()
            {
                yield return new WaitForSeconds(.5f);
                EventController.OnTurnAudienceCam();
            }
            StartCoroutine(waitToChangeCam());

            if (!isPlayerCard)
            {

            }
        }
    }

    private void OnDisable()
    {
        EventController.CardReadyToPlay -= setCardReadyToPlay;
        EventController.TurnTableCam -= StartSecondTurn; 
    }
}
