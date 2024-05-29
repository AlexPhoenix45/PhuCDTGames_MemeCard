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
    public GameObject[] shoes;
    public GameObject[] hats;
    public GameObject[] eyeBrows;
    public SkinnedMeshRenderer skin;
    #endregion

    #region Colored Material
    [Header("Colored Material")]
    public Material[] clothesMaterials;
    public Material[] hairMaterials;
    public Material[] skinMaterials;
    public Material[] shoesMaterials;
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
        GenerateAudienceApperance();
    }

    [Button]
    public void GenerateAudienceApperance()
    {
        int randomHair = Random.Range(0, hair.Length + 1);
        int randomShirts = Random.Range(0, shirts.Length);  
        int randomPants = Random.Range(0, pants.Length);
        int randomShoes = Random.Range(0, 2);

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
                    Material mat = hairMaterials[Random.Range(0, hairMaterials.Length)];
                    hair[i].GetComponent<MeshRenderer>().material = mat;
                    eyeBrows[0].GetComponent<SkinnedMeshRenderer>().material = mat;
                    eyeBrows[1].GetComponent<SkinnedMeshRenderer>().material = mat;
                }
                else
                {
                    hair[i].SetActive(false);
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

        //Shoes
        if (randomShoes == 0)
        {
            Material mat = shoesMaterials[Random.Range(0, shoesMaterials.Length)];
            shoes[0].SetActive(true);
            shoes[0].GetComponent<SkinnedMeshRenderer>().material = mat;
            shoes[1].SetActive(true);
            shoes[1].GetComponent<SkinnedMeshRenderer>().material = mat;
            shoes[2].SetActive(false);
            shoes[3].SetActive(false);
        }
        else
        {
            Material mat = shoesMaterials[Random.Range(0, shoesMaterials.Length)];
            shoes[2].SetActive(true);
            shoes[2].GetComponent<MeshRenderer>().material = mat;
            shoes[3].SetActive(true);
            shoes[3].GetComponent<MeshRenderer>().material = mat;
            shoes[1].SetActive(false);
            shoes[0].SetActive(false);
        }

        skin.material = skinMaterials[Random.Range(0, skinMaterials.Length)];

        foreach (GameObject item in glasses)
        {
            item.SetActive(false);
        }

        foreach (GameObject item in hats)
        {
            item.SetActive(false);
        }
    }

    public void GenerateOpponentApperance(OpponentData opponent)
    {
        GenerateAudienceApperance();

        print(opponent.name);

        switch (opponent.name)
        {
            case "Opponent1":
                {
                    SetHair(0, 2);
                    SetSkin(3);
                    break;
                }
            case "Opponent2":
                {
                    SetHair(3, 10);
                    SetSkin(0);
                    break;
                }
            case "Opponent3":
                {
                    SetHair(4, 3);
                    SetSkin(3);
                    break;
                }
            case "Opponent4":
                {
                    SetHair(2, 4);
                    SetSkin(1);
                    break;
                }
            case "Opponent5":
                {
                    SetHair(4, 8);
                    SetSkin(0);
                    break;
                }
            case "Opponent6":
                {
                    SetHair(2, 2);
                    SetSkin(0);
                    break;
                }
            case "Opponent7":
                {
                    SetHair(2, 10);
                    SetSkin(1);
                    break;
                }
            case "Opponent8":
                {
                    SetHair(1, 6);
                    SetSkin(3);
                    break;
                }
            case "Opponent9":
                {
                    SetHair(3, 10);
                    SetSkin(3);
                    break;
                }
            case "Opponent10":
                {
                    SetHair(0, 5);
                    SetSkin(2);
                    break;
                }
            case "Boss1":
                {
                    SetHair(3, 3);
                    SetSkin(0);
                    SetGlasses(2);
                    break;
                }
            case "Boss2":
                {
                    SetHair(3, 4);
                    SetSkin(2);
                    SetHat(0);
                    break;
                }
            case "Boss3":
                {
                    SetHair(3, 3);
                    SetSkin(2);
                    SetHat(1);
                    break;
                }
            case "Boss4":
                {
                    SetHair(1, 3);
                    SetSkin(1);
                    SetGlasses(0);
                    break;
                }
            case "Boss5":
                {
                    SetSkin(1);
                    SetGlasses(1);
                    SetHat(2);
                    break;
                }
            case "Boss6":
                {
                    SetSkin(2);
                    SetHat(0);
                    SetGlasses(1);
                    break;
                }
            case "Boss7":
                {
                    SetSkin(0);
                    SetHat(2);
                    SetGlasses(2);
                    break;
                }
            case "Boss8":
                {
                    SetSkin(2);
                    SetGlasses(0);
                    SetHair(2, 4);
                    break;
                }
            case "Boss9":
                {
                    SetSkin(1);
                    SetGlasses(1);
                    SetHair(2, 10);
                    break;
                }
            case "Boss10":
                {
                    SetHat(0);
                    SetSkin(1);
                    SetGlasses(1);
                    SetHair(4, 8);
                    break;
                }
            case "Boss11":
                {
                    SetHat(2);
                    SetSkin(2);
                    SetGlasses(0);
                    SetHair(4, 6);
                    break;
                }
            case "Boss12":
                {
                    SetSkin(3);
                    SetGlasses(1);
                    SetHair(4, 5);
                    SetHat(1);
                    break;
                }
        }

        void SetHair(int hairIndex, int colorIndex)
        {
            for (int i = 0; i < hair.Length; i++)
            {
                if (i == hairIndex)
                {
                    hair[i].SetActive(true);
                    
                    Material mat = hairMaterials[colorIndex];
                    hair[i].GetComponent<MeshRenderer>().material = mat;
                    eyeBrows[0].GetComponent<SkinnedMeshRenderer>().material = mat;
                    eyeBrows[1].GetComponent<SkinnedMeshRenderer>().material = mat;
                }
                else
                {
                    hair[i].SetActive(false);
                }
            }
        }

        void SetSkin(int skinColorIndex)
        {
            skin.material = skinMaterials[skinColorIndex];
        }

        void SetGlasses(int glassesIndex)
        {
            for (int i = 0; i < glasses.Length; i++)
            {
                if (i == glassesIndex)
                {
                    glasses[i].SetActive(true);    
                }
                else
                {
                    glasses[i].SetActive(false);
                }
            }
        }

        void SetHat(int hatIndex)
        {
            for (int i = 0; i < glasses.Length; i++)
            {
                if (i == hatIndex)
                {
                    hats[i].SetActive(true);
                }
                else
                {
                    hats[i].SetActive(false);
                }
            }
        }

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
                    EventController.OnSFXPlay_Sad();
                    int random = UnityEngine.Random.Range(1, 3);
                    GetComponent<Animator>().SetTrigger("PlayOpponentSad" + random);
                }
                else
                {
                    EventController.OnSFXPlay_Laugh();
                    GetComponent<Animator>().SetTrigger("PlayOpponentLaugh");
                }
                yield return new WaitForSeconds(4.5f);
                GetComponent<Animator>().SetTrigger("PlayOpponentIdle");
            }
            else
            {
                if (point <= 10)
                {
                    //EventController.OnSFXPlay_Sad();
                    int random = UnityEngine.Random.Range(1, 4);
                    GetComponent<Animator>().SetTrigger("PlayAudienceSad" + random);
                }
                else if (point > 10 && point <= 60)
                {
                    //EventController.OnSFXPlay_Laugh();
                    int random = UnityEngine.Random.Range(1, 6);
                    GetComponent<Animator>().SetTrigger("PlayAudienceLaugh" + random);
                }
                else
                {
                    //EventController.OnSFXPlay_Laugh();
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
