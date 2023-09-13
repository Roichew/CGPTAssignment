using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound;

    void Update()
    {
        if (!MenuScreen.isPaused)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    footstepsSound.enabled = false;
                    sprintSound.enabled = true;
                }
                else
                {
                    footstepsSound.enabled = true;
                    sprintSound.enabled = false;
                }
            }
            else
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = false;
            }
        }
    }
}

