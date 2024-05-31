using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] tables;
    public GameObject[] bookStands;
    public GameObject[] roomRights;
    public GameObject[] roomBacks;
    public GameObject[] roomWalls;
    public GameObject[] chairs;
    public ParticleSystem explosion;
    private void OnEnable()
    {
        EventController.UpdateEnvironment += UpdateEnvironment;
    }

    private void UpdateEnvironment(int playerLvl)
    {
        switch (playerLvl)
        {
            case 6:
                explosion.Play();
                DisableFurniture();
                roomBacks[1].SetActive(true);
                roomRights[0].SetActive(true);
                bookStands[0].SetActive(true);
                tables[0].SetActive(true);
                roomWalls[0].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(0);
                }
                break;
            case 11:
                explosion.Play();
                DisableFurniture();
                roomBacks[1].SetActive(true);
                roomRights[1].SetActive(true);
                bookStands[0].SetActive(true);
                tables[0].SetActive(true);
                roomWalls[0].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(0);
                }
                break;
            case 16:
                explosion.Play();
                DisableFurniture();
                roomBacks[1].SetActive(true);
                roomRights[1].SetActive(true);
                bookStands[1].SetActive(true);
                tables[0].SetActive(true);
                roomWalls[0].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(0);
                }
                break;
            case 21:
                explosion.Play();
                DisableFurniture();
                roomBacks[1].SetActive(true);
                roomRights[1].SetActive(true);
                bookStands[1].SetActive(true);
                tables[1].SetActive(true);
                roomWalls[0].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(0);
                }
                break;
            case 26:
                explosion.Play();
                DisableFurniture();
                roomBacks[1].SetActive(true);
                roomRights[1].SetActive(true);
                bookStands[1].SetActive(true);
                tables[1].SetActive(true);
                roomWalls[1].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(1);
                }
                break;
            case 6 + 25:
                explosion.Play();
                DisableFurniture(); 
                roomBacks[2].SetActive(true);
                roomRights[1].SetActive(true);
                bookStands[1].SetActive(true);
                tables[1].SetActive(true);
                roomWalls[1].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(1);
                }
                break;
            case 11 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[2].SetActive(true);
                roomRights[2].SetActive(true);
                bookStands[1].SetActive(true);
                tables[1].SetActive(true);
                roomWalls[1].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(1);
                }
                break;
            case 16 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[2].SetActive(true);
                roomRights[2].SetActive(true);
                bookStands[2].SetActive(true);
                tables[1].SetActive(true);
                roomWalls[1].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(1);
                }
                break;
            case 21 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[2].SetActive(true);
                roomRights[2].SetActive(true);
                bookStands[2].SetActive(true);
                tables[2].SetActive(true);
                roomWalls[1].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(1);
                }
                break;
            case 26 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[2].SetActive(true);
                roomRights[2].SetActive(true);
                bookStands[2].SetActive(true);
                tables[2].SetActive(true);
                roomWalls[2].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(2);
                }
                break;
            case 31 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[3].SetActive(true);
                roomRights[2].SetActive(true);
                bookStands[2].SetActive(true);
                tables[2].SetActive(true);
                roomWalls[2].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(2);
                }
                break;
            case 36 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[3].SetActive(true);
                roomRights[3].SetActive(true);
                bookStands[2].SetActive(true);
                tables[2].SetActive(true);
                roomWalls[2].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(2);
                }
                break;
            case 41 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[3].SetActive(true);
                roomRights[3].SetActive(true);
                bookStands[3].SetActive(true);
                tables[2].SetActive(true);
                roomWalls[2].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(2);
                }
                break;
            case 46 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[3].SetActive(true);
                roomRights[3].SetActive(true);
                bookStands[3].SetActive(true);
                tables[3].SetActive(true);
                roomWalls[2].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(2);
                }
                break;
            case 51 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[3].SetActive(true);
                roomRights[3].SetActive(true);
                bookStands[3].SetActive(true);
                tables[3].SetActive(true);
                roomWalls[3].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(3);
                }
                break;
            case 56 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[4].SetActive(true);
                roomRights[3].SetActive(true);
                bookStands[3].SetActive(true);
                tables[3].SetActive(true);
                roomWalls[3].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(3);
                }
                break;
            case 61 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[4].SetActive(true);
                roomRights[4].SetActive(true);
                bookStands[3].SetActive(true);
                tables[3].SetActive(true);
                roomWalls[3].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(3);
                }
                break;
            case 66 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[4].SetActive(true);
                roomRights[4].SetActive(true);
                bookStands[4].SetActive(true);
                tables[3].SetActive(true);
                roomWalls[3].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(3);
                }
                break;
            case 71 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[4].SetActive(true);
                roomRights[4].SetActive(true);
                bookStands[4].SetActive(true);
                tables[4].SetActive(true);
                roomWalls[3].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(3);
                }
                break;
            case 76 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[4].SetActive(true);
                roomRights[4].SetActive(true);
                bookStands[4].SetActive(true);
                tables[4].SetActive(true);
                roomWalls[4].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(4);
                }
                break;
            case 81 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[5].SetActive(true);
                roomRights[4].SetActive(true);
                bookStands[4].SetActive(true);
                tables[4].SetActive(true);
                roomWalls[4].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(4);
                }
                break;
            case 86 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[5].SetActive(true);
                roomRights[5].SetActive(true);
                bookStands[4].SetActive(true);
                tables[4].SetActive(true);
                roomWalls[4].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(4);
                }
                break;
            case 91 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[5].SetActive(true);
                roomRights[5].SetActive(true);
                bookStands[5].SetActive(true);
                tables[4].SetActive(true);
                roomWalls[4].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(4);
                }
                break;
            case 96 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[5].SetActive(true);
                roomRights[5].SetActive(true);
                bookStands[5].SetActive(true);
                tables[5].SetActive(true);
                roomWalls[4].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(4);
                }
                break;

            case 101 + 25:
                explosion.Play();
                DisableFurniture();
                roomBacks[5].SetActive(true);
                roomRights[5].SetActive(true);
                bookStands[5].SetActive(true);
                tables[5].SetActive(true);
                roomWalls[5].SetActive(true);
                foreach (GameObject chair in chairs)
                {
                    chair.GetComponent<Chairs>().ChairEnable(5);
                }
                break;
        }
    }

    private void DisableFurniture()
    {
        foreach (GameObject table in tables)
        {
            table.SetActive(false);
        }

        foreach (GameObject book in bookStands)
        {
            book.SetActive(false);
        }

        foreach (GameObject room in roomRights)
        {
            room.SetActive(false);
        }

        foreach (GameObject room in roomWalls)
        {
            room.SetActive(false);
        }

        foreach (GameObject room in roomBacks)
        {
            room.SetActive(false);
        }

        foreach (GameObject chair in chairs)
        {
            chair.GetComponent<Chairs>().DisableChairs();
        }
    }

    private void OnDisable()
    {
        EventController.UpdateEnvironment -= UpdateEnvironment;
    }
}
