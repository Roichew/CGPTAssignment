using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    private bool keyE;//是否可以按E键
    public GameObject shiqu;//拾取提示
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
