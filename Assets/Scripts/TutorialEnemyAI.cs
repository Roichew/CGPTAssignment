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
    public GameObject enemyObject;
    public Animator animator;
    public float walkSpeed, chaseSpeed, minidleTime, maxidleTime, idleTime;
    public float viewDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime;
    public float jumpscareTime;
    public bool walking, chasing, despawn;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;

    void Start()
    {
        EnemyState(false);
    }
    void Update()
    {
        walking = true;
        despawn = false;
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
                StartCoroutine("chaseRoutine");
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
            if(AI.remainingDistance <= AI.stoppingDistance)
            {
                AI.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
       

    }

    public void EnemyState(bool state)
    {
        if (state==false)
        {
            enemyObject.SetActive(false);

        } else if (state==true)
        {
            enemyObject.SetActive(true);
        }
    }

    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseRoutine");
        currentDest = Destinations[0];
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minidleTime, maxidleTime);
        yield return new WaitForSeconds(idleTime);
        walking=true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = Destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        stopChase();
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }
}
