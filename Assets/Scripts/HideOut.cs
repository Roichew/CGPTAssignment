using UnityEngine;

public class HideOut : MonoBehaviour
{
    [Header("Settings")]

    // Wardrobe camera
    [SerializeField] private Camera InCamera;

    // Min distance to enter the wardrobe 
    [SerializeField] float RaycastDistance = 2.25f;

    [SerializeField] private KeyCode TriggerKey = KeyCode.E;
    [SerializeField] private KeyCode ExitKey = KeyCode.Q;

    // Not-serialized
    private Collider collider;
    private bool isInside;
    private GameObject player;
    private Camera playerCam;


    private void Start()
    {
        // Get the player's camera (main camera)
        playerCam = Camera.main;

        // Get player gameobject
        player = GameObject.FindGameObjectWithTag("Player");

        // Get wardrobe collider
        collider = GetComponent<Collider>();
    }


    // Gets in
    private void GetIn() { 
        isInside = true;

        // Disable the player and the camera
        playerCam.gameObject.SetActive(false);
        player.SetActive(false);

        // Enable wardrobe camera
        InCamera.gameObject.SetActive(true);
    }

    // Gets out
    private void GetOut(){
        isInside = false;

        // Disable wardrobe camera
        InCamera.gameObject.SetActive(false);
       
        // Enable the player and the camera
        player.SetActive(true);
        playerCam.gameObject.SetActive(true);

    }

    // Check if the player is facing the wardrobe
    private bool RayCast()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        return (collider.Raycast(ray, out RaycastHit hitinfo, RaycastDistance));
    }

    private void Update()
    {
        // Gets in the wardrobe when the player presses the trigger key
        if (Input.GetKeyDown(TriggerKey)){

            // Checks if the player is inside the wardrobe
            if (!isInside)
            {
                if (RayCast())
                {
                    GetIn();
                }
            }
        }
        // Gets in the wardrobe when the player presses the exit key
        else if (Input.GetKeyDown(ExitKey))
        {
            if(isInside)
            {
                GetOut();
            }
        }

    }
}
