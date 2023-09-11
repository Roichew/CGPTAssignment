using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class TutorialEnemyAI : MonoBehaviour
{
    [Header("Nav Mesh")]
    [SerializeField] public NavMeshAgent AI;

    [Space(10)]
    [SerializeField] public List<Transform> Destinations;
    [Space(10)]
    [Header("AI Game Object")]
    [SerializeField] public GameObject enemyObject;
    [Space(10)]
    [SerializeField] public Animator animator;
    public AudioSource SpawnSound,DespawnSound,JumpscareSound,HeartBeatSound;
    [SerializeField] public float walkSpeed, chaseSpeed;
    [SerializeField] public float viewDistance, catchDistance;
    [SerializeField] public float jumpscareTime, chaseTime, minChaseTime, maxChaseTime;
    public bool walking, chasing, despawn;
    //public Transform spawnLocation;
    [Space(10)]
    [Header("Player")]
    [SerializeField] public Transform player;
    Transform currentDest;
    Vector3 dest;
    //public int spawnNumber;
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

        currentDest = Destinations[destinationAmount];
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, viewDistance)) { 
            if(hit.collider.gameObject.tag == "Player")
            {
                walking = false;
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
                animator.SetTrigger("jumpscare");
                JumpscareSound.Play();
                HeartBeatSound.Play();
                StartCoroutine("deathRoutine");
                chasing = false;
            }
        }

        if (walking == true)
        {

            dest = currentDest.position;
            AI.destination = dest;
            AI.speed = walkSpeed;
            AI.updateRotation = true;
            Debug.Log(Vector3.Distance(transform.position, currentDest.position));
            if (Vector3.Distance(transform.position, currentDest.position) < 3f)
            {
                Debug.Log(transform.position);
                enemyObject.SetActive(false);
                //DespawnSound.Play();
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
