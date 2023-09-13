using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool open=false;//����
    public  bool key=false;
    private Animator ani;
    public GameObject uiText;//������ʾ
    public GameObject noKey;//�Ƿ���Կ��
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
                        //�رմ�����
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
    public void Win() //��ɵķ����������ڿ���֮��һ֡�¼���
    {
        gamePassed.SetActive(true) ;//���ź���ʾͨ��   
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
