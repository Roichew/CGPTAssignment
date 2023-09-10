using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMe : MonoBehaviour
{

    public GameObject Player;
    TutorialEnemyAI enemyAI;
    bool isEnter;
 
    void Start()
    {
        isEnter = false;
    }
    
    void Update()
    {
        if (isEnter)
        {
            enemyAI.stopChase();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("I am ub");
            isEnter = true;

        }
    }

}
