using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitchTrigger : MonoBehaviour
{

    public GameObject Light;
    //public TutorialEnemyAI enemyAI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Light.SetActive(false);
        }
        if (other.gameObject.CompareTag("Drake"))
        {
            Light.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drake"))
        {
            Light.SetActive(false);
        }
    }
}
