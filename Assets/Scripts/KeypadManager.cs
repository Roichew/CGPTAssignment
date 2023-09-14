using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Codice.CM.Common.CmCallContext;

public class KeypadManager : MonoBehaviour
{
    private HubDoors hubdoors;
    [SerializeField] public float PressSpeed;
    [SerializeField] public TextMeshPro UI_KPAD_ScreenCode;
    [SerializeField] private GameObject UIWinBoard;

    [Header("Level Names")]
    [SerializeField] private string NextLevelName;

    //for tutorial
    [Header("Tutorial")]
    [SerializeField] private bool Tutorial = false;
    [SerializeField] private int finalNumber;
    
    [Header("Tutorial")]
    [SerializeField] private UnityEvent Correct;
    [SerializeField] private UnityEvent Incorrect;



    // Not-serialized
    private KPAD_Key[] keys;
    public bool isResetting { get; set; }
    

    // Static constructor
    public static KeypadManager Instance;

    [SerializeField] public KeyCode TriggerKey = KeyCode.E;

   
    private void Awake()
    {
        Instance = this;
    }

    // Store the loaded keys 
    private KPAD_Key[] GetKeys()
    {
        KPAD_Key[] keys;
        int keyCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("KPAD"))
            {
                keyCount++;
            }
        }

        keys = new KPAD_Key[keyCount];

        for (int i = 0; i < keyCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name.Contains("KPAD"))
            {
                keys[i] = child.GetComponent<KPAD_Key>();
            }
        }

        return keys;
    }

    public void WriteToScreen(string value)
    {
        string screenCode = UI_KPAD_ScreenCode.text;

        if (screenCode == "00")
        {
            UI_KPAD_ScreenCode.text = value.ToString();
        }

        if (Tutorial == false)
        {
            // Win Condition (Right Code)
            if (Convert.ToInt32(screenCode + value) == CupManager.instance.CupsCounter)
            {
                UI_KPAD_ScreenCode.text += value.ToString();

                UI_KPAD_ScreenCode.color = new Color(0, 1, 0, 1);
                SceneManager.LoadScene(NextLevelName, LoadSceneMode.Single);
                HubDoors.HospitalCompleted = true;
                Correct.Invoke();
                UIWinBoard.SetActive(true);
            }

            // Lose Condition (Wrong Code)
            else if (screenCode.Length + 1 == 2)
            {
                UI_KPAD_ScreenCode.text += value.ToString();

                UI_KPAD_ScreenCode.color = new Color(1, 0, 0, 1);
                Invoke("ResetScreenCode", 1.25f);
                isResetting = true;
                Incorrect.Invoke();
                
                //LVLManager.instance.Invoke("RestartLevel", 1f);

            }
            else if (screenCode.Length < 2)
            {
                UI_KPAD_ScreenCode.text += value.ToString();
            }
        }
        else
        {
            // Win Condition (Right Code)
            if (Convert.ToInt32(screenCode + value) == finalNumber)
            {
                UI_KPAD_ScreenCode.text += value.ToString();
                UI_KPAD_ScreenCode.color = new Color(0, 1, 0, 1);
                Correct.Invoke();
                SceneManager.LoadScene(NextLevelName, LoadSceneMode.Single);
            }

            // Lose Condition (Wrong Code)
            else if (screenCode.Length + 1 == 2)
            {
                UI_KPAD_ScreenCode.text += value.ToString();
                UI_KPAD_ScreenCode.color = new Color(1, 0, 0, 1);
                Incorrect.Invoke();
                Invoke("ResetScreenCode", 1.25f);
                isResetting = true;

            }
            else if (screenCode.Length < 2)
            {
                UI_KPAD_ScreenCode.text += value.ToString();
            }
        }

    }

    private void ResetScreenCode()
    {
        UI_KPAD_ScreenCode.text = "00";
        UI_KPAD_ScreenCode.color = new Color(0.7f, 0.7f, 0.7f, 1);
        isResetting = false;
    }

    private void Start()
    {
        keys = GetKeys();
    }

}
