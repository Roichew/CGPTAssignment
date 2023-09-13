using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    //public GameObject GameObject;
    public AudioSource doorSound;
    public AudioSource Boo;
    public int SpawnChance;
    public GameObject Image;
    bool isOpen;
    public bool inReach;


    void Start()
    {
        inReach = false;
        isOpen = false;

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

        if (inReach && Input.GetButtonDown("Interact") && isOpen == false )
        {
            DoorOpens();
        }

        else
        {
            DoorCloses();
        }


    }
    void DoorOpens ()
    {
        Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        isOpen = true;
        doorSound.Play();
        if (SpawnChance >0 )
        {
            Jumpscare();
        }
    }

    void DoorCloses()
    {
        Debug.Log("It Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
        isOpen = false;
    }

    void Jumpscare()
    {
        int Randomizer = Random.Range(0, 10);
        if (Randomizer <= SpawnChance)
        {
            Image.SetActive(true);
            Boo.Play();
            Invoke("JumpImage", 1);
            Debug.Log(Randomizer);
        }
        else
        {

        }
    }

    void JumpImage()
    {
        Image.SetActive(false);
    }

}
