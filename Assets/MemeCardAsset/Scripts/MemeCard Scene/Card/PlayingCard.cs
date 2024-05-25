using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using static UnityEngine.Rendering.DebugUI;

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

    public Sprite common2dSprite;
    public Sprite rare2dSprite;
    public Sprite epic2dSprite;

    public SpriteRenderer rarityRenderer;

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
        SetCard(cardData);
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
                memeBorder.material = epicMat;
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
            if (readyToPlay && !isHighlighted && !isPlayed)
            {
                EventController.OnHighlightCard(this);
            }
            else if (readyToPlay && isPlayerCard && !isPlayed)
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
                    LeanTween.value(0, 1, .25f).setOnStart(() =>
                    {
                        rarityRenderer.color = Vector4.zero;
                        rarityRenderer.gameObject.SetActive(true);

                        if (cardData.rarityType == RarityType.Common)
                        {
                            rarityRenderer.sprite = common2dSprite;
                        }
                        else if (cardData.rarityType == RarityType.Rare)
                        {
                            rarityRenderer.sprite = rare2dSprite;
                        }
                        else if (cardData.rarityType == RarityType.Epic)
                        {
                            rarityRenderer.sprite = epic2dSprite;
                        }
                    }).setOnUpdate((float value) =>
                    {
                        rarityRenderer.color = new Vector4(1, 1, 1, value);
                    }).setOnComplete(() =>
                    {
                        rarityRenderer.color = new Vector4(1, 1, 1, 1);
                    });
                }).setOnUpdate((float value) =>
                {
                    transform.position = new Vector3(transform.position.x, value, transform.position.z);
                }).setOnComplete(() =>
                {
                    EventController.OnNextCardCheck(cardData);

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
        rarityRenderer.gameObject.SetActive(false);
    }
}
