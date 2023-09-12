using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialSpawner : MonoBehaviour
{
    [Header("Enemy Object")]
    [SerializeField]public TutorialEnemyAI tutorialAI;
    [SerializeField] private UnityEvent CallEvent;
    bool isSpawned=false;
    void OnTriggerEnter(Collider other)
    {
        if (isSpawned==false) {
            if (other.gameObject.CompareTag("Player"))
            {
                tutorialAI.EnemyState(true);
                CallEvent.Invoke();
                isSpawned=true;
            }
        }
    }

}