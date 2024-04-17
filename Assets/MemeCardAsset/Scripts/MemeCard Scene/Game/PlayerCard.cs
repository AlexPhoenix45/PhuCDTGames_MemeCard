using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    public Transform placePos;

    private bool firstSelected = false;

    private void OnMouseDown()
    {
        transform.LeanMove(placePos.position, .5f);
        transform.LeanRotate(placePos.transform.eulerAngles, .5f);
        transform.LeanScale(new Vector3(0.6f, 0.05f, 0.6f), .5f);
    }
}
