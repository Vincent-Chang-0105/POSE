using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public enum AppearanceDetails
    {
        Top,
        Bottom
    }

    public GameObject[] Tops;
    public GameObject[] Bottoms;

    private Mannequin activeMannequin;

    public void SetActiveMannequin(Mannequin mannequin)
    {
        if (activeMannequin != null)
        {
            // Save the clothing state for the current mannequin
            activeMannequin.UpdateClothing(activeMannequin.activeTopIndex, activeMannequin.activeBottomIndex);
        }

        // Switch to the new mannequin
        activeMannequin = mannequin;

        // Update the customization options
        Tops = mannequin.Tops;
        Bottoms = mannequin.Bottoms;

        // Sync the CustomizationManager indices with the mannequin
        ApplyModification(AppearanceDetails.Top, mannequin.activeTopIndex);
        ApplyModification(AppearanceDetails.Bottom, mannequin.activeBottomIndex);
    }

    public void ChangeTopColor(Color color)
    {
        if (activeMannequin != null && activeMannequin.activeTopIndex < Tops.Length)
        {
            activeMannequin.currentTopColor = color;
            activeMannequin.SetColor(Tops[activeMannequin.activeTopIndex], color);
        }
    }

    public void ChangeBottomColor(Color color)
    {
        if (activeMannequin != null && activeMannequin.activeBottomIndex < Bottoms.Length)
        {
            activeMannequin.currentBottomColor = color;
            activeMannequin.SetColor(Bottoms[activeMannequin.activeBottomIndex], color);
        }
    }

    public void TopUp()
    {
        if (activeMannequin == null) return;

        activeMannequin.activeTopIndex = (activeMannequin.activeTopIndex + 1) % Tops.Length;
        ApplyModification(AppearanceDetails.Top, activeMannequin.activeTopIndex);
    }

    public void TopDown()
    {
        if (activeMannequin == null) return;

        activeMannequin.activeTopIndex = (activeMannequin.activeTopIndex - 1 + Tops.Length) % Tops.Length;
        ApplyModification(AppearanceDetails.Top, activeMannequin.activeTopIndex);
    }

    public void BottomUp()
    {
        if (activeMannequin == null) return;

        activeMannequin.activeBottomIndex = (activeMannequin.activeBottomIndex + 1) % Bottoms.Length;
        ApplyModification(AppearanceDetails.Bottom, activeMannequin.activeBottomIndex);
    }

    public void BottomDown()
    {
        if (activeMannequin == null) return;

        activeMannequin.activeBottomIndex = (activeMannequin.activeBottomIndex - 1 + Bottoms.Length) % Bottoms.Length;
        ApplyModification(AppearanceDetails.Bottom, activeMannequin.activeBottomIndex);
    }

    public void ApplyModification(AppearanceDetails details, int index)
    {
        if (activeMannequin == null) return;

        switch (details)
        {
            case AppearanceDetails.Top:
                activeMannequin.UpdateClothing(index, activeMannequin.activeBottomIndex);
                break;
            case AppearanceDetails.Bottom:
                activeMannequin.UpdateClothing(activeMannequin.activeTopIndex, index);
                break;
        }
    }

    // Functions to change the color of the top
    public void ChangeTopColorRed()
    {
        ChangeTopColor(Color.red);
    }

    public void ChangeTopColorYellow()
    {
        ChangeTopColor(Color.yellow);
    }

    public void ChangeTopColorBlack()
    {
        ChangeTopColor(Color.black);
    }

    // Functions to change the color of the bottom
    public void ChangeBottomColorRed()
    {
        ChangeBottomColor(Color.red);
    }

    public void ChangeBottomColorYellow()
    {
        ChangeBottomColor(Color.yellow);
    }

    public void ChangeBottomColorBlack()
    {
        ChangeBottomColor(Color.black);
    }
}
