using UnityEngine;

public class CupManager : MonoBehaviour
{
    private GameObject[] Cups;

    // Don't change it 
    [SerializeField] public int CupsCounter;

    // Static constructor
    public static CupManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Get all the loaded cups in the scene
        Cups = GameObject.FindGameObjectsWithTag("Cup");

        // Get number of cups
        CupsCounter = Cups.Length;
    }
}
