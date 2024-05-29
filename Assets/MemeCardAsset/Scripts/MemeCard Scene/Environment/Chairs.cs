using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    public GameObject[] chairs;

    public void DisableChairs()
    {
        foreach (var chair in chairs)
        {
            chair.SetActive(false);
        }
    }

    public void ChairEnable(int index)
    {
        DisableChairs();
        chairs[index].SetActive(true);
    }
}
