using UnityEngine;
using UnityEngine.UI;

public class UIHint : MonoBehaviour
{
    [SerializeField] Collider Interactable;
    [SerializeField] float MinDistance = 3.5f;
    [SerializeField] LayerMask Layer;

    private Camera playerCam;
    private Text UITextDisplay;

    private void Start()
    {
        UITextDisplay = GetComponent<Text>();
        playerCam = Camera.main;
    }

    private bool Raycast()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        return Physics.Raycast(ray, out RaycastHit hitinfo, MinDistance, Layer);
    }

    private void Update()
    {
        if(Raycast())
        {
            UITextDisplay.enabled = true;
        }

        else
        {
             UITextDisplay.enabled = false;
        }
    }
}
