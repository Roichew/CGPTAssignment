using UnityEngine;

public class Door : MonoBehaviour
{
    // The minimum distance to trigger the door
    [SerializeField] float MinDistanceToPlayer = 4f;
    
    private Animator anim;
    private Transform player;

    public bool isOpen { get; set; }

    private void Start()
    {
        anim = GetComponent<Animator>();

        // Get player's transform
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Open()
    {
        anim.SetBool("canClose", false);
        anim.SetBool("canOpen", true);
        isOpen = true;
    }

    public void Close()
    {
        // Switches bool
        anim.SetBool("canOpen", false);
        anim.SetBool("canClose", true);
        isOpen = false;
    }
}
