using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] public float PressSpeed;
    [SerializeField] public TextMeshPro UI_KPAD_ScreenCode;

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

        if(screenCode == "00")
        {
            UI_KPAD_ScreenCode.text = value.ToString();
        }

        // Win Condition (Right Code)
        else if (Convert.ToInt32(screenCode + value) == CupManager.instance.CupsCounter)
        {
            UI_KPAD_ScreenCode.text += value.ToString();

            UI_KPAD_ScreenCode.color = new Color(0, 1, 0, 1);

            LVLManager.instance.Invoke("TeleportToHub", 1.5f);
        }

        // Lose Condition (Wrong Code)
        else if (screenCode.Length + 1 == 2)
        {
            UI_KPAD_ScreenCode.text += value.ToString();

            UI_KPAD_ScreenCode.color = new Color(1, 0, 0, 1);
            Invoke("ResetScreenCode", 1.25f);
            isResetting = true;

            LVLManager.instance.Invoke("RestartLevel", 1f);

        }
        else if (screenCode.Length < 2)
        {
            UI_KPAD_ScreenCode.text += value.ToString();
        }
    }

    private void ResetScreenCode()
    {
        UI_KPAD_ScreenCode.text = "00";
        UI_KPAD_ScreenCode.color = new Color(0.7f, 0.7f, 0.7f, 1);
        isResetting = false; 
    }

    private void Start(){
        keys = GetKeys();
    }

}
