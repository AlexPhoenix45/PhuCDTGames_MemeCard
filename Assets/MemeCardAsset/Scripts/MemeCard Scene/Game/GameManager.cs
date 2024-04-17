using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Card Prefabs")]
    [Header("Attribute for Meme Card Battle")]
    public GameObject card3D;

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

    //Private Attributes
    private GameObject playerCardMid, playerCardLeft, playerCardRight, opponentCardMid, opponentCardLeft, opponentCardRight;

    #region Meme Card Battle
    public void StartSpawn()
    {

    }

    public void StartGame()
    {
        SpawnCard();
    }

    [Button]
    public void SpawnCard()
    {
        IEnumerator MoveCardToHand()
        {
            playerCardMid = Instantiate(card3D, playerCardHolder, playerCardStack.transform);
            opponentCardMid = Instantiate(card3D, opponentCardHolder, opponentCardStack.transform);

            playerCardMid.transform.position = playerCardStack.transform.position;
            playerCardMid.transform.rotation = Quaternion.Euler(Vector3.zero);
            playerCardMid.transform.LeanMove(playerCardMidPos.position, .75f);
            playerCardMid.transform.LeanRotate(playerCardMidPos.transform.eulerAngles, .75f);

            opponentCardMid.transform.position = opponentCardStack.transform.position;
            opponentCardMid.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardMid.transform.LeanMove(opponentCardMidPos.position, .75f);
            opponentCardMid.transform.LeanRotate(opponentCardMidPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            playerCardLeft = Instantiate(card3D, playerCardHolder, playerCardStack.transform);
            opponentCardLeft = Instantiate(card3D, opponentCardHolder, opponentCardStack.transform);

            playerCardLeft.transform.position = playerCardStack.transform.position;
            playerCardLeft.transform.rotation = Quaternion.Euler(Vector3.zero);
            playerCardLeft.transform.LeanMove(playerCardLeftPos.position, .75f);
            playerCardLeft.transform.LeanRotate(playerCardLeftPos.transform.eulerAngles, .75f);

            opponentCardLeft.transform.position = opponentCardStack.transform.position;
            opponentCardLeft.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardLeft.transform.LeanMove(opponentCardLeftPos.position, .75f);
            opponentCardLeft.transform.LeanRotate(opponentCardLeftPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            playerCardRight = Instantiate(card3D, playerCardHolder, playerCardStack.transform);
            opponentCardRight = Instantiate(card3D, opponentCardHolder, opponentCardStack.transform);

            playerCardRight.transform.rotation = Quaternion.Euler(Vector3.zero);
            playerCardRight.transform.position = playerCardStack.transform.position;
            playerCardRight.transform.LeanMove(playerCardRightPos.position, .75f);
            playerCardRight.transform.LeanRotate(playerCardRightPos.transform.eulerAngles, .75f);

            opponentCardRight.transform.rotation = opponentCardStack.transform.rotation;
            opponentCardRight.transform.position = opponentCardStack.transform.position;
            opponentCardRight.transform.LeanMove(opponentCardRightPos.position, .75f);
            opponentCardRight.transform.LeanRotate(opponentCardRightPos.transform.eulerAngles, 0f);
            yield return new WaitForSeconds(.75f);

            playerCardMid.GetComponent<PlayerCard>().placePos = playerPlacingPos;
            playerCardLeft.GetComponent<PlayerCard>().placePos = playerPlacingPos;
            playerCardRight.GetComponent<PlayerCard>().placePos = playerPlacingPos;

            opponentCardMid.GetComponent<PlayerCard>().placePos = opponentPlacingPos;
            opponentCardLeft.GetComponent<PlayerCard>().placePos = opponentPlacingPos;
            opponentCardRight.GetComponent<PlayerCard>().placePos = opponentPlacingPos;
        }
        StartCoroutine(MoveCardToHand());
    }

    public void HideCard()
    {

    }

    public void DrawQuestion()
    {

    }
    #endregion
}
