using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class TutorialEnemyAI : MonoBehaviour
{
    public NavMeshAgent AI;
    public List<Transform> Destinations;
    public GameObject gameObject;
    public Animator animator;
    public float walkSpeed, chaseSpeed, minidleTime, maxidleTime, idleTime;
    public float viewDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime;
    public float jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum, randNum2;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;

    void Start()
    {
        EnemyState();
    }
    void Update()
    {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = Destinations[randNum];
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, viewDistance)) { 
            if(hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                chasing = true;
            }
        }
        if (chasing == true)
        {
            dest = player.position;
            AI.destination = dest;
            AI.speed = chaseSpeed;
            if (AI.remainingDistance <= catchDistance)
            {
              player.gameObject.SetActive(false);
                animator.ResetTrigger("idle");
                animator.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }

        if (walking == true)
        {
            dest = currentDest.position;
            AI.destination = dest;
            AI.speed = walkSpeed;
            AI.updateRotation = true;
            if (AI.remainingDistance <= AI.stoppingDistance)
            {
                randNum2 = Random.Range(0, 2);
                if (randNum2 == 0)
                {
                    randNum = Random.Range(0, destinationAmount);
                    currentDest = Destinations[randNum];
                }
                if (randNum2 == 1)
                {
                    animator.ResetTrigger("idle");
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    walking = false;
                }
            }

        }
        //AI.destination = player.position;

    }

    public void EnemyState(bool state)
    {
        if (state)
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minidleTime, maxidleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = Destinations[randNum];
        animator.ResetTrigger("idle");
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = Destinations[randNum];
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }
}
