using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockPick : MonoBehaviour
{
    public Camera cam;
    public Transform innerLock;
    public Transform pickPosition;

    public GameObject AHH;
    public AudioSource Meerp;

    public AudioSource Finished;

    public float maxAngle = 90;
    public float lockSpeed = 10;

    [Range(1, 25)]
    public float lockRange = 10;

    public DoorController doorController;

    public string levelToLoad;

    private float eulerAngle;
    private float unlockAngle;
    private Vector2 unlockRange;

    private float keyPressTime = 0;

    private bool movePick = true;
    HubDoors hub;
    // Start is called before the first frame update
    void Start()
    {
        newLock();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = pickPosition.position;

        if (movePick)
        {
            Vector3 dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

            eulerAngle = Vector3.Angle(dir, Vector3.up);

            Vector3 cross = Vector3.Cross(Vector3.up, dir);
            if (cross.z < 0) { eulerAngle = -eulerAngle; }

            eulerAngle = Mathf.Clamp(eulerAngle, -maxAngle, maxAngle);

            Quaternion rotateTo = Quaternion.AngleAxis(eulerAngle, Vector3.forward);
            transform.rotation = rotateTo;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            movePick = false;
            keyPressTime = 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movePick = true;
            keyPressTime = 0;
        }

        float percentage = Mathf.Round(100 - Mathf.Abs(((eulerAngle - unlockAngle) / 100) * 100));
        float lockRotation = ((percentage / 100) * maxAngle) * keyPressTime;
        float maxRotation = (percentage / 100) * maxAngle;

        float lockLerp = Mathf.LerpAngle(innerLock.eulerAngles.z, lockRotation, Time.deltaTime * lockSpeed);
        
        innerLock.eulerAngles = new Vector3(0, 0, lockLerp);

        if (lockLerp >= maxRotation - 1)
        {
            if (eulerAngle < unlockRange.y && eulerAngle > unlockRange.x)
            {
                Debug.Log("Unlocked!");
                newLock();

                movePick = true;
                keyPressTime = 0;
                //Scare();
                Invoke("Unlocked", 1);
                Finished.Play();
                HubDoors.HotelCompleted = true;
            }

            else
            {
                float randomRotation = Random.insideUnitCircle.x;
                transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomRotation, randomRotation));
            }
        }
    }

    void newLock()
    {
        unlockAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(unlockAngle - lockRange, unlockAngle + lockRange);
    }
    void Unlocked()
    {
       SceneManager.LoadScene(levelToLoad);
    }

    void Scare()
    {
        AHH.SetActive(true);
        Meerp.Play();
        Invoke("JumpImage", 1);
    }

    private void Off()
    {
        AHH.SetActive(false);
    }
}