using UnityEngine;
using UnityEngine.UI;

public class MaterialColorChanger : MonoBehaviour
{
    public Image shirtImage; // Assign your shirt UI Image here in the Inspector
    public Renderer gameObjectRenderer; // Assign the GameObject Renderer here
    public Button colorChangeButton; // Assign your button here in the Inspector
    public Color[] colors; // Define a list of colors in the Inspector
    private int currentColorIndex = 0;

    void Start()
    {
        // Assign the button click listener
        colorChangeButton.onClick.AddListener(ChangeColor);
    }

    void ChangeColor()
    {
        // Cycle through colors
        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        // Change the shirt UI color
        if (shirtImage != null)
        {
            shirtImage.color = colors[currentColorIndex];
        }

        // Change the GameObject's material color
        if (gameObjectRenderer != null)
        {
            gameObjectRenderer.material.color = colors[currentColorIndex];
        }
    }
}
