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
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(1f);
            GetComponent<BoxCollider>().enabled = true;
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
