using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MannequinScript : MonoBehaviour
{
    public GameObject mannequinCanvas; // Reference to the Canvas to show
    public TextMeshProUGUI mannequinText; // Text for mannequin information
    private Camera arCamera;

    private bool isTouchProcessed = false; // Prevent multiple inputs for a single touch

    private CustomizationManager customizationManager; // Reference to CustomizationManager

    void Start()
    {
        arCamera = Camera.main; // Set the AR Camera
        mannequinCanvas.SetActive(false); // Hide canvas by default

        // Find the CustomizationManager in the scene
        customizationManager = FindObjectOfType<CustomizationManager>();

        if (customizationManager == null)
        {
            Debug.LogError("CustomizationManager not found in the scene!");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !isTouchProcessed)
            {
                isTouchProcessed = true; // Mark the touch as processed

                // Convert the touch position to a ray
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object is a mannequin
                    Mannequin mannequin = hit.collider.GetComponent<Mannequin>();

                    if (mannequin != null)
                    {
                        HandleMannequinInteraction(mannequin);
                    }
                    else
                    {
                        mannequinText.text = "This is not a mannequin.";
                        Debug.Log("Hit object is not a mannequin.");
                    }
                }
                else
                {
                    mannequinText.text = "Raycast did not hit any object.";
                    Debug.Log("Raycast did not hit any object.");
                }
            }
        }
        else
        {
            ResetTouch(); // Reset touch when no touches are detected
        }
    }

    private void HandleMannequinInteraction(Mannequin mannequin)
    {
        // Show the canvas
        mannequinCanvas.SetActive(true);

        // Update the UI
        mannequinText.text = "Interacting with: " + mannequin.mannequinName;

        // Set the active mannequin in the CustomizationManager
        customizationManager.SetActiveMannequin(mannequin);

        Debug.Log($"Now customizing: {mannequin.mannequinName}");
    }


    private void ResetTouch()
    {
        isTouchProcessed = false; // Reset the touch flag
    }
}
