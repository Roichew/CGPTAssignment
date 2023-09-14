using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishGaemDoor : MonoBehaviour
{
    public GameObject Text;
    public GameObject Win;
    public AudioSource HAha;
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
            Text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            Text.SetActive(false);
        }
    }

    void Update()
    {
        if (!MenuScreen.isPaused)
        {
            if (inReach && Input.GetButtonDown("Interact"))
            {
                HAha.Play();
                Win.SetActive(true);
                Invoke("Switchlevel", 1);
            }
        }
    }
    void Switchlevel()
    {
        SceneManager.LoadScene(LevelOpen);
    }
}
