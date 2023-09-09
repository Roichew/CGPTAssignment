using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawner : MonoBehaviour
{
    public GameObject gameObject;
    public TutorialEnemyAI tutorialAI;
    public float spawnNumber;
    bool isSpawned;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isSpawned==false) { 
            tutorialAI.EnemyState(true);
            //Instantiate(tutorialAI.enemyObject,tutorialAI.spawnLocation);
            tutorialAI.SpawnSound.Play();
            isSpawned = true;
            }
        }
    }

    void Start()
    {
       tutorialAI.spawnNumber =  (int)spawnNumber;
        isSpawned = false;
    }
}
