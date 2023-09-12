using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HubDoors : MonoBehaviour
{
    public GameObject openText;
    public AudioSource EnterLevel;
    public bool inReach;
    public string LevelOpen;


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

        if (inReach && Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(LevelOpen);
            EnterLevel.Play();
        }

    }
}
