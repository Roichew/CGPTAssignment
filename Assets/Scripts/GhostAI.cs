using UnityEngine;

// ' Enemy Ghost AI Script '
public class GhostAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed = 4f;
    [SerializeField] private float Height = 10.92f;
    [SerializeField] private Collider[] WanderAreas;
    [SerializeField] private float DurationPerArea;
    [SerializeField] public string RestartLevelName;

    // ~ Not-serialized
    private bool isRotating;
    private Vector3 targetArea;
    private Vector3 targetRotation;
    private Transform player;
    private Animator ghostAnim;
    private int AreaTracker = 0;
    private float _areaTimer = 0.0f;
    private int areaIndex;

    private const float MinSpawnRangeX = 38.0f;
    private const float MaxSpawnRangeX = 46.0f;

    private const float MinSpawnRangeZ = -2.0f;
    private const float MaxSpawnRangeZ = 4.76f;

    private void Spawn()
    {
        float randomPosX = Random.Range(MinSpawnRangeX, MaxSpawnRangeX);
        float randomPosZ = Random.Range(MinSpawnRangeZ, MaxSpawnRangeZ);

        transform.position = new Vector3(randomPosX, Height, randomPosZ);
        areaIndex = 0;
    }

    private void Start()
    {
        Spawn();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghostAnim = GetComponent<Animator>();

        targetArea = WanderAreas[0].transform.position; 
        ghostAnim.SetBool("canWalk", true);
    }

    private void Regulate()
    {
        transform.position =
    new Vector3(transform.position.x, Height, transform.position.z);

    }

    private bool CanWalk() {
        return ghostAnim.GetBool("canWalk");
    }

    // Set target to the next area
    private Vector3 SelectArea()
    {
        int numOfAreas = WanderAreas.Length -1;

        if (AreaTracker + 1 <= numOfAreas) {
            AreaTracker++;
            return WanderAreas[AreaTracker].transform.position;
        }

        AreaTracker = 0;

        return WanderAreas[0].transform.position;
    }

    // Moves towards the target area
    private void MoveToArea(Vector3 targetArea)
    {
        transform.position = Vector3.Lerp(transform.position, targetArea, Speed * Time.deltaTime);
    
        if(GetDistanceToArea(targetArea) < 1.4f)
        {
            ghostAnim.SetBool("canWalk", false);
        }
    }

    private float GetDistanceToArea(Vector3 targetArea){
        return Vector3.Distance(transform.position, targetArea);
    }

    private float GetRandomRotation()
    {
        return Random.Range(-360, 360);
    }

    // Rotates the ghost inside the wander area
    private void WanderRotate() {
        if (!isRotating){
            targetRotation
                = new Vector3(transform.eulerAngles.x, GetRandomRotation(), transform.eulerAngles.z);
            isRotating = true;
        }

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, RotationSpeed * Time.deltaTime);

        if (transform.eulerAngles.y == targetRotation.y)
        {
            isRotating = false;
        }

    }

    private void Update()
    {
        Regulate();

        if (CanWalk())
        {
            MoveToArea(targetArea);

            transform.LookAt(targetArea);
            transform.eulerAngles =
                new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

            _areaTimer = 0.0f;
        }
        else if(!CanWalk() && _areaTimer >= DurationPerArea)
        {
            targetArea = SelectArea();
            ghostAnim.SetBool("canWalk", true);
            _areaTimer = 0.0f;
        }
        else if(GetDistanceToArea(targetArea) <= 5f)
        {
            _areaTimer += Time.deltaTime;
            WanderRotate();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Restarts the level if the player touches the ghost
        if (collider.CompareTag("Player"))
        
        {
            LVLManager.instance.RestartLevel(RestartLevelName);
        }

    }
}
