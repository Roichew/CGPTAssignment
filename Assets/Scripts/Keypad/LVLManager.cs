using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLManager : MonoBehaviour
{
    // Static Constructor
    public static LVLManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void TeleportToHub()
    {
        SceneManager.LoadScene("HUBHUBHUBHUB", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Hospital", LoadSceneMode.Single);
    }
}
