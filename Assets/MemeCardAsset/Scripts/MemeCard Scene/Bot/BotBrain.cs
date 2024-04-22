using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBrain : MonoBehaviour
{
    #region Card Battle
    [HideInInspector]
    public GameObject opponentCardMid;
    [HideInInspector]
    public GameObject opponentCardLeft;
    [HideInInspector]
    public GameObject opponentCardRight;

    private void OnEnable()
    {
        EventController.BotSetCard += BotSetCard;
        EventController.BotPlay += BotPlay;
    }

    private void BotPlay()
    {
        IEnumerator waitToPlay()
        {
            yield return new WaitForSeconds(4.5f);

            while (FindObjectOfType<CameraMovement>().isBlending) 
            {
                yield return null;
            }

            int rand = UnityEngine.Random.Range(0, 3);

            if (rand == 0)
            {
                if (opponentCardMid != null)
                {
                    opponentCardMid.GetComponent<PlayingCard>().PlayThisCard();
                }
            }
            else if (rand == 1)
            {
                if (opponentCardLeft != null)
                {
                    opponentCardLeft.GetComponent<PlayingCard>().PlayThisCard();
                }
            }
            else
            {
                if (opponentCardRight != null)
                {
                    opponentCardRight.GetComponent<PlayingCard>().PlayThisCard();
                }
            }
        }
        StartCoroutine(waitToPlay());
    }

    private void BotSetCard(GameObject cardMid, GameObject cardLeft, GameObject cardRight)
    {
        opponentCardMid = cardMid;
        opponentCardLeft = cardLeft;
        opponentCardRight = cardRight;
    }
    #endregion

    private void OnDisable()
    {
        EventController.BotSetCard -= BotSetCard;
        EventController.BotPlay -= BotPlay;
    }
}
