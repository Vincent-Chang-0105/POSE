using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurnitureReceiver : MonoBehaviour, ITappable
{
    private float _rotationSpeed = 500.0f;
    private bool isPanning = false;
    private bool isSelected;

    [SerializeField] private string furnitureName;

    [SerializeField] private GameObject selectedHighlight;

    public void OnTap()
    {
        isSelected = !isSelected;
    }

    public void OnDrag(object sender, DragEventArgs args)
    {
        if (isSelected)
        {
            Vector2 deltaPosition = args.TargetFinger.deltaPosition;

            float horizontalDelta = -(deltaPosition.x / Screen.dpi); 
            float verticalDelta = -(deltaPosition.y / Screen.dpi); 

            Vector3 currentPosition = this.transform.position;

            currentPosition.x += horizontalDelta; 
            currentPosition.z += verticalDelta; 

            this.transform.position = currentPosition;
        }
    }


    public void OnTwoFingerPan(object sender, TwoFingerPanEventArgs args)
    {
        if (isSelected) {

            isPanning = true;

            Vector2 deltaPosition0 = args.Finger1.deltaPosition;
            Vector2 deltaPosition1 = args.Finger2.deltaPosition;

            Vector2 averageDeltaPosition = (deltaPosition0 + deltaPosition1) / 2;

            float horizontalDelta = -(averageDeltaPosition.x / Screen.dpi);

            float rotationY = horizontalDelta * _rotationSpeed * Time.deltaTime;

            this.transform.Rotate(0, rotationY, 0);

            Debug.Log("Rotation Change: " + rotationY);
        }

    }

    public bool IsPanning
    {
        get { return this.isPanning; }
    }

    private void Update()
    {
        if (Input.touchCount != 2)
        {
            isPanning = false;
        }

        if (isSelected)
        {
            selectedHighlight.SetActive(true);
        }
        else
        {
            selectedHighlight.SetActive(false);
        }
    }

    void Start()
    {
        selectedHighlight.SetActive(false);

        GestureManager.Instance.OnTwoFingerPan += this.OnTwoFingerPan;
        GestureManager.Instance.OnDrag += this.OnDrag;
    }

    void OnDisable()
    {
        GestureManager.Instance.OnTwoFingerPan -= this.OnTwoFingerPan;
        GestureManager.Instance.OnDrag -= this.OnDrag;
    }
}
