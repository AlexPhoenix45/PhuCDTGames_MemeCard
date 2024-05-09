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

    public bool isHighlighted;

    private bool readyToPlay = false;

    //Package Parameters
    public bool isForPackage;
    [HideInInspector]
    public bool hasClick = false;
    [HideInInspector]
    public bool isLastItem = false;

    private void OnEnable()
    {
        EventController.CardReadyToPlay += SetCardReadyToPlay;
        EventController.TurnTableCam += StartSecondTurn; //This is called for second turn

        isHighlighted = false;
        hasClick = false;
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

    private void SetCardReadyToPlay()
    {
        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);
            readyToPlay = true;
        }
        StartCoroutine(wait());
    }

    [Button]
    public void ShowCard()
    {
        SetCard(this.cardData);
    }
    public void SetCard (CardData cardData)
    {
        this.cardData = cardData;
        if (cardData.rarityType == RarityType.Common)
        {
            memeBorder.material = commonMat;
            memeImage.material = cardData.memeMaterial;
            videoPlayer.enabled = false;
        }
        else if (cardData.rarityType == RarityType.Rare)
        {
            memeBorder.material = rareMat;
            memeImage.material = cardData.memeMaterial;
            videoPlayer.enabled = false;
        }
        else if (cardData.rarityType == RarityType.Epic)
        {
            memeBorder.material = epicMat;

            if (cardData.memeGif != null)
            {
                videoPlayer.clip = cardData.memeGif;
                videoPlayer.enabled = true;
                videoPlayer.isLooping = true;
                videoPlayer.Play();
            }
            else
            {
                memeBorder.material = rareMat;
                memeImage.material = cardData.memeMaterial;
                videoPlayer.enabled = false;
            }
        }
    }

    /// <summary>
    /// This method is for bot call, it is the same with OnMouseDown() method
    /// </summary>
    [Button]
    public void PlayThisCard()
    { if (readyToPlay)
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
        }

    }

    /// <summary>
    /// This is for player click to play card
    /// </summary>
    private void OnMouseDown() 
    {
        //Is not use for package
        if (!isForPackage)
        {
            if (readyToPlay && !isHighlighted)
            {
                EventController.OnHighlightCard(this);
            }
            else if (readyToPlay && isPlayerCard)
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
            }
        }
        else //Use for package
        {
            if (!hasClick)
            {
                Vector3 tempPos = transform.position;
                LeanTween.value(transform.position.y, transform.position.y + 2f, .5f).setEaseOutBack().setOnStart(() =>
                {
                    hasClick = true;
                }).setOnUpdate((float value) =>
                {
                    transform.position = new Vector3(transform.position.x, value, transform.position.z);
                }).setOnComplete(() =>
                {
                    LeanTween.value(transform.position.x, transform.position.x + 10, .5f).setOnUpdate((float value) =>
                    {
                        transform.position = new Vector3(value, transform.position.y, transform.position.z);
                    }).setOnComplete(() =>
                    {
                        gameObject.SetActive(false);
                        gameObject.transform.position = tempPos;
                        if (isLastItem)
                        {
                            EventController.OnGetLastCardEvent();
                        }
                    }).setEaseInBack();
                });
            }
        }
    }

    private void OnDisable()
    {
        EventController.CardReadyToPlay -= SetCardReadyToPlay;
        EventController.TurnTableCam -= StartSecondTurn; 
    }
}
