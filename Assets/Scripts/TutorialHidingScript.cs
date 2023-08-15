using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHidingScript : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject normalPlayer, hidingPlayer;
    public TutorialEnemyAI enemyAI;
    public Transform enemyTransform;
    bool interactable, hiding;
    public float loseDistance;

    void Start()
    {
        interactable = false;
        hiding = false; 
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Reach"))
        {
            hideText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            hideText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("E");
                hideText.SetActive(false);
                hidingPlayer.SetActive(true);
                float distance = Vector3.Distance(enemyTransform.position, normalPlayer.transform.position);
                if (distance > loseDistance)
                {
                    if (enemyAI.chasing == true)
                    {
                        enemyAI.stopChase();
                    }
                }
                stopHideText.SetActive(true);
                hiding = true;
                normalPlayer.SetActive(false);
                interactable = false;
                
            }
        }

        if (hiding == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                stopHideText.SetActive(false);
                normalPlayer.SetActive(true);
                hidingPlayer.SetActive(false);
                hiding = false;
            }
        }
    }
}
