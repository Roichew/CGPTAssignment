using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILock : MonoBehaviour
{

    public GameObject UIPuzzle;
    public GameObject openText;
    public GameObject GameObject;

    public bool inReach;

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
            
            openText.SetActive(true);
        }

    }

}
