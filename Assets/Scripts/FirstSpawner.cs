using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawner : MonoBehaviour
{
    public GameObject gameObject;
    public TutorialEnemyAI tutorialAI;
    public float spawnNumber;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialAI.EnemyState(true);
        }
    }

    void Start()
    {
       tutorialAI.spawnNumber =  (int)spawnNumber;
    }
}
