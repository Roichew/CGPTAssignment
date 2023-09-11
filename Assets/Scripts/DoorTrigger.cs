using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private KeyCode TriggerKey = KeyCode.E;
    [SerializeField] float RaycastDistance = 2.25f;

    [SerializeField] private Door door;

    private Camera playerCam;
    private Collider collider;

    private void Start()
    {
        playerCam = Camera.main;
        collider = GetComponent<Collider>();
    }

    private bool Raycast()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        return collider.Raycast(ray, out RaycastHit hitinfo, RaycastDistance);
    }

    private void Update()
    {
        if(Input.GetKeyDown(TriggerKey))
        {
            if(Raycast())
            {
                if (door.isOpen)
                    door.Close();
                else
                    door.Open();
            }
        }
    }
}
