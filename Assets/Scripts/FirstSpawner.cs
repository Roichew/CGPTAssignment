using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstSpawner : MonoBehaviour
{
    public GameObject gameObject;
    public TutorialEnemyAI tutorialAI;
    public float spawnNumber;
    bool isSpawned;


    public void spawnEnemy()
    {
        if (isSpawned == false)
        {
            tutorialAI.EnemyState(true);
            tutorialAI.SpawnSound.Play();
            isSpawned = true;
        }
    }
}
