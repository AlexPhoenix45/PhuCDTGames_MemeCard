using Assets.Core.Scripts.Core.Managers;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GameManager : MonoBehaviour
{
    #region Card Battle Attributes
    #region Card Attributes
    [Header("Card Attributes")]
    //[Header("Attribute for Meme Card Battle")]
    public GameObject playingCardPrefabs;

    //[Header("Card Stack (Drawing Card Position)")]
    public Transform playerCardDeck;
    public Transform opponentCardDeck;

    //[Header("On-hand Card Position")]
    public Transform playerCardMidPos;
    public Transform playerCardLeftPos;
    public Transform playerCardRightPos;
    public Transform opponentCardMidPos;
    public Transform opponentCardLeftPos;
    public Transform opponentCardRightPos;

    //[Header("Card Holder In Hierarchy")]
    public Transform playerCardHolder;
    public Transform opponentCardHolder;

    //[Header("Card Placed Position")]
    public Transform playerPlacingPos;
    public Transform opponentPlacingPos;

    //[Header("Card Hide Pos")]
    public Transform playerCardHidePos;

    //[Header("Card Data")]
    public CardData[] cardDatas;

    //Private Attributes
    private GameObject playerCardMid, playerCardLeft, playerCardRight, opponentCardMid, opponentCardLeft, opponentCardRight;

    private GameObject playerPlacedCard;
    private GameObject opponentPlacedCard;

    private int timeCardUnFitEmotion_Player = 0;
    private int timeCardUnFitEmotion_Opponent = 0;
    private int timeAllowGetUnFitEmotion_Player = 0;
    private int timeAllowGetUnFitEmotion_Opponent = 0;

    public static List<CardData> laughCardList = new List<CardData>();
    public static List<CardData> angryCardList = new List<CardData>();
    public static List<CardData> susCardList = new List<CardData>();
    public static List<CardData> cryCardList = new List<CardData>();
    public static List<CardData> surpriseCardList = new List<CardData>();
    public static List<CardData> coolCardList = new List<CardData>();

    #endregion

    #region Question Attributes
    public GameObject questionCardPrefab;

    public Transform questionCardDeck;

    public Transform questionPos;
    [Header("Question Data")]
    public QuestionData[] questionDatas;

    private QuestionData currentQuestionData;
    #endregion

    #region Players Attributes & Opponent
    [Header("Players Attributes & Opponent")]
    //Player
    private int playerPoint;

    //Opponent
    private int opponentPoint;

    private int opponentLvl;

    public List<BotBrain> audience = new List<BotBrain>();
    #endregion

    #region Game Attributes

    private int CardBattle_RoundCount = 0;

    public ParticleSystem smokeExplosionOpponent;
    public ParticleSystem smokeExplosionAudience;

    #endregion

    #region Point Attributes
    private readonly int commonCardPointMin = 10;
    private readonly int commonCardPointMax = 30;
    private readonly int rareCardPointMin = 30;
    private readonly int rareCardPointMax = 60;
    private readonly int epicCardPointMin = 60;
    private readonly int epicCardPointMax = 100;
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

        EventController.GenerateCardDataPackage += GetCardCollection;

        EventController.LoadPlayerOwnedCard += ImportPlayerOwnedCardData;
        EventController.AddPlayerOwnedCard += AddPlayerOwnedCardData;

        EventController.ShowBot += ShowBot;
        EventController.ChangeAudienceApperance += ChangeAudienceApperance;

        //Get Card data to generate card pack data
        EventController.GetCardData += PassCardData;

        //This method is for home button on-click reset
        EventController.ResetTable += CardBattle_GameOver;


        //Spawn a Game (need a random after testing)
        StartSpawn_CardBattle();

        //Attach CardID to Card ScriptableObject
        foreach (CardData card in cardDatas)
        {
            card.cardID = card.name;
        }

        IEnumerator LoadData()
        {
            yield return new WaitForSeconds(.1f);
            EventController.OnLoadPlayerData();
            EventController.OnUpdateEnvironment(PlayerDataStorage.Instance.data.currentLvl);
        }
        StartCoroutine(LoadData());

        ChangeAudienceApperance();

    }

    private void StartGame()
    {
        //Start a Game (need a parameter for which game is running)
        StartGame_CardBattle();
        ImportPlayerOwnedCardData();
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
        //ImportPlayerOwnedCardData();

        EventController.OnChooseOpponent();
        GenerateQuestionData();
        timeAllowGetUnFitEmotion_Player = UnityEngine.Random.Range(1, 4);
        timeAllowGetUnFitEmotion_Opponent = UnityEngine.Random.Range(1, 4);
        //Call choose opponent in UI Controller, then it will call DrawCard and RoundCount
        CardBattle_RoundCount++;
    }

    #region Level
    #endregion

    #region Card
    /// <summary>
    /// First round card drawing
    /// </summary>
    public void DrawCard(int lvl)
    {
        opponentLvl = lvl;

        IEnumerator MoveCardToHand()
        {
            DrawQuestion();

            yield return new WaitForSeconds(2);

            //Reset Generate Data
            timeCardUnFitEmotion_Player = 0;
            timeCardUnFitEmotion_Opponent = 0;

            //Mid Card
            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
            playerCardMid.GetComponent<PlayingCard>().isPlayerCard = true;

            opponentCardMid = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));
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
            playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));
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
            playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
            opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));
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
            EventController.OnBotSetCard(opponentCardMid, opponentCardLeft, opponentCardRight);
            EventController.OnCardReadyToPlay();
            EventController.OnShowHomeButton();
        }
        StartCoroutine(MoveCardToHand());
    }

    /// <summary>
    /// Draw missing card when played a card
    /// </summary>
    /// <param name="cardObj">This is the card that has played</param>
    public void DrawMissingCard(GameObject cardObj)
    {
        //Reset Generate Data
        timeCardUnFitEmotion_Player = 0;
        timeCardUnFitEmotion_Opponent = 0;

        if (cardObj == playerCardMid)
        {
            playerPlacedCard = playerCardMid;

            playerCardMid = Instantiate(playingCardPrefabs, playerCardHolder, playerCardDeck.transform);
            playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, true));
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
            playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, true));
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
            playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, true));
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
            opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, true));
            opponentCardMid.GetComponent<PlayingCard>().isPlayerCard = false; 
            opponentCardMid.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardLeft)
        {
            opponentPlacedCard = opponentCardLeft;

            opponentCardLeft = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, true));
            opponentCardLeft.GetComponent<PlayingCard>().isPlayerCard = false; 
            opponentCardLeft.transform.SetPositionAndRotation(opponentCardDeck.transform.position, opponentCardDeck.transform.rotation);
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, .75f);
        }
        else if (cardObj == opponentCardRight)
        {
            opponentPlacedCard = opponentCardRight;

            opponentCardRight = Instantiate(playingCardPrefabs, opponentCardHolder, opponentCardDeck.transform);
            opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, true));
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
        EventController.OnHideHomeButton();
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
        EventController.OnShowHomeButton();
        EventController.OnCardReadyToPlay();
    }

    /// <summary>
    /// Renew card data for second turn
    /// </summary>
    [Button]
    public void RenewCard()
    {
        //Reset Generate Data
        timeCardUnFitEmotion_Player = 0;
        timeCardUnFitEmotion_Opponent = 0;

        playerCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
        playerCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
        playerCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(true, false));
        opponentCardMid.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));
        opponentCardLeft.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));
        opponentCardRight.GetComponent<PlayingCard>().SetCard(GenerateCardData(false, false));

        //Reset Generate Data
        timeCardUnFitEmotion_Player = 0;
        timeCardUnFitEmotion_Opponent = 0;

        EventController.OnBotSetCard(opponentCardMid, opponentCardLeft, opponentCardRight);
    }

    /// <summary>
    /// Generate Card Data
    /// </summary>
    /// <returns></returns>
    public CardData GenerateCardData(bool isForPlayer, bool isDrawMissing)
    {
        //About rarity of the card
        CardData CardSpawningMechanism(bool isForced, bool isForOpponent)
        {
            if (!isForOpponent)
            {
                //Normal spawning
                if (!isForced)
                {
                    int percentage = UnityEngine.Random.Range(0, 100);

                    //Spawn Common card
                    if (percentage >= 0 && percentage < 50)
                    {
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                        {
                            if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common)
                            {
                                cardIndex.Add(i);
                            }
                        }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                        return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    //Spawn Rare card
                    else if (percentage >= 50 && percentage < 85)
                    {
                        List<int> cardIndex = new List<int>();

                        //Check if there is any card is Rare
                        bool isAnyRare = false;
                        foreach (CardData item in PlayerDataStorage.Instance.playerOwnedCard)
                        {
                            if (item.rarityType == RarityType.Rare)
                            {
                                isAnyRare = true;
                            }
                        }

                        //If there is a rare card
                        if (isAnyRare)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Rare)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If not, spawn common card instead
                        else
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                    }
                    //Spawn Epic card
                    else
                    {
                        List<int> cardIndex = new List<int>();

                        //Check if there is any card is Rare or Epic
                        bool isAnyRare = false;
                        bool isAnyEpic = false;
                        foreach (CardData item in PlayerDataStorage.Instance.playerOwnedCard)
                        {
                            if (item.rarityType == RarityType.Rare)
                            {
                                isAnyRare = true;
                            }
                            else if (item.rarityType == RarityType.Epic)
                            {
                                isAnyEpic = true;
                            }
                        }

                        //If there is a Epic
                        if (isAnyEpic)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Epic)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If there isn't an Epic, spawn Rare instead
                        else if (isAnyRare)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Rare)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If there isn't an Epic or Rare, spawn Common instead
                        else
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                    }
                }
                //Forced spawning (must be right emotion)
                else
                {
                    int percentage = UnityEngine.Random.Range(0, 100);

                    if (percentage >= 0 && percentage < 50)
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                        {
                            if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                cardIndex.Add(i);
                            }
                        }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                        return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    else if (percentage >= 50 && percentage < 85)
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();

                        //Check if there is any card is Rare
                        bool isAnyRare = false;
                        foreach (CardData item in PlayerDataStorage.Instance.playerOwnedCard)
                        {
                            if (item.rarityType == RarityType.Rare && item.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                isAnyRare = true;
                            }
                        }

                        //If there is a rare card
                        if (isAnyRare)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Rare && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If not, spawn common card instead
                        else
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        
                    }
                    else
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();

                        //Check if there is any card is Rare or Epic
                        bool isAnyRare = false;
                        bool isAnyEpic = false;
                        foreach (CardData item in PlayerDataStorage.Instance.playerOwnedCard)
                        {
                            if (item.rarityType == RarityType.Rare && item.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                isAnyRare = true;
                            }
                            else if (item.rarityType == RarityType.Epic && item.playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                isAnyEpic = true;
                            }
                        }

                        //If there is a Epic
                        if (isAnyEpic)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Epic && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                                {
                                    cardIndex.Add(i);
                                }
                            }
                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If there isn't an Epic, spawn Rare instead
                        else if (isAnyRare)
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Rare && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                        //If there isn't an Epic or Rare, spawn Common instead
                        else
                        {
                            for (int i = 0; i < PlayerDataStorage.Instance.playerOwnedCard.Count; i++)
                            {
                                if (PlayerDataStorage.Instance.playerOwnedCard[i].rarityType == RarityType.Common && PlayerDataStorage.Instance.playerOwnedCard[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                                {
                                    cardIndex.Add(i);
                                }
                            }

                            print(cardIndex[Random.Range(0, cardIndex.Count)]);
                            return PlayerDataStorage.Instance.playerOwnedCard[cardIndex[Random.Range(0, cardIndex.Count)]];
                        }
                    }
                }
            }
            else
            {
                if (!isForced)
                {
                    int percentage = UnityEngine.Random.Range(0, 100);

                    if (percentage >= 0 && percentage < cardRarityLvlExecuter(0))
                    {
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Common)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    else if (percentage >= cardRarityLvlExecuter(0) && percentage < cardRarityLvlExecuter(1))
                    {
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Rare)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    else
                    {
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Epic)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                }
                else
                {
                    int percentage = UnityEngine.Random.Range(0, 100);

                    if (percentage >= 0 && percentage < cardRarityLvlExecuter(0))
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Common && cardDatas[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    else if (percentage >= cardRarityLvlExecuter(0) && percentage < cardRarityLvlExecuter(1))
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Rare && cardDatas[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                    else
                    {
                        //print(currentQuestionData.questionCardEmotionalType);
                        List<int> cardIndex = new List<int>();
                        for (int i = 0; i < cardDatas.Length; i++)
                        {
                            if (cardDatas[i].rarityType == RarityType.Epic && cardDatas[i].playingCardEmotionalType == currentQuestionData.questionCardEmotionalType)
                            {
                                cardIndex.Add(i);
                            }
                        }

                        return cardDatas[cardIndex[Random.Range(0, cardIndex.Count)]];
                    }
                }

                int cardRarityLvlExecuter(int rarityType)
                {
                    int commonPercentage = 0, rarePercentage = 0;
                    if (opponentLvl == 5)
                    {
                        commonPercentage = 50;
                        rarePercentage = 35;
                    }
                    else if (opponentLvl == 10)
                    {
                        commonPercentage = 45;
                        rarePercentage = 40;
                    }
                    else if (opponentLvl == 15 || opponentLvl == 20)
                    {
                        commonPercentage = 40;
                        rarePercentage = 40;
                    }
                    else if (opponentLvl == 25 || opponentLvl == 30)
                    {
                        commonPercentage = 35;
                        rarePercentage = 40;
                    }
                    else if (opponentLvl == 35 || opponentLvl == 40)
                    {
                        commonPercentage = 30;
                        rarePercentage = 40;
                    }
                    else if (opponentLvl == 45 || opponentLvl == 50)
                    {
                        commonPercentage = 25;
                        rarePercentage = 40;
                    }
                    else if (opponentLvl == 55 || opponentLvl == 60)
                    {
                        commonPercentage = 10;
                        rarePercentage = 50;
                    }
                    else if (opponentLvl > 0 && opponentLvl < 15)
                    {
                        commonPercentage = 70;
                        rarePercentage = 20;
                    }
                    else if (opponentLvl > 15 && opponentLvl < 30)
                    {
                        commonPercentage = 60;
                        rarePercentage = 25;
                    }
                    else if (opponentLvl > 30 && opponentLvl < 45)
                    {
                        commonPercentage = 50;
                        rarePercentage = 30;
                    }
                    else if (opponentLvl > 45 && opponentLvl < 60)
                    {
                        commonPercentage = 35;
                        rarePercentage = 40;
                    }

                    //Return part
                    if (rarityType == 0)
                    {
                        //print(commonPercentage);
                        return commonPercentage;
                    }
                    else if (rarityType == 1)
                    {
                        //print(commonPercentage + rarePercentage);
                        return commonPercentage + rarePercentage;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        } //Rarity spawn

        bool DuplicateCheck(CardData tempCard)
        {
            bool returnedValue = true;
            if (isForPlayer)
            {
                if (playerCardMid != null && tempCard == playerCardMid.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                    print("mid card duplicate");
                }
                if (playerCardLeft != null && tempCard == playerCardLeft.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                    print("left card duplicate");
                }
                if (playerCardRight != null && tempCard == playerCardRight.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                    print("right card duplicate");
                }
            }
            else
            {
                if (opponentCardMid != null && tempCard == opponentCardMid.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                }
                if (opponentCardLeft != null && tempCard == opponentCardLeft.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                }
                if (opponentCardRight != null && tempCard == opponentCardRight.GetComponent<PlayingCard>().cardData)
                {
                    returnedValue = false;
                }
            }

            return returnedValue;
        } //Duplicate check

        CardData tempCard;

        if (isForPlayer)
        {
            if (!isDrawMissing)
            {
                do
                {
                    tempCard = CardSpawningMechanism(false, false);
                }
                while (!DuplicateCheck(tempCard));

                if (tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType)
                {
                    timeCardUnFitEmotion_Player++;
                }

                if (timeCardUnFitEmotion_Player == timeAllowGetUnFitEmotion_Player)
                {
                    tempCard = CardSpawningMechanism(true, false);
                }

                return tempCard;
            }
            else
            {
                tempCard = CardSpawningMechanism(false, false);
                return tempCard;
            }
        }
        else
        {
            do
            {
                tempCard = CardSpawningMechanism(false, true);
            }
            while (!DuplicateCheck(tempCard));

            if (tempCard.playingCardEmotionalType != currentQuestionData.questionCardEmotionalType)
            {
                timeCardUnFitEmotion_Opponent++;
            }

            if (timeCardUnFitEmotion_Opponent == timeAllowGetUnFitEmotion_Opponent)
            {
                tempCard = CardSpawningMechanism(true, true);
            }

            return tempCard;
        }
    }

    public void ImportPlayerOwnedCardData()
    {
        //List<int> removedIndex = new List<int>();
        //for (int i = 0; i < PlayerDataStorage.Instance.data.cardOwned.Count - 1; i++)
        //{
        //    for (int j = i + 1; j < PlayerDataStorage.Instance.data.cardOwned.Count; j++)
        //    {
        //        if (PlayerDataStorage.Instance.data.cardOwned[i].cardName == PlayerDataStorage.Instance.data.cardOwned[j].cardName && PlayerDataStorage.Instance.data.cardOwned[i].cardRarity == PlayerDataStorage.Instance.data.cardOwned[j].cardRarity)
        //        {
        //            removedIndex.Add(j);
        //        }
        //    }
        //}

        //int temp = 0;
        //foreach (int index in removedIndex)
        //{
        //    PlayerDataStorage.Instance.data.cardOwned.Remove(PlayerDataStorage.Instance.data.cardOwned[index - temp]);
        //    temp--;
        //}

        //Import player data .json file to CardData types
        List<CardData> allCards = cardDatas.ToList();

        PlayerDataStorage.Instance.playerOwnedCard.Clear();

        foreach (CardOwned playerCardOwned in PlayerDataStorage.Instance.data.cardOwned)
        {
            CardData tempCard = allCards.Where(obj => obj.cardID == playerCardOwned.cardName).SingleOrDefault();
            CardData card = Instantiate(tempCard);

            if (playerCardOwned.cardRarity == 0)
            {
                card.rarityType = RarityType.Common;
                card.memeImage = tempCard.memeImage;
            }
            else if (playerCardOwned.cardRarity == 1)
            {
                card.rarityType = RarityType.Rare;
                card.memeImage = tempCard.memeImage;
            }
            else if (playerCardOwned.cardRarity == 2)
            {
                card.rarityType = RarityType.Epic;
                card.memeGif = tempCard.memeGif;
            }

            //This part is make playerOwnedCard<CardData> doesn't have any duplicated card
            bool canBeAdded = true;
            foreach (CardData cardData in PlayerDataStorage.Instance.playerOwnedCard) 
            { 
                if (card.rarityType == cardData.rarityType && card.cardID == cardData.cardID)
                {
                    canBeAdded = false;
                }
            }

            if (canBeAdded)
            {
                PlayerDataStorage.Instance.playerOwnedCard.Add(card);
            }
        }
    }

    public void AddPlayerOwnedCardData(CardData cardData)
    {
        //Check if is there any duplicate card, if that's true, break the operation
        foreach (CardData card in PlayerDataStorage.Instance.playerOwnedCard)
        {
            if (cardData == card)
            {
                print("aaaaaaaaaaaaaaaaaaaaa");
                return;
            }
        }

        CardOwned tempCardOwned = new CardOwned();

        tempCardOwned.cardName = cardData.cardID.ToString();

        if (cardData.rarityType == RarityType.Common)
        {
            tempCardOwned.cardRarity = 0;
        }
        else if (cardData.rarityType == RarityType.Rare)
        {
            tempCardOwned.cardRarity = 1;
        }
        else if (cardData.rarityType == RarityType.Epic)
        {
            tempCardOwned.cardRarity = 2;
        }

        PlayerDataStorage.Instance.data.cardOwned.Add(tempCardOwned);
        EventController.OnSavePlayerData();
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

    private void GetCardCollection()
    {
        EventController.OnLoadPlayerOwnedCard();
        laughCardList = new List<CardData>();
        angryCardList = new List<CardData>();
        susCardList = new List<CardData>();
        cryCardList = new List<CardData>();
        surpriseCardList = new List<CardData>();
        coolCardList = new List<CardData>();

        //Common card Filter
        foreach (CardData card in PlayerDataStorage.Instance.playerOwnedCard)
        {
            if (card.playingCardEmotionalType == EmotionalType.Laugh && card.rarityType == RarityType.Common)
            {
                laughCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Angry && card.rarityType == RarityType.Common)
            {
                angryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Sus && card.rarityType == RarityType.Common)
            {
                susCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cry && card.rarityType == RarityType.Common)
            {
                cryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Surprise && card.rarityType == RarityType.Common)
            {
                surpriseCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cool && card.rarityType == RarityType.Common)
            {
                coolCardList.Add(card);
            }
        }

        foreach (CardData card in PlayerDataStorage.Instance.playerOwnedCard)
        {
            if (card.playingCardEmotionalType == EmotionalType.Laugh && card.rarityType == RarityType.Rare)
            {
                laughCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Angry && card.rarityType == RarityType.Rare)
            {
                angryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Sus && card.rarityType == RarityType.Rare)
            {
                susCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cry && card.rarityType == RarityType.Rare)
            {
                cryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Surprise && card.rarityType == RarityType.Rare)
            {
                surpriseCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cool && card.rarityType == RarityType.Rare)
            {
                coolCardList.Add(card);
            }
        }

        foreach (CardData card in PlayerDataStorage.Instance.playerOwnedCard)
        {
            if (card.playingCardEmotionalType == EmotionalType.Laugh && card.rarityType == RarityType.Epic)
            {
                laughCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Angry && card.rarityType == RarityType.Epic)
            {
                angryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Sus && card.rarityType == RarityType.Epic)
            {
                susCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cry && card.rarityType == RarityType.Epic)
            {
                cryCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Surprise && card.rarityType == RarityType.Epic)
            {
                surpriseCardList.Add(card);
            }
            else if (card.playingCardEmotionalType == EmotionalType.Cool && card.rarityType == RarityType.Epic)
            {
                coolCardList.Add(card);
            }
        }

        //print("laughCardList " + laughCardList.Count);
        //print("angryCardList " + angryCardList.Count);
        //print("susCardList " + susCardList.Count);
        //print("cryCardList " + cryCardList.Count);
        //print("coolCardList " + coolCardList.Count);
    }

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
        if (opponentLvl % 5 != 0)
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
        else
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
                if (CardBattle_RoundCount <= 3)
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
                playerPoint += tempPoint;
                EventController.OnBotPlayEmotionAnim(tempPoint);
            }
            else
            {
                int tempPoint = UnityEngine.Random.Range(min, max + 1);
                opponentPoint += tempPoint;
                EventController.OnBotPlayEmotionAnim(tempPoint);
            }
        }
    }
    //----------Point Executer------------------
    #endregion

    #region Fxs
    private void PlayExplosionOpponent()
    {
        smokeExplosionOpponent.Play();
    }

    private void PlayExplosionAudience()
    {
        smokeExplosionAudience.Play();
    }
    #endregion

    #region Bot
    private void ShowBot(OpponentData oppoData)
    {
        foreach (BotBrain bot in audience)
        {
            if (bot.isOpponent)
            {
                StartCoroutine(wait());
                IEnumerator wait()
                {
                    PlayExplosionOpponent();
                    yield return new WaitForSeconds(.5f);
                    bot.gameObject.SetActive(true);
                    bot.GenerateOpponentApperance(oppoData);
                }
            }
        }
    }

    private void ChangeAudienceApperance()
    {
        //Set up bot position & apperance
        PlayExplosionOpponent();
        PlayExplosionAudience();

        foreach (BotBrain bot in audience)
        {
            if (!bot.isOpponent)
            {
                bot.GenerateAudienceApperance();
            }
            else
            {
                bot.gameObject.SetActive(false);
            }
        }
    }
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

    #region Pack Opening
    [Button]
    public void PassCardData()
    {
        EventController.OnGeneratePackCards(cardDatas);
    }
    #endregion

    private void OnDisable()
    {
        EventController.HideAndDrawCard -= HideAndDrawCard;
        EventController.StartGame -= StartGame;
        EventController.ExecutingPoint -= ExecutingPoint;

    }

    /// <summary>
    /// On application quit, save player data
    /// </summary>
    private void OnApplicationQuit()
    {
        EventController.OnSavePlayerData();
    }
}
