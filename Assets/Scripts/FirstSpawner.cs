using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawner : MonoBehaviour
{
    public GameObject gameObject;
    public TutorialEnemyAI tutorialAI;
    public Transform SpawnPlace;
    public float spawnNumber;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialAI.EnemyState(true);
            //Instantiate(gameObject,SpawnPlace.position,SpawnPlace.rotation);
        }
    }

    void Start()
    {
       
    }
}
