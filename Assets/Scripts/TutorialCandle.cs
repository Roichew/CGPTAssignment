using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCandle : MonoBehaviour
{
    public GameObject candle; // The GameObject representing the unlit candle
    public GameObject candleText;
    public GameObject objectsToHide; // Old game objects to hide
    public GameObject[] objectsToShow; // New game objects to show
    bool interactable;
    public Light candleLight; // The light component of the candle
    public ParticleSystem candleParticles; // Particle system for the candle's flame effect

    bool isLit = false;

     void Start()
    {
        // Ensure the candle and its light are initially disabled

        candleLight.enabled = false;
        candleParticles.Stop();
        interactable = false;
    }

     void OnTriggerStay(Collider other)
    {
        if (isLit == false)
        {
            if (other.CompareTag("Reach"))
            {
                candleText.SetActive(true);
                interactable = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
          if (other.CompareTag("Reach"))
            {
                candleText.SetActive(false);
                interactable = false;
            }
    }

    void Update()
    {
        if (interactable == true) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isLit)
                {
                    LightCandle();
                    HideOldObjects();
                    ShowNewObjects();
                }
            }
        }
    }

    void LightCandle()
    {
        isLit = true;
        candle.SetActive(true);
        candleLight.enabled = true;
        candleParticles.Play();
        isLit = true;  
    }

     void HideOldObjects()
    {
        objectsToHide.SetActive(false);
    }

    void ShowNewObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
}
