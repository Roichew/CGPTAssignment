using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSense = 300f;
    public Transform playerBody;
    public float camLookStart = 90f;
    public float camLookEnd = -90f;
    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        //hide and lock cursor to the center
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltatime(amount of time gone by when update is called)
        float mouseX = Input.GetAxis("Mouse X")*mouseSense*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSense*Time.deltaTime;

        //To rotate camera around
        xRotation -= mouseY;

        //limits player rotation
        xRotation=Mathf.Clamp(xRotation, camLookEnd, camLookStart);

        //Quarternion responsible for rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
        

    }
}
