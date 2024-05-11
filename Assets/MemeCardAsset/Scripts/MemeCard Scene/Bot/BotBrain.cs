using NaughtyAttributes;
using System.Collections;
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

    #region Body Part
    [Header("Apperances")]
    public GameObject[] hair;
    public GameObject[] glasses;
    public GameObject[] shirts;
    public GameObject[] pants;
    public SkinnedMeshRenderer skin;
    #endregion

    #region Colored Material
    [Header("Colored Material")]
    public Material[] glassesMaterials;
    public Material[] clothesMaterials;
    public Material[] hairMaterials;
    public Material[] skinMaterials;
    #endregion

    #region Animations
    public RuntimeAnimatorController audienceAnim;
    public RuntimeAnimatorController opponentAnim;
    #endregion

    private void OnEnable()
    {
        EventController.BotSetCard += BotSetCard;
        EventController.BotPlay += BotPlay;
        EventController.BotPlayEmotionAnim += PlayAnimation;

        if (isOpponent)
        {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = opponentAnim;
        }
        else
        {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = audienceAnim;
        }
        GenerateApperance();
    }

    [Button]
    public void GenerateApperance()
    {
        int randomHair = Random.Range(0, hair.Length + 1);
        int randomGlasses = Random.Range(0, glasses.Length + 1);
        int randomShirts = Random.Range(0, shirts.Length);  
        int randomPants = Random.Range(0, pants.Length);

        //Hair
        if (randomHair == hair.Length)
        {
            for (int i = 0; i < hair.Length; i++)
            {
                hair[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < hair.Length; i++)
            {
                if (i == randomHair)
                {
                    hair[i].SetActive(true);
                    hair[i].GetComponent<MeshRenderer>().material = hairMaterials[Random.Range(0, hairMaterials.Length)];
                }
                else
                {
                    hair[i].SetActive(false);
                }
            }
        }

        //Glasses
        if (randomGlasses == glasses.Length)
        {
            for (int i = 0; i < glasses.Length; i++)
            {
                glasses[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < glasses.Length; i++)
            {
                if (i == randomGlasses)
                {
                    glasses[i].SetActive(true);
                    glasses[i].GetComponent<MeshRenderer>().material = glassesMaterials[Random.Range(0, glassesMaterials.Length)];
                }
                else
                {
                    glasses[i].SetActive(false);
                }
            }
        }

        //Shirts
        for (int i = 0; i < shirts.Length; i++)
        {
            if (i == randomShirts)
            {
                shirts[i].SetActive(true);
                shirts[i].GetComponent<SkinnedMeshRenderer>().material = clothesMaterials[Random.Range(0, clothesMaterials.Length)];
            }
            else
            {
                shirts[i].SetActive(false);
            }
        }

        //Pants
        for (int i = 0; i < pants.Length; i++)
        {
            if (i == randomPants)
            {
                pants[i].SetActive(true);
                pants[i].GetComponent<SkinnedMeshRenderer>().material = clothesMaterials[Random.Range(0, clothesMaterials.Length)];
            }
            else
            {
                pants[i].SetActive(false);
            }
        }

        skin.material = skinMaterials[Random.Range(0, skinMaterials.Length)];
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
        if (isOpponent)
        {
            StartCoroutine(waitToPlay());
        }
    }

    private void BotSetCard(GameObject cardMid, GameObject cardLeft, GameObject cardRight)
    {
        if (isOpponent)
        {
            opponentCardMid = cardMid;
            opponentCardLeft = cardLeft;
            opponentCardRight = cardRight;
        }
    }

    private void PlayAnimation(int point)
    {
        StartCoroutine(playAnim());
        IEnumerator playAnim()
        {
            if (isOpponent)
            {
                if (point <= 10)
                {
                    int random = UnityEngine.Random.Range(1, 3);
                    GetComponent<Animator>().SetTrigger("PlayOpponentSad" + random);
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("PlayOpponentLaugh");
                }
                yield return new WaitForSeconds(4.5f);
                GetComponent<Animator>().SetTrigger("PlayOpponentIdle");
            }
            else
            {
                if (point <= 10)
                {
                    int random = UnityEngine.Random.Range(1, 4);
                    GetComponent<Animator>().SetTrigger("PlayAudienceSad" + random);
                }
                else if (point > 10 && point <= 60)
                {
                    int random = UnityEngine.Random.Range(1, 6);
                    GetComponent<Animator>().SetTrigger("PlayAudienceLaugh" + random);
                }
                else
                {
                    int random = UnityEngine.Random.Range(6, 10);
                    GetComponent<Animator>().SetTrigger("PlayAudienceLaugh" + random);
                }
                yield return new WaitForSeconds(4.5f);
                GetComponent<Animator>().SetTrigger("PlayAudienceIdle");
            }
        }
    }
    #endregion

    private void OnDisable()
    {
        EventController.BotSetCard -= BotSetCard;
        EventController.BotPlay -= BotPlay;
    }
}
