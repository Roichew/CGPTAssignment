using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostDespawner : MonoBehaviour
{
    public List<Transform> destination; // The destination the enemy is heading towards
    public int destinationNumber;
    Transform newDestination;

     void Update()
    {
        // Check if the enemy has reached the destination
        newDestination = destination[destinationNumber];
        if (Vector3.Distance(transform.position, newDestination.position) < 0.5f)
        {
            // Enemy has reached the destination, despawn it
            Destroy(gameObject);
        }
    }
}
