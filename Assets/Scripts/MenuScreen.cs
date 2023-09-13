using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] KeyCode PauseButton;

    [Space(10)]
    [Header("Shinda Screen")]
    [SerializeField] private GameObject DeathScene;
    [SerializeField] public static bool isded;

    [Header("Pause Screen")]
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] public static bool isPaused;


    private void Start()
    {
        PauseScreen.SetActive(false);
        DeathScene.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (isded)
        {
            Death();
        }
    }

    public void Death()
    {
            DeathScene.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1f;
        //By my shaggy bark

    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
