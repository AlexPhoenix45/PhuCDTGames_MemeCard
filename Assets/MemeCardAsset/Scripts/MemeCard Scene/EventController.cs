using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventController
{
    #region Card - Card Battle Game
    /// <summary>
    /// Draw Card
    /// </summary>
    public static event UnityAction<GameObject> HideAndDrawCard;
    public static void OnHideAndDrawCard(GameObject cardObj) => HideAndDrawCard?.Invoke(cardObj);

    /// <summary>
    /// Draw Starting Card for Card Battle Game, call by choosing opponent in UI controller
    /// </summary>
    public static event UnityAction <int> DrawStartingCard;
    public static void OnDrawStartingCard(int opponentData) => DrawStartingCard?.Invoke(opponentData);

    /// <summary>
    /// Start Game - Card Battle
    /// </summary>
    public static event UnityAction StartGame;
    public static void OnStartGame() => StartGame?.Invoke();

    /// <summary>
    /// Allow player to click the card
    /// </summary>
    public static event UnityAction CardReadyToPlay;
    public static void OnCardReadyToPlay() => CardReadyToPlay?.Invoke();

    /// <summary>
    /// For drawing question of next turn
    /// </summary>
    public static event UnityAction CardBattleTurnTwo;
    public static void OnCardBattleTurnTwo() => CardBattleTurnTwo?.Invoke(); 

    /// <summary>
    /// Next turn after drawing question
    /// </summary>
    public static event UnityAction CardBattleNextTurn;
    public static void OnCardBattleNextTurn() => CardBattleNextTurn?.Invoke();

    /// <summary>
    /// Card Battle Game Over call
    /// </summary>
    public static event UnityAction <bool> CardBattleGameOver;
    public static void OnCardBattleGameOver(bool isPlayerWin) => CardBattleGameOver?.Invoke(isPlayerWin);

    /// <summary>
    /// Make the card appearance higher than the others
    /// </summary>
    public static event UnityAction <PlayingCard> HighlightCard;
    public static void OnHighlightCard(PlayingCard playingCard) => HighlightCard?.Invoke(playingCard);

    /// <summary>
    /// Start Choose opponent animation
    /// </summary>
    public static event UnityAction ChooseOpponent;
    public static void OnChooseOpponent() => ChooseOpponent?.Invoke();

    public static event UnityAction GetCardCollection;
    public static void OnGetCardCollection() => GetCardCollection?.Invoke();
    #endregion

    #region Question
    /// <summary>
    /// Show UI question
    /// </summary>
    public static event UnityAction <int> ShowQuestion;
    public static void OnShowQuestion(int roundCount) => ShowQuestion?.Invoke(roundCount);

    /// <summary>
    /// Hide UI question
    /// </summary>
    public static event UnityAction HideQuestion;
    public static void OnHideQuestion() => HideQuestion?.Invoke();

    /// <summary>
    /// Set Question data
    /// </summary>
    public static event UnityAction <QuestionData> SetQuestion;
    public static void OnSetQuestion(QuestionData questData) => SetQuestion?.Invoke(questData);
    #endregion

    #region Camera
    /// <summary>
    /// Change main cam to Room cam
    /// </summary>
    public static event UnityAction TurnRoomCam;
    public static void OnTurnRoomCam() => TurnRoomCam?.Invoke();

    /// <summary>
    /// Change main cam to Table cam
    /// </summary>
    public static event UnityAction TurnTableCam;
    public static void OnTurnTableCam() => TurnTableCam?.Invoke();

    /// <summary>
    /// Change main cam to Direct Table (Top of Table) cam
    /// </summary>
    public static event UnityAction TurnDirectTableCam;
    public static void OnTurnDirectTableCam() => TurnDirectTableCam?.Invoke();

    /// <summary>
    /// Change main cam to Collection cam
    /// </summary>
    public static event UnityAction TurnCollectionCam;
    public static void OnTurnCollectionCam() => TurnCollectionCam?.Invoke();

    /// <summary>
    /// Change main cam to Audience cam
    /// </summary>
    public static event UnityAction TurnAudienceCam;
    public static void OnTurnAudienceCam() => TurnAudienceCam?.Invoke();
    #endregion

    #region Navigation & Point Slider (Card Battle)
    /// <summary>
    /// Show navigation Footer
    /// </summary>
    public static event UnityAction ShowNavButtons;
    public static void OnShowNavButtons() => ShowNavButtons?.Invoke();

    /// <summary>
    /// Hide navigation Footer
    /// </summary>
    public static event UnityAction HideNavButtons;
    public static void OnHideNavButtons() => HideNavButtons?.Invoke();

    /// <summary>
    /// Calculating Point after a hit
    /// </summary>
    public static event UnityAction <PlayingCard> ExecutingPoint;
    public static void OnExecutingPoint(PlayingCard card) => ExecutingPoint?.Invoke(card);

    /// <summary>
    /// Transfer point data to UI Slider
    /// </summary>
    public static event UnityAction <int, int> ExecutingPointToUI;
    public static void OnExecutingPointToUI(int playerPoint, int opponentPoint) => ExecutingPointToUI?.Invoke(playerPoint, opponentPoint);

    /// <summary>
    /// Show Point slider
    /// </summary>
    public static event UnityAction ShowPointSlider;
    public static void OnShowPointSlider() => ShowPointSlider?.Invoke();

    /// <summary>
    /// Hide Point slider
    /// </summary>
    public static event UnityAction HidePointSlider;
    public static void OnHidePointSlider() => HidePointSlider?.Invoke();

    #endregion

    #region Bot
    public static event UnityAction <GameObject, GameObject, GameObject> BotSetCard;
    public static void OnBotSetCard(GameObject cardMid, GameObject cardLeft, GameObject cardRight) => BotSetCard?.Invoke(cardMid, cardLeft, cardRight);

    public static event UnityAction BotPlay;
    public static void OnBotPlay() => BotPlay?.Invoke();
    #endregion

    #region Spawn Game on Table
    public static event UnityAction SpawnGameOnTable;
    public static void OnSpawnGameOnTable() => SpawnGameOnTable?.Invoke();
    #endregion

    #region Player
    public static event UnityAction SavePlayerData;
    public static void OnSavePlayerData() => SavePlayerData?.Invoke();

    public static event UnityAction LoadPlayerData;
    public static void OnLoadPlayerData() => LoadPlayerData?.Invoke();

    public static event UnityAction <PlayerData> SetLevelSlider;
    public static void OnSetLevelSlider(PlayerData playerData) => SetLevelSlider?.Invoke(playerData);

    public static event UnityAction <PlayerData> SetPlayerCoin;
    public static void OnSetPlayerCoin(PlayerData playerData) => SetPlayerCoin?.Invoke(playerData);

    public static event UnityAction<int> AddPlayerCoin;
    public static void OnAddPlayerCoin(int amount) => AddPlayerCoin?.Invoke(amount);

    public static event UnityAction<int> AddPlayerLevel;
    public static void OnAddPlayerLevel(int amount) => AddPlayerLevel?.Invoke(amount);

    public static event UnityAction LoadPlayerOwnedCard;
    public static void OnLoadPlayerOwnedCard() => LoadPlayerOwnedCard?.Invoke();

    #endregion
}
