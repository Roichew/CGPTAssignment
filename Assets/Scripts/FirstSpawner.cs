using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawner : MonoBehaviour
{
    public GameObject gameObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TutorialEnemyAI.
        }
    }
}
