
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NewPlayerController))]
public class playerSprinting : MonoBehaviour
{
    PlayerInput PlayerInput;
    NewPlayerController playerController;
    InputAction sprintMovement;

    [SerializeField] float speedMultiplier = 2f;

    void Awake()
    {
        playerController = GetComponent<NewPlayerController>();
        PlayerInput = GetComponent<PlayerInput>();
        sprintMovement = PlayerInput.actions["Sprint"];
    }

    void OnEnable() => playerController.OnBeforeMove += OnBeforeMove;
    
    void OnDisable() => playerController.OnBeforeMove -= OnBeforeMove;


    void OnBeforeMove()
    {
        var sprintInput = sprintMovement.ReadValue<float>();
        if (sprintInput == 0) return;
        //to make sure player only sprints when looking forward
        var forwardMovementFactor = Mathf.Clamp01(
            Vector3.Dot(playerController.transform.forward, 
            playerController.velocity.normalized)
            );
        var multiplier = Mathf.Lerp(1f, speedMultiplier, forwardMovementFactor);
        playerController.movementSpeedMultiplier *= multiplier;
    }
}
