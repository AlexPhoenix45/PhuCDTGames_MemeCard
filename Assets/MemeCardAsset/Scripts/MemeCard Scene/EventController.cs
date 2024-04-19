using UnityEngine;
using UnityEngine.Events;

public class EventController
{
    public static event UnityAction <GameObject> HideAndDrawCard;
    public static void OnHideAndDrawCard(GameObject cardObj) => HideAndDrawCard?.Invoke(cardObj);

    public static event UnityAction ShowQuestion;
    public static void OnShowQuestion() => ShowQuestion?.Invoke();

    public static event UnityAction HideQuestion;
    public static void OnHideQuestion() => HideQuestion?.Invoke();

    public static event UnityAction <QuestionData> SetQuestion;
    public static void OnSetQuestion(QuestionData questDat) => SetQuestion?.Invoke(questDat);

    public static event UnityAction StartGame;
    public static void OnStartGame() => StartGame?.Invoke();

    public static event UnityAction TurnRoomCam;
    public static void OnTurnRoomCam() => TurnRoomCam?.Invoke();

    public static event UnityAction TurnTableCam;
    public static void OnTurnTableCam() => TurnTableCam?.Invoke();

    public static event UnityAction TurnDirectTableCam;
    public static void OnTurnDirectTableCam() => TurnDirectTableCam?.Invoke();

    public static event UnityAction TurnCollectionCam;
    public static void OnTurnCollectionCam() => TurnCollectionCam?.Invoke(); 
    
    public static event UnityAction TurnAudienceCam;
    public static void OnTurnAudienceCam() => TurnAudienceCam?.Invoke();

    public static event UnityAction ShowNavButtons;
    public static void OnShowNavButtons() => ShowNavButtons?.Invoke();

    public static event UnityAction HideNavButtons;
    public static void OnHideNavButtons() => HideNavButtons?.Invoke();

    public static event UnityAction <PlayingCard> ExecutingPoint;
    public static void OnExecutingPoint(PlayingCard card) => ExecutingPoint?.Invoke(card);

    public static event UnityAction CardReadyToPlay;
    public static void OnCardReadyToPlay() => CardReadyToPlay?.Invoke();

    public static event UnityAction CardBattleTurnTwo;
    public static void OnCardBattleTurnTwo() => CardBattleTurnTwo?.Invoke();
}
