using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPack : MonoBehaviour
{
    private readonly Vector3 cardPackSpawnPos = new Vector3(104.228f, -23.31f, 5.27f);
    private SkinnedMeshRenderer packTearPart;
    private SkinnedMeshRenderer packBodyPart;

    public GameObject packPrefabs;
    public Material commonMat;
    public Material rareMat;
    public Material epicMat;

    private int packRariry;
    private GameObject tempPack;

    private void OnEnable()
    {
        EventController.OnTurnDirectTableCam();
        packTearPart = packPrefabs.transform.Find("2").GetComponent<SkinnedMeshRenderer>();
        packBodyPart = packPrefabs.transform.Find("3").GetComponent<SkinnedMeshRenderer>();
    }

    public int rarity;
    [Button]
    public void Show()
    {
        ShowPack(rarity);
    }

    private void ShowPack(int rarity)
    {
        packRariry = rarity;
        if (rarity == 0)
        {
            packTearPart.material = commonMat;
            packBodyPart.material = commonMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, transform);
        }
        else if (rarity == 1)
        {
            packTearPart.material = rareMat;
            packBodyPart.material = rareMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, transform);
        }
        else if (rarity == 2)
        {
            packTearPart.material = epicMat;
            packBodyPart.material = epicMat;
            tempPack = Instantiate(packPrefabs, cardPackSpawnPos, Quaternion.identity, transform);
        }
    }

    [Button]
    public void OpenPack()
    {
        tempPack.GetComponent<Animator>().SetTrigger("OpenPack");
    }
}
