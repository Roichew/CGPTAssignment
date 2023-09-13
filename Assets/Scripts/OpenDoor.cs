using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool open=false;//按键
    public  bool key=false;
    private Animator ani;
    public GameObject uiText;//开门提示
    public GameObject noKey;//是否有钥匙
    public GameObject gamePassed;
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
                        //关闭触发器
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (isTutorial == true)
                    {
                        ani.SetBool("TutorialDoor", true);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                }
                   
                else 
                {
                    noKey.SetActive(true) ;
                }
            }

        }
       
    }
    public void Win() //完成的方法，挂载在开门之后一帧事件上
    {
        gamePassed.SetActive(true) ;//开门后显示通关   
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
