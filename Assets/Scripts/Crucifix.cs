using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucifix : MonoBehaviour
{
    public GameObject crucifixPrefab; // Prefab of the crucifix object
    public LayerMask ghostLayer; // Layer containing the ghost objects

    private GameObject currentCrucifix; // The instantiated crucifix in the player's hand
    private bool isEquipped = false; // Flag to track if the crucifix is equipped

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isEquipped) {
                EquipCrucifix();
            }
            else
            {
                EquipCrucifix();
            }
            
        }
        if (isEquipped)
        {
            // Check if the player is clicking the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // Perform a raycast to check if the player is facing a ghost
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ghostLayer))
                {
                    // Despawn the ghost
                    GameObject ghost = hit.collider.gameObject;
                    Destroy(ghost);
                }
            }
        }
    }

    public void PickUpCrucifix()
    {
        if (!isEquipped)
        {
            // Instantiate the crucifix prefab in the player's hand
            currentCrucifix = Instantiate(crucifixPrefab, transform.position, Quaternion.identity);
            currentCrucifix.transform.SetParent(transform); // Attach crucifix to player's hand
            currentCrucifix.transform.localPosition = Vector3.zero; // Adjust position

            isEquipped = true;
        }
    }

    public void EquipCrucifix()
    {
        if (currentCrucifix != null)
        {
            isEquipped = true;
            currentCrucifix.SetActive(true);
        }
    }

    public void UnequipCrucifix()
    {
        if (currentCrucifix != null)
        {
            isEquipped = false;
            currentCrucifix.SetActive(false);
        }
    }
}