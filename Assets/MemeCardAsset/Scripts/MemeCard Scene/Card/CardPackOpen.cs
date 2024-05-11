using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPackOpen : MonoBehaviour
{
    private bool hasClick = false;
    private void OnEnable()
    {
        hasClick = false;
        StartCoroutine(wait());
        IEnumerator wait()
        {
            GetComponent<BoxCollider>().enabled = true;
            hasClick = true;
            yield return new WaitForSeconds(2.5f);
            hasClick = false;
        }
    }

    private void OnMouseDown()
    {
        if (!hasClick)
        {
            EventController.OnOpenPack();
            hasClick = true;
        }
    }
}
