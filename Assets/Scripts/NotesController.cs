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
    
    [Header("Notes")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextUI;

    [Space(10)]
    [SerializeField] [TextArea]private string noteText;

    [Space(10)]
    [Header("Events")]
    [SerializeField] private UnityEvent openEvent; 
    [SerializeField] private UnityEvent closeEvent; 
    
    [Space(10)]
    [Header("Interaction UI")]
    [SerializeField] private GameObject openNote;
    [SerializeField] private GameObject closeNote;


    private bool isOpen = false;
    private bool inReach = false;
    private bool isRead = false;
    //TutorialEnemyAI enemyAI;


    public void showNote()
    {
        openNote.SetActive(false);
        closeNote.SetActive(true);
        noteTextUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        DisablePlayer(true);
        isOpen = true;
    }



    void DisableNote()
    {
        noteCanvas.SetActive(false);
        closeNote.SetActive(false);
        DisablePlayer(false);
        isOpen = false;
        closeEvent.Invoke();
        isRead = true;

    }

    void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
    }

    private void Update()
    {
        if (isRead == false)
        {
            if (inReach == true)
            {
                Debug.Log("Reached");
                if (Input.GetKeyDown(OpenKey))
                {
                    Debug.Log(OpenKey);
                    showNote();
                }
            }
            if (isOpen == true)
            {
                if (Input.GetKeyDown(closeKey))
                {
                    Debug.Log(closeKey);
                    DisableNote();
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

}
