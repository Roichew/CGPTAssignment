using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NotesController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey;
    [SerializeField] private KeyCode OpenKey;


    [Space(10)]
    [SerializeField] private NewPlayerController player;
    
    [Header("Input")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextUI;

    [Space(10)]
    [SerializeField] [TextArea]private string noteText;

    [Space(10)]
    [SerializeField] private UnityEvent openEvent; 
    
    [Space(10)]
    [SerializeField] private GameObject openNote;
    [SerializeField] private GameObject closeNote;

    [Space(10)]
    [SerializeField] private GameObject[] HideObject;

    private bool isOpen = false;
    private bool inReach = false;


    public void showNote()
    {
        openNote.SetActive(false);
        noteTextUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        DisablePlayer(true);
        isOpen = true;
    }



    void DisableNote()
    {
        noteCanvas.SetActive(false);
        DisablePlayer(false);
        isOpen = false;
    }

    void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
    }

    private void Update()
    {
        if (inReach == true)
        {
            Debug.Log("Reached");
            if (isOpen == true)
            {
                if (Input.GetKeyDown(closeKey))
                {
                    Debug.Log(closeKey);
                    DisableNote();
                }

            }
            else
            {
                if (Input.GetKeyDown(OpenKey))
                {
                    Debug.Log(OpenKey);
                    showNote();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openNote.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openNote.SetActive(false);
        }
    }

    void hideobjects()
    {
        for(int i = 0; i < HideObject.Length; i++)
        {
            HideObject[i].gameObject.SetActive(false);
        }
    }
}
