using UnityEngine;

public class HideOutCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float MouseSensitivity = 30f;
    [SerializeField] private float MinRotX = -45f;
    [SerializeField] private float MaxRotX = -90f;

    private Camera cam;
    private float RotationX;

    private void Start()
    {
        cam = GetComponent<Camera>();
        RotationX = transform.localEulerAngles.y;
    }

    private void Look()
    {

        RotationX += Input.GetAxis("Mouse X");

        RotationX = Mathf.Clamp(RotationX, MinRotX, MaxRotX);

        //transform.localEulerAngles = new Vector3(0, RotationX, 0);

        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, RotationX, 0), 1);

    }

    private void Update()
    {
            Look();
    }


}
