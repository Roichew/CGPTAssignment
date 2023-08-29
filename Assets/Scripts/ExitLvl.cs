using UnityEngine;
using UnityEngine.SceneManagement;

public class ExilLvl : MonoBehaviour
{
    public string levelToLoad; // Name of the level you want to load
    bool isIn;

     void Start()
    {
        isIn = false;
    }
     void Update()
    {
        if (isIn)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isIn = true;
        }
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}