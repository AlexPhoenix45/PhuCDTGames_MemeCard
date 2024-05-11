using Assets.Core.Scripts.Core.Managers;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceHolder : MonoBehaviour
{
    public ServiceManager serviceManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        serviceManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void ABC()
    {
        ServiceManager.ShowInter();
    }
}
