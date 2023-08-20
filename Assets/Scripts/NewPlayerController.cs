
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class NewPlayerController : MonoBehaviour
{
    //to get reference from the camera
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 5f;
    [SerializeField] float acceleration = 20f;

    Vector2 look;
    //internal to expose variable ouside of this file
    internal Vector3 velocity;
    CharacterController characterController;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction sprintMovement;
    public AudioSource walkingSound, runningSound;
    bool isRun = false, isWalk = false;
    public event Action OnBeforeMove;

    internal float movementSpeedMultiplier;
    // Start is called before the first frame update
    void Awake()
    {
        isWalk = true;
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        sprintMovement = playerInput.actions["Sprint"];
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateGravity();
    }

    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        //to check if theres anything to run before 
        OnBeforeMove?.Invoke();

        var input = GetMovementInput();

        //to make movement less snappy
        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x,input.x,factor);
        velocity.z = Mathf.Lerp(velocity.z,input.z,factor);
        characterController.Move(velocity*Time.deltaTime);
    }
    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;

        //is grounded to check if character is on the floor
        //if grounded, set to -1 if not set it to vertical velocity + gravity
        velocity.y = characterController.isGrounded ? -1f : velocity.y + gravity.y;

    }
    Vector3 GetMovementInput()
    {
        //var x = Input.GetAxis("Horizontal");
        //var y = Input.GetAxis("Vertical");
        var moveInput = moveAction.ReadValue<Vector2>();
        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        var sprintInput = sprintMovement.ReadValue<float>();

        //if sprint input more than 0 times 1.5f of sprint input
        var multiplier = sprintInput > 0 ? 1.5f :1f;
        input *= movementSpeed * movementSpeedMultiplier;
        return input;

    }
}
