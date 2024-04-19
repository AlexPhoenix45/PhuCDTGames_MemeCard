using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Card Attributes
    [Header("Card Prefabs")]
    [Header("Attribute for Meme Card Battle")]
    public GameObject playingCardPrefabs;

    [Header("Card Stack (Drawing Card Position)")]
    public Transform playerCardStack;
    public Transform opponentCardStack;

    [Header("On-hand Card Position")]
    public Transform playerCardMidPos;
    public Transform playerCardLeftPos;
    public Transform playerCardRightPos;
    public Transform opponentCardMidPos;
    public Transform opponentCardLeftPos;
    public Transform opponentCardRightPos;

    [Header("Card Holder In Hierarchy")]
    public Transform playerCardHolder;
    public Transform opponentCardHolder;

    [Header("Card Placed Position")]
    public Transform playerPlacingPos;
    public Transform opponentPlacingPos;

    [Header("Card Hide Pos")]
    public Transform playerCardHidePos;

    [Header("Card Data")]
    public CardData[] cardDatas;

    //Private Attributes
    private GameObject playerCardMid, playerCardLeft, playerCardRight, opponentCardMid, opponentCardLeft, opponentCardRight;

    private GameObject playedCard;
    #endregion

    #region Questing Attributes
    public GameObject questionCardPrefab;

    public Transform questionCardStack;

    public Transform questionPos;

    [Header("Question Data")]
    public QuestionData[] questionDatas;

    private QuestionData currentQuestionData;
    #endregion

    #region Players Attributes
    //[HideInInspector]
    public int playerPoint;

    //[HideInInspector]
    public int opponentPoint;
    #endregion

    private void OnEnable()
    {
        //Subscribe to Events
        EventController.HideAndDrawCard += HideAndDrawCard;
        EventController.StartGame += StartGame;
        EventController.ExecutingPoint += ExecutingPoint;
        EventController.CardBattleTurnTwo += CardBattleTurnTwo;
        EventController.CardReadyToPlay += ShowCard;

        //Spawn a Game (need a random after testing)
        StartSpawn_CardBattle();
    }


    private void StartGame()
    {
        //Start a Game (need a parameter for which game is running)
        StartGame_CardBattle();
    }

    #region Meme Card Battle
    public void StartSpawn_CardBattle()
    {
        questionCardStack.gameObject.SetActive(true);
        playerCardStack.gameObject.SetActive(true);
        opponentCardStack.gameObject.SetActive(true);
    }

    public void StartGame_CardBattle()
    {
        DrawCard();
    }

    //--------Card-----------
    public void DrawCard()
    {
        IEnumerator MoveCardToHand()
        {
            //Mid Card
            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            playerCardMid.GetComponent<PlayingCard>().isPlayerCard = true;

            opponentCardMid = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            opponentCardMid.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardMid.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardMid.transform.position = playerCardStack.transform.position;
            playerCardMid.transform.rotation = playerCardStack.transform.rotation;
            playerCardMid.transform.LeanMove(playerCardMidPos.position, .75f);
            playerCardMid.transform.LeanRotate(playerCardMidPos.transform.eulerAngles, .75f);

            opponentCardMid.transform.position = opponentCardStack.transform.position;
            opponentCardMid.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            //Left Card
            playerCardLeft = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            opponentCardLeft = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            playerCardLeft.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            playerCardLeft.GetComponent<PlayingCard>().isPlayerCard = true;
            opponentCardLeft.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardLeft.transform.position = playerCardStack.transform.position;
            playerCardLeft.transform.rotation = playerCardStack.transform.rotation;
            playerCardLeft.transform.LeanMove(playerCardLeftPos.position, .75f);
            playerCardLeft.transform.LeanRotate(playerCardLeftPos.transform.eulerAngles, .75f);

            opponentCardLeft.transform.position = opponentCardStack.transform.position;
            opponentCardLeft.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            //Right Card
            playerCardRight = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            opponentCardRight = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            playerCardRight.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardRight.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            playerCardRight.GetComponent<PlayingCard>().isPlayerCard = true;
            opponentCardRight.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardRight.transform.rotation = playerCardStack.transform.rotation;
            playerCardRight.transform.position = playerCardStack.transform.position;
            playerCardRight.transform.LeanMove(playerCardRightPos.position, .75f);
            playerCardRight.transform.LeanRotate(playerCardRightPos.transform.eulerAngles, .75f);

            opponentCardRight.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardRight.transform.position = opponentCardStack.transform.position;
            opponentCardRight.transform.LeanMove(opponentCardRightPos.position, .75f);
            opponentCardRight.transform.LeanRotate(opponentCardRightPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            //Set Place Pos for Cards
            playerCardMid.GetComponent<PlayingCard>().placePos = playerPlacingPos;
            playerCardLeft.GetComponent<PlayingCard>().placePos = playerPlacingPos;
            playerCardRight.GetComponent<PlayingCard>().placePos = playerPlacingPos;

            opponentCardMid.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
            opponentCardLeft.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
            opponentCardRight.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
            yield return new WaitForSeconds(.75f);
            DrawQuestion();
        }
        StartCoroutine(MoveCardToHand());
    }

    public void DrawMissingCard(GameObject cardObj)
    {
        if (cardObj == playerCardMid)
        {
            playedCard = playerCardMid;

            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]); 
            playerCardMid.transform.position = playerCardStack.transform.position;
            playerCardMid.transform.rotation = playerCardStack.transform.rotation;
            playerCardMid.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardMid.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);
        }
        else if (cardObj == playerCardLeft)
        {
            playedCard = playerCardLeft;

            playerCardLeft = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            playerCardLeft.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            playerCardLeft.transform.position = playerCardStack.transform.position;
            playerCardLeft.transform.rotation = playerCardStack.transform.rotation;
            playerCardLeft.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardLeft.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);
        }
        else if (cardObj == playerCardRight)
        {
            playedCard = playerCardRight;

            playerCardRight = Instantiate(playingCardPrefabs, playerCardHolder, playerCardStack.transform);
            playerCardRight.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            playerCardRight.transform.position = playerCardStack.transform.position;
            playerCardRight.transform.rotation = playerCardStack.transform.rotation;
            playerCardRight.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardRight.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardMid)
        {
            playedCard = opponentCardMid;

            opponentCardMid = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            opponentCardMid.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardMid.transform.position = opponentCardStack.transform.position;
            opponentCardMid.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardLeft)
        {
            playedCard = opponentCardLeft;

            opponentCardLeft = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardLeft.transform.position = opponentCardStack.transform.position;
            opponentCardLeft.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardRight)
        {
            playedCard = opponentCardRight;

            opponentCardRight = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardStack.transform);
            opponentCardRight.GetComponent<PlayingCard>().SetCard(cardDatas[Random.Range(0, cardDatas.Length)]);
            opponentCardRight.transform.position = opponentCardStack.transform.position;
            opponentCardRight.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardRight.transform.LeanMove(opponentCardRightPos.position, .75f);
            opponentCardRight.transform.LeanRotate(opponentCardRightPos.transform.eulerAngles, .75f);
        }

        //Set Place Pos for Cards
        playerCardMid.GetComponent<PlayingCard>().placePos = playerPlacingPos;
        playerCardLeft.GetComponent<PlayingCard>().placePos = playerPlacingPos;
        playerCardRight.GetComponent<PlayingCard>().placePos = playerPlacingPos;

        opponentCardMid.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
        opponentCardLeft.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
        opponentCardRight.GetComponent<PlayingCard>().placePos = opponentPlacingPos;
    }
    
    /// <summary>
    /// Hide Remaining Cards on Hand
    /// </summary>
    public void HideAndDrawCard(GameObject cardObj)
    {
        if (cardObj == playerCardLeft)
        {
            playerCardRight.transform.LeanMove(playerCardHidePos.position, .5f);
            playerCardMid.transform.LeanMove(playerCardHidePos.position, .5f);
        }
        else if (cardObj == playerCardRight)
        {
            playerCardLeft.transform.LeanMove(playerCardHidePos.position, .5f);
            playerCardMid.transform.LeanMove(playerCardHidePos.position, .5f);
        }
        else if (cardObj == playerCardMid)
        {
            playerCardRight.transform.LeanMove(playerCardHidePos.position, .5f);
            playerCardLeft.transform.LeanMove(playerCardHidePos.position, .5f);
        }
        DrawMissingCard(cardObj);
    }

    public void ShowCard()
    {
        playerCardMid.transform.LeanMove(playerCardMidPos.position, .75f);
        playerCardMid.transform.LeanRotate(playerCardMidPos.transform.eulerAngles, .75f);
        playerCardLeft.transform.LeanMove(playerCardLeftPos.position, .75f);
        playerCardLeft.transform.LeanRotate(playerCardLeftPos.transform.eulerAngles, .75f);
        playerCardRight.transform.LeanMove(playerCardRightPos.position, .75f);
        playerCardRight.transform.LeanRotate(playerCardRightPos.transform.eulerAngles, .75f);
    }
    //--------Card-----------

    //--------Question-----------
    public void DrawQuestion()
    {
        GameObject question = Instantiate(questionCardPrefab, questionCardStack.transform);

        question.transform.LeanMove(questionPos.position, 1f);
        question.transform.LeanRotate(questionPos.transform.eulerAngles, 1f)
            .setOnStart(() => 
            {
                currentQuestionData = questionDatas[Random.Range(0, questionDatas.Length)];
                EventController.OnSetQuestion(currentQuestionData);
            })
            .setOnComplete(() =>
            {
                EventController.OnShowQuestion();
                Destroy(question);
            }
        );
    }
    //--------Question-----------
    #endregion

    #region Point Executer
    public void ExecutingPoint(PlayingCard card)
    {
        if (card.cardData.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
        {
            if (card.cardData.cardType == CardType.Common)
            {
                AddPoint(20);
            }
            else if (card.cardData.cardType == CardType.Rare)
            {
                AddPoint(30);
            }
            else if (card.cardData.cardType == CardType.Epic)
            {
                AddPoint(50);
            }
        }
        else
        {
            AddPoint(5);
        }

        void AddPoint(int addedPoint)
        {
            if (card.isPlayerCard)
            {
                playerPoint += addedPoint;
            }
            else
            {
                opponentPoint += addedPoint;
            }
        }
    }

    private void CardBattleTurnTwo()
    {
        DrawQuestion();
    }
    #endregion

    private void OnDisable()
    {
        EventController.HideAndDrawCard -= HideAndDrawCard;
        EventController.StartGame -= StartGame;
        EventController.ExecutingPoint -= ExecutingPoint;

    }
}
