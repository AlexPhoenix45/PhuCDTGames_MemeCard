using DentedPixel.LTExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private void OnEnable()
    {
        if (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.zero;
            transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();
        }
        else
        {
            transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();
        }
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;

    }
}
