using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public string mannequinName;

    public GameObject[] Tops;
    public GameObject[] Bottoms;

    public int activeTopIndex = 0;
    public int activeBottomIndex = 0;

    private GameObject activeTop;
    private GameObject activeBottom;

    public Color currentTopColor = Color.white; // Default top color
    public Color currentBottomColor = Color.white; // Default bottom color

    public void InitializeClothing()
    {
        // Deactivate all tops and bottoms
        foreach (var top in Tops)
        {
            top.SetActive(false);
        }
        foreach (var bottom in Bottoms)
        {
            bottom.SetActive(false);
        }

        // Activate the currently selected top and bottom
        if (Tops.Length > activeTopIndex)
        {
            activeTop = Tops[activeTopIndex];
            activeTop.SetActive(true);
            SetColor(activeTop, currentTopColor);
        }
        if (Bottoms.Length > activeBottomIndex)
        {
            activeBottom = Bottoms[activeBottomIndex];
            activeBottom.SetActive(true);
            SetColor(activeBottom, currentBottomColor);
        }
    }

    public void UpdateClothing(int topIndex, int bottomIndex)
    {
        // Update top
        if (activeTop != null)
        {
            activeTop.SetActive(false);
        }
        if (Tops.Length > topIndex)
        {
            activeTop = Tops[topIndex];
            activeTop.SetActive(true);
            SetColor(activeTop, currentTopColor);
        }

        // Update bottom
        if (activeBottom != null)
        {
            activeBottom.SetActive(false);
        }
        if (Bottoms.Length > bottomIndex)
        {
            activeBottom = Bottoms[bottomIndex];
            activeBottom.SetActive(true);
            SetColor(activeBottom, currentBottomColor);
        }

        // Save the new indices
        activeTopIndex = topIndex;
        activeBottomIndex = bottomIndex;
    }

    public void SetColor(GameObject clothing, Color color)
    {
        var renderer = clothing.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
