using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSetActive : MonoBehaviour
{
    public float timeEstimate = 1.00f;
    private float timeConsumed = 9999f;
    private void Start()
    {
        timeConsumed = 0f;
    }
    void Update()
    {
        timeConsumed += Time.deltaTime;

        if (timeConsumed > timeEstimate)
        {
            gameObject.SetActive(false);
        }
    }
}
