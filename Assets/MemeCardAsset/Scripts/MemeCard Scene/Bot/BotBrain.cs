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

    public bool isOpponent = false;
    public ParticleSystem explosion;

    #region Body Part
    [Header("Hair")]
    public GameObject maleHair1;
    public GameObject maleHair2;
    public GameObject femaleHair1;
    public GameObject femaleHair2;
    public GameObject femaleHair3;
    [Header("Glasses")]
    public GameObject glasses1;
    public GameObject glasses2;
    public GameObject glasses3;
    [Header("Shirt")]
    public GameObject shirt1;
    public GameObject shirt2;
    [Header("Pants")]
    public GameObject malePants1;
    public GameObject femalePants1;
    public GameObject femalePants2;
    #endregion
    private void OnEnable()
    {
        EventController.BotSetCard += BotSetCard;
        EventController.BotPlay += BotPlay;

        if (isOpponent)
        {
            explosion.Play();
        }
    }

    private void GenerateApperance()
    {

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
