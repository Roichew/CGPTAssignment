using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    private bool keyE;//Mark whether the E key is pressed
    public GameObject shiqu;//UI used to display key retrieval prompts
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyE) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                gameObject.SetActive(false);
                door.GetComponent<OpenDoor>().key = true;
                shiqu.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
           keyE= true;
           shiqu.SetActive(true);
           
        }
    }
}
