using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    [HideInInspector]
    public bool isBlending = false;

    public CinemachineVirtualCamera roomCam;
    public CinemachineVirtualCamera tableCam;
    public CinemachineVirtualCamera directTableCam;
    public CinemachineVirtualCamera collectionCam;
    public CinemachineVirtualCamera audienceCam;

    private void OnEnable()
    {
        EventController.TurnRoomCam += TurnRoomCam;
        EventController.TurnTableCam += TurnTableCam;
        EventController.TurnDirectTableCam += TurnDirectTableCam;
        EventController.TurnCollectionCam += TurnCollectionCam;
        EventController.TurnAudienceCam += TurnAudienceCam;
    }

    private void Update()
    {
        if (cinemachineBrain.IsBlending)
        {
            isBlending = true;
        }
        else
        {
            isBlending = false;
        }
    }

    public void TurnRoomCam()
    {
        roomCam.gameObject.SetActive(true);
        tableCam.gameObject.SetActive(false);
        directTableCam.gameObject.SetActive(false);
        collectionCam.gameObject.SetActive(false);
        audienceCam.gameObject.SetActive(false);
        cinemachineBrain.m_DefaultBlend.m_Time = 2f;
    }
    public void TurnTableCam()
    {
        roomCam.gameObject.SetActive(false);
        tableCam.gameObject.SetActive(true);
        directTableCam.gameObject.SetActive(false);
        collectionCam.gameObject.SetActive(false);
        audienceCam.gameObject.SetActive(false);
        cinemachineBrain.m_DefaultBlend.m_Time = 1f;
    }
    public void TurnDirectTableCam()
    {
        roomCam.gameObject.SetActive(false);
        tableCam.gameObject.SetActive(false);
        directTableCam.gameObject.SetActive(true);
        collectionCam.gameObject.SetActive(false);
        audienceCam.gameObject.SetActive(false);
        cinemachineBrain.m_DefaultBlend.m_Time = 2f;
    }
    public void TurnCollectionCam()
    {
        roomCam.gameObject.SetActive(false);
        tableCam.gameObject.SetActive(false);
        directTableCam.gameObject.SetActive(false);
        collectionCam.gameObject.SetActive(true);
        audienceCam.gameObject.SetActive(false);
        cinemachineBrain.m_DefaultBlend.m_Time = 2f;
    }
    public void TurnAudienceCam()
    {
        roomCam.gameObject.SetActive(false);
        tableCam.gameObject.SetActive(false);
        directTableCam.gameObject.SetActive(false);
        collectionCam.gameObject.SetActive(false);
        audienceCam.gameObject.SetActive(true);
        cinemachineBrain.m_DefaultBlend.m_Time = .75f;
        IEnumerator waitToChangeCam()
        {
            yield return new WaitForSeconds(2.5f);
            EventController.OnTurnTableCam();
        }
        StartCoroutine(waitToChangeCam());
    }
    private void OnDisable()
    {
        EventController.TurnRoomCam -= TurnRoomCam;
        EventController.TurnTableCam -= TurnTableCam;
        EventController.TurnDirectTableCam -= TurnDirectTableCam;
        EventController.TurnCollectionCam -= TurnCollectionCam;
        EventController.TurnAudienceCam -= TurnAudienceCam;
    }

}
