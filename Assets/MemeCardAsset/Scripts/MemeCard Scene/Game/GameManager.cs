using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Card Battle Attributes
    #region Card Attributes
    [Header("Card Prefabs")]
    [Header("Attribute for Meme Card Battle")]
    public GameObject playingCardPrefabs;

    [Header("Card Stack (Drawing Card Position)")]
    public Transform playerCardDeck;
    public Transform opponentCardDeck;

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

    private GameObject playerPlacedCard;
    private GameObject opponentPlacedCard;

    public int timeCardUnFitEmotion_Player = 0;
    public int timeCardUnFitEmotion_Opponent = 0;
    public int timeAllowGetUnFitEmotion_Player = 0;
    public int timeAllowGetUnFitEmotion_Opponent = 0;
    #endregion

    #region Question Attributes
    public GameObject questionCardPrefab;

    public Transform questionCardDeck;

    public Transform questionPos;
    [Header("Question Data")]
    public QuestionData[] questionDatas;

    private QuestionData currentQuestionData;
    #endregion

    #region Players Attributes
    //[HideInInspector]
    private int playerPoint;

    //[HideInInspector]
    private int opponentPoint;
    #endregion

    #region Game Attributes

    private int CardBattle_RoundCount = 0;

    #endregion

    #region Point Attributes
    private int commonCardPointMin = 10;
    private int commonCardPointMax = 30;
    private int rareCardPointMin = 30;
    private int rareCardPointMax = 60;
    private int epicCardPointMin = 60;
    private int epicCardPointMax = 100;
    #endregion

    #endregion

    private void OnEnable()
    {
        //Subscribe to Events
        EventController.HideAndDrawCard += HideAndDrawCard;
        EventController.StartGame += StartGame;
        EventController.ExecutingPoint += ExecutingPoint;
        EventController.CardBattleTurnTwo += CardBattleTurnTwo;
        EventController.CardBattleNextTurn += ShowCard;
        EventController.CardBattleNextTurn += RenewCard; //This is called for second turn
        EventController.SpawnGameOnTable += StartSpawn_CardBattle; //need to make it random

        EventController.DrawStartingCard += DrawCard; //Draw Starting card, called by ChoosingOpponent in UI Controller

        EventController.HighlightCard += HighlightCard;

        //doi cardbattleturntwo thanh` turntwoafter drawing question

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
        questionCardDeck.gameObject.SetActive(true);
        playerCardDeck.gameObject.SetActive(true);
        opponentCardDeck.gameObject.SetActive(true);
    }

    public void StartGame_CardBattle()
    {
        EventController.OnChooseOpponent();
        GenerateQuestionData();
        timeAllowGetUnFitEmotion_Player = UnityEngine.Random.Range(1, 4);
        timeAllowGetUnFitEmotion_Opponent = UnityEngine.Random.Range(1, 4);
        //Call choose opponent in UI Controller, then it will call DrawCard and RoundCount
        CardBattle_RoundCount++;
    }

    #region Card
    //--------Card-----------
    /// <summary>
    /// First round card drawing
    /// </summary>
    public void DrawCard()
    {
        IEnumerator MoveCardToHand()
        {
            //Mid Card
            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            playerCardMid.GetComponent<PlayingCard>().isPlayerCard = true;

            opponentCardMid = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            opponentCardMid.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardMid.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardMid.transform.LeanMove(playerCardMidPos.position, .75f);
            playerCardMid.transform.LeanRotate(playerCardMidPos.transform.eulerAngles, .75f);

            opponentCardMid.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            //Left Card
            playerCardLeft = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            opponentCardLeft = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            playerCardLeft.GetComponent<PlayingCard>().isPlayerCard = true;
            opponentCardLeft.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardLeft.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardLeft.transform.LeanMove(playerCardLeftPos.position, .75f);
            playerCardLeft.transform.LeanRotate(playerCardLeftPos.transform.eulerAngles, .75f);

            opponentCardLeft.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            //Right Card
            playerCardRight = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            opponentCardRight = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            playerCardRight.GetComponent<PlayingCard>().isPlayerCard = true;
            opponentCardRight.GetComponent<PlayingCard>().isPlayerCard = false;

            playerCardRight.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardRight.transform.LeanMove(playerCardRightPos.position, .75f);
            playerCardRight.transform.LeanRotate(playerCardRightPos.transform.eulerAngles, .75f);

            opponentCardRight.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
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

            //Reset Generate Data
            timeCardUnFitEmotion_Player = 0;
            timeCardUnFitEmotion_Opponent = 0;

            yield return new WaitForSeconds(.75f);
            DrawQuestion();
            EventController.OnBotSetCard(opponentCardMid, opponentCardLeft, opponentCardRight);
        }
        StartCoroutine(MoveCardToHand());
    }

    /// <summary>
    /// Draw missing card when played a card
    /// </summary>
    /// <param name="cardObj">This is the card that has played</param>
    public void DrawMissingCard(GameObject cardObj)
    {
        if (cardObj == playerCardMid)
        {
            playerPlacedCard = playerCardMid;

            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            playerCardMid.GetComponent<PlayingCard>().isPlayerCard = true; 
            playerCardMid.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardMid.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardMid.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);

            EventController.OnBotPlay();
        }
        else if (cardObj == playerCardLeft)
        {
            playerPlacedCard = playerCardLeft;

            playerCardLeft = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            playerCardLeft.GetComponent<PlayingCard>().isPlayerCard = true; 
            playerCardLeft.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardLeft.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardLeft.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);

            EventController.OnBotPlay();
        }
        else if (cardObj == playerCardRight)
        {
            playerPlacedCard = playerCardRight;

            playerCardRight = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
            playerCardRight.GetComponent<PlayingCard>().isPlayerCard = true; 
            playerCardRight.transform.SetPositionAndRotation(playerCardDeck.transform.position, playerCardDeck.transform.rotation);
            playerCardRight.transform.LeanMove(playerCardHidePos.position, .75f);
            playerCardRight.transform.LeanRotate(playerCardHidePos.transform.eulerAngles, .75f);

            EventController.OnBotPlay();
        }
        else if (cardObj == opponentCardMid)
        {
            opponentPlacedCard = opponentCardMid;

            opponentCardMid = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            opponentCardMid.GetComponent<PlayingCard>().isPlayerCard = false; 
            opponentCardMid.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardLeft)
        {
            opponentPlacedCard = opponentCardLeft;

            opponentCardLeft = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            opponentCardLeft.GetComponent<PlayingCard>().isPlayerCard = false; 
            opponentCardLeft.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardRight)
        {
            opponentPlacedCard = opponentCardRight;

            opponentCardRight = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
            opponentCardRight.GetComponent<PlayingCard>().isPlayerCard = false; 
            opponentCardRight.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
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

        //Reset Generate Data
        timeCardUnFitEmotion_Player = 0;
        timeCardUnFitEmotion_Opponent = 0;
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

    /// <summary>
    /// This is called to show player's card every turn
    /// </summary>
    public void ShowCard() 
    {
        Destroy(playerPlacedCard);
        Destroy(opponentPlacedCard);
        playerCardMid.transform.LeanMove(playerCardMidPos.position, .75f);
        playerCardMid.transform.LeanRotate(playerCardMidPos.transform.eulerAngles, .75f);
        playerCardLeft.transform.LeanMove(playerCardLeftPos.position, .75f);
        playerCardLeft.transform.LeanRotate(playerCardLeftPos.transform.eulerAngles, .75f);
        playerCardRight.transform.LeanMove(playerCardRightPos.position, .75f);
        playerCardRight.transform.LeanRotate(playerCardRightPos.transform.eulerAngles, .75f);
    }

    /// <summary>
    /// Renew card data for second turn
    /// </summary>
    [Button]
    public void RenewCard()
    {
        playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
        playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
        playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true));
        opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
        opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));
        opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false));

        //Reset Generate Data
        timeCardUnFitEmotion_Player = 0;
        timeCardUnFitEmotion_Opponent = 0;

        EventController.OnBotSetCard(opponentCardMid, opponentCardLeft, opponentCardRight);
    }

    /// <summary>
    /// Generate Card Data
    /// </summary>
    /// <returns></returns>
    public CardData GenerateCardData(bool isForPlayer)
    {
        //About rarity of the card
        CardData CardSpawningMechanism(bool isForced)
        {
            if (!isForced)
            {
                int percentage = UnityEngine.Random.Range(0, 100);

                CardData tempCard; //create new empty card

                if (percentage >= 0 && percentage < 50)
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Common);
                }
                else if (percentage >= 50 && percentage < 85)
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Rare);
                }
                else
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Epic);
                }

                return tempCard;
            }
            else
            {
                int percentage = UnityEngine.Random.Range(0, 100);

                CardData tempCard; //create new empty card

                if (percentage >= 0 && percentage < 50)
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Common || tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType);
                    print(tempCard.playingCardEmotionalType + " " + currentQuestionData.questionCardEmotionalType);
                }
                else if (percentage >= 50 && percentage < 85)
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Rare || tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType);
                    print(tempCard.playingCardEmotionalType + " " + currentQuestionData.questionCardEmotionalType);
                }
                else
                {
                    do
                    {
                        tempCard = cardDatas[Random.Range(0, cardDatas.Length)];
                    }
                    while (tempCard.rarityType != RarityType.Epic || tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType);
                    print(tempCard.playingCardEmotionalType + " " + currentQuestionData.questionCardEmotionalType);
                }

                return tempCard;
            }
        } //Rarity spawn

        bool DuplicateCheck(CardData tempCard, bool isforPlayer)
        {
            if (isForPlayer)
            {
                if (playerCardMid != null && tempCard == playerCardMid.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else if (playerCardLeft != null && tempCard == playerCardLeft.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else if (playerCardRight != null && tempCard == playerCardRight.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (opponentCardMid != null && tempCard == opponentCardMid.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else if (opponentCardLeft != null && tempCard == opponentCardLeft.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else if (opponentCardRight != null && tempCard == opponentCardRight.GetComponent<PlayingCard>().cardData)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        } //Duplicate check

        CardData tempCard;

        if (isForPlayer)
        {
            do
            {
                tempCard = CardSpawningMechanism(false);
            }
            while (!DuplicateCheck(tempCard, true));

            if (tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType)
            {
                timeCardUnFitEmotion_Player++;
            }

            if (timeCardUnFitEmotion_Player == timeAllowGetUnFitEmotion_Player)
            {
                tempCard = CardSpawningMechanism(true);
            }
            return tempCard;
        }
        else
        {
            do
            {
                tempCard = CardSpawningMechanism(false);
            }
            while (!DuplicateCheck(tempCard, false));

            if (tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType)
            {
                timeCardUnFitEmotion_Opponent++;
            }

            if (timeCardUnFitEmotion_Opponent == timeAllowGetUnFitEmotion_Opponent)
            {
                tempCard = CardSpawningMechanism(true);
            }
            return tempCard;
        }
    }

    public void HighlightCard(PlayingCard playingCard)
    {
        if (playingCard.gameObject == playerCardMid)
        {
            playerCardMid.GetComponent<PlayingCard>().isHighlighted = true;
            playerCardMid.LeanMove(new Vector3(playerCardMid.transform.position.x, playerCardMid.transform.position.y + 2, playerCardMid.transform.position.z - 2), .25f);

            if (playerCardLeft.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardLeft.LeanMove(playerCardLeftPos.transform.position, .25f);
                playerCardLeft.GetComponent<PlayingCard>().isHighlighted = false;   
            }
            else if (playerCardRight.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardRight.LeanMove(playerCardRightPos.transform.position, .25f);
                playerCardRight.GetComponent<PlayingCard>().isHighlighted = false;
            }
        }
        else if (playingCard.gameObject == playerCardLeft)
        {
            playerCardLeft.GetComponent<PlayingCard>().isHighlighted = true;
            playerCardLeft.LeanMove(new Vector3(playerCardLeft.transform.position.x, playerCardLeft.transform.position.y + 2, playerCardLeft.transform.position.z - 2), .25f);

            if (playerCardMid.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardMid.LeanMove(playerCardMidPos.transform.position, .25f);
                playerCardMid.GetComponent<PlayingCard>().isHighlighted = false;
            }
            else if (playerCardRight.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardRight.LeanMove(playerCardRightPos.transform.position, .25f);
                playerCardRight.GetComponent<PlayingCard>().isHighlighted = false;
            }
        }
        else if (playingCard.gameObject == playerCardRight)
        {
            playerCardRight.GetComponent<PlayingCard>().isHighlighted = true;
            playerCardRight.LeanMove(new Vector3(playerCardRight.transform.position.x, playerCardRight.transform.position.y + 2, playerCardRight.transform.position.z - 2), .25f);

            if (playerCardLeft.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardLeft.LeanMove(playerCardLeftPos.transform.position, .25f);
                playerCardLeft.GetComponent<PlayingCard>().isHighlighted = false;
            }
            else if (playerCardMid.GetComponent<PlayingCard>().isHighlighted)
            {
                playerCardMid.LeanMove(playerCardMidPos.transform.position, .25f);
                playerCardMid.GetComponent<PlayingCard>().isHighlighted = false;
            }
        }
    }

    //--------Card-----------
    #endregion

    #region Question
    //--------Question-----------
    /// <summary>
    /// Draw new question, but this is just 3d animation
    /// </summary>
    public void DrawQuestion()
    {
        GameObject question = Instantiate(questionCardPrefab, questionCardDeck.transform);

        question.transform.LeanMove(questionPos.position, 1f);
        question.transform.LeanRotate(questionPos.transform.eulerAngles, 1f)
            .setOnStart(() => 
            {
                if (CardBattle_RoundCount > 1)
                {
                    GenerateQuestionData();
                }
                EventController.OnSetQuestion(currentQuestionData);
            })
            .setOnComplete(() =>
            {
                EventController.OnShowQuestion(CardBattle_RoundCount);
                Destroy(question);
            }
        );
    }
    //--------Question-----------

    public void GenerateQuestionData()
    {
        currentQuestionData = questionDatas[Random.Range(0, questionDatas.Length)];
    }
    #endregion

    #region Next Turn and Game Over Handler
    //-------Next Turn Handler-------------
    /// <summary>
    /// This is judger to call second turn or not
    /// </summary>
    private void CardBattleTurnTwo()
    {
        if (playerPoint == opponentPoint)
        {
            DrawQuestion();
            CardBattle_RoundCount++;

            //This part is or generating card
            timeCardUnFitEmotion_Player = 0;
            timeCardUnFitEmotion_Opponent = 0;
            timeAllowGetUnFitEmotion_Player = UnityEngine.Random.Range(1, 4); //From 1 - 3
            timeAllowGetUnFitEmotion_Opponent = UnityEngine.Random.Range(1, 4); //From 1 - 3
        }
        else
        {
            if (CardBattle_RoundCount <= 1)
            {
                DrawQuestion();
                CardBattle_RoundCount++;

                //This part is or generating card
                timeCardUnFitEmotion_Player = 0;
                timeCardUnFitEmotion_Opponent = 0;
                timeAllowGetUnFitEmotion_Player = UnityEngine.Random.Range(1, 4); //From 1 - 3
                timeAllowGetUnFitEmotion_Opponent = UnityEngine.Random.Range(1, 4); //From 1 - 3
            }
            else
            {
                timeCardUnFitEmotion_Player = 0;
                timeCardUnFitEmotion_Opponent = 0;
                timeAllowGetUnFitEmotion_Player = UnityEngine.Random.Range(1, 4); //From 1 - 3
                timeAllowGetUnFitEmotion_Opponent = UnityEngine.Random.Range(1, 4); //From 1 - 3

                if (playerPoint > opponentPoint)
                {
                    EventController.OnCardBattleGameOver(true);
                }
                else
                {
                    EventController.OnCardBattleGameOver(false);
                }
                CardBattle_GameOver();
            }
        }
    }

    //-------Next Turn Handler-------------
    #endregion

    #region Point Executer
    //----------Point Executer------------------
    /// <summary>
    /// Calculating point
    /// </summary>
    /// <param name="card"></param>
    public void ExecutingPoint(PlayingCard card)
    {
        //Executign point based on rarity and emotional fitting
        if (card.cardData.rarityType == RarityType.Common)
        {
            if (card.cardData.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
            {
                AddPoint(commonCardPointMin, commonCardPointMax);
            }
            else
            {
                AddPoint(commonCardPointMin / 2, commonCardPointMax / 2);
            }
        }
        else if (card.cardData.rarityType == RarityType.Rare)
        {
            if (card.cardData.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
            {
                AddPoint(rareCardPointMin, rareCardPointMax);
            }
            else
            {
                AddPoint(rareCardPointMin / 2, rareCardPointMax / 2);
            }
        }
        else if (card.cardData.rarityType == RarityType.Epic)
        {
            if (card.cardData.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
            {
                AddPoint(epicCardPointMin, epicCardPointMax);
            }
            else
            {
                AddPoint(epicCardPointMin / 2, epicCardPointMax / 2);
            }
        }

        EventController.OnExecutingPointToUI(playerPoint, opponentPoint);

        void AddPoint(int min, int max)
        {
            if (card.isPlayerCard)
            {
                int tempPoint = UnityEngine.Random.Range(min, max + 1);
                print(tempPoint);
                playerPoint += tempPoint;
            }
            else
            {
                int tempPoint = UnityEngine.Random.Range(min, max + 1);
                print(tempPoint);
                opponentPoint += tempPoint;
            }
        }
    }
    //----------Point Executer------------------
    #endregion

    /// <summary>
    /// This help cleaning cards, card decks, and card on players hand
    /// </summary>
    private void CardBattle_GameOver()
    {
        Destroy(playerCardMid);
        Destroy(playerCardLeft);
        Destroy(playerCardRight);
        Destroy(opponentCardMid);
        Destroy(opponentCardLeft);
        Destroy(opponentCardRight);

        Destroy(playerPlacedCard);
        Destroy(opponentPlacedCard);

        playerCardDeck.gameObject.SetActive(false);
        opponentCardDeck.gameObject.SetActive(false);
        questionCardDeck.gameObject.SetActive(false);

        playerPoint = 0;
        opponentPoint = 0;
        CardBattle_RoundCount = 0;
    }

    #endregion

    private void OnDisable()
    {
        EventController.HideAndDrawCard -= HideAndDrawCard;
        EventController.StartGame -= StartGame;
        EventController.ExecutingPoint -= ExecutingPoint;

    }
}
