using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class handanimator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator Anim;

    private void Start()
    {
        Anim=GetComponent<Animator>();
    }
    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        Anim.SetFloat("Trigger", triggerValue);
        Anim.SetFloat("Grip", gripValue);

    }
}
