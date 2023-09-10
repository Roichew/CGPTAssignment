using UnityEngine;

public class Door : MonoBehaviour
{
    // The minimum distance to trigger the door
    [SerializeField] float MinDistanceToPlayer = 4f;
    
    private Animator anim;
    private Transform player;

    private void Start()
    {
        anim = GetComponent<Animator>();

        // Get player's transform
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Open()
    {
        anim.SetBool("canClose", false);
        anim.SetBool("canOpen", true);
    }

    private void Close()
    {
        // Switches bool
        anim.SetBool("canOpen", false);
        anim.SetBool("canClose", true);
    }

    private void Update()
    {
        // Checks the distance between the door and the player
        if (Vector3.Distance(transform.position, player.position) < MinDistanceToPlayer)
            Open();
        else
            Close();
    }
}
