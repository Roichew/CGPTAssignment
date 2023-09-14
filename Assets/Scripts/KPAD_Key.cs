using UnityEngine;

public class KPAD_Key : MonoBehaviour
{
    [SerializeField] public int value;

    // ~ Not-serialized
    private Collider collider;
    private float cooldownTimer = 0.0f;
    private const float RaycastDistance = 2.25f;
    private const float CooldownDelay = 0.75f;
    private bool isPress;

    private Vector3 target;

    private float originZ;

    private void Start()
    {
        collider = GetComponent<Collider>();
        originZ = transform.position.z;
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.03f);
    }

    private bool RayCast()
    {
        if (Camera.main != null){
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            return (collider.Raycast(ray, out RaycastHit hitinfo, RaycastDistance));
        }

        return false;
    }

    private void WriteToScreen()
    {
        string screenCode = KeypadManager.Instance.UI_KPAD_ScreenCode.text;
        if(screenCode != "00" && screenCode.Length < 2)
        {
            KeypadManager.Instance.UI_KPAD_ScreenCode.text += value.ToString();
        }
        else
        {
            KeypadManager.Instance.UI_KPAD_ScreenCode.text = value.ToString();
        }
    }

    public void Reset()
    {
        isPress = false;
    }

    public void Press()
    {
        isPress = true;
    }

    private void Update()
    {
        if (cooldownTimer < Time.time && !KeypadManager.Instance.isResetting)
        {
            if (Input.GetKeyDown(KeypadManager.Instance.TriggerKey))
            {
                if (RayCast())
                {
                    Press();
                    KeypadManager.Instance.WriteToScreen(value.ToString());
                    cooldownTimer = Time.time + CooldownDelay;
                }
            }
        }

        if(isPress)
        {
            if(transform.position.z >= target.z - 0.01f)
            {
                Reset();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target, KeypadManager.Instance.PressSpeed);
            }
        }
        else
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y, originZ);
            transform.position = Vector3.Lerp(transform.position, origin, KeypadManager.Instance.PressSpeed);
        }

    }
}
