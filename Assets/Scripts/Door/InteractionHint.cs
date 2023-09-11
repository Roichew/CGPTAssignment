using UnityEngine;
using TMPro;

public class InteractionHint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float MinDistance = 3.0f;

    // Not-serialized
    private Transform Player;
    private TMP_Text UITextDisplay;

    public bool CanDisplay { get; set; } = true;

    private void Start()
    {
        UITextDisplay = GetComponent<TMP_Text>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private float GetDistanceToPlayer() {
        return Vector3.Distance(transform.position, Player.position);
    }

    private void Update()
    {
         if(GetDistanceToPlayer() < MinDistance && CanDisplay) {
            UITextDisplay.enabled = true;
         }
         else {
            UITextDisplay.enabled = false;
         }


    }

}
