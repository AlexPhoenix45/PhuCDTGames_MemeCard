using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public static EventController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action onGameStart;
    public void OnGameStart()
    {
        if (onGameStart != null) onGameStart();
    }

}
