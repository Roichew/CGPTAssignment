using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool open=false;//Mark whether the door can be opened
    public  bool key=false;
    private Animator ani;
    public GameObject uiText;//UI showing prompt text
    public GameObject noKey;//Prompt UI when there is no key
    public GameObject gamePassed;
    public AudioSource UnlockSound;
    //Tutorial
    public bool isTutorial=false;
    void Start()
    {
        ani= GetComponent<Animator>();
    }

    
    void Update()
    {
        if (open)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (key)
                {
                    if (isTutorial == false)
                    {
                        ani.SetBool("openDoor", true);
                        uiText.SetActive(false);
                        //Hide prompt text UI
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                        UnlockSound.Play();
                    }
                    else if (isTutorial == true)
                    {
                        ani.SetBool("TutorialDoor", true);
                        uiText.SetActive(false);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                        UnlockSound.Play();
                    }
                }
                   
                else 
                {
                    noKey.SetActive(true) ;
                }
            }

        }
       
    }
    public void Win() //Processing function when the game is won
    {
        gamePassed.SetActive(true) ;//Display game clearance prompt UI
        Time.timeScale= 1f;
    }
   
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") 
        {
            open = true;
           uiText.SetActive(true); 
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
       uiText.SetActive(false);
        noKey.SetActive(false);
        open = false;

    }
}
