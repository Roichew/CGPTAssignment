using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HubDoors : MonoBehaviour
{
    public GameObject openText;
    public GameObject finalDoor;
    public AudioSource EnterLevel;
    public bool inReach;
    public string LevelOpen;
    public static bool HotelCompleted, HouseCompleted, HospitalCompleted;

    void Start()
    {
        inReach = false;
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }



    void Update()
    {
        if (!MenuScreen.isPaused)
        {
            if (inReach && Input.GetButtonDown("Interact"))
            {
                EnterLevel.Play();
                Invoke("Switchlevel", 1);
            }

            if (HotelCompleted && HouseCompleted && HospitalCompleted)
            {
                Debug.Log("All completed");
                finalDoor.SetActive(true);

            }
        }
    }

    void Switchlevel()
    {
        SceneManager.LoadScene(LevelOpen);
    }


}
