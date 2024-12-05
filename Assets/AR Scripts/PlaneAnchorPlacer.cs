using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneAnchorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private GameObject toiletPrefab;
    [SerializeField] private GameObject tablePrefab;

    [SerializeField] private Vector3 contentOffset = new Vector3(0, .05f, 0);

    [SerializeField] private Button toggleButton;

    [SerializeField] private Button chairButton;
    [SerializeField] private Button toiletButton;
    [SerializeField] private Button tableButton;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits;
    private Color normalColor;
    private bool isPlaneVisible;
    private int activePrefab;



    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        normalColor = chairButton.colors.normalColor;
        isPlaneVisible = true;
        activePrefab = 3;
        hits = new List<ARRaycastHit>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction, Color.red, 1.5f);

            if (raycastManager.Raycast(r, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                if (activePrefab != 3) {
                    AnchorContent(hits[0]);
                }
            }
        }

        if(isPlaneVisible == false)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }

    private void AnchorContent(ARRaycastHit hit)
    {
        if (IsPlaneHorizontal(hit))
        {
            GameObject newAnchor = new GameObject("Anchor");
            newAnchor.transform.parent = null;
            newAnchor.transform.position = hit.pose.position;
            newAnchor.AddComponent<ARAnchor>();

            if (activePrefab == 0)
            {
                GameObject content = Instantiate(chairPrefab, newAnchor.transform);
                content.transform.localPosition = contentOffset;
                activePrefab = 3;
            }
            if (activePrefab == 1)
            {
                GameObject content = Instantiate(toiletPrefab, newAnchor.transform);
                content.transform.localPosition = contentOffset;
                activePrefab = 3;
            }
            else if (activePrefab == 2)
            {
                GameObject content = Instantiate(tablePrefab, newAnchor.transform);
                content.transform.localPosition = contentOffset;
                activePrefab = 3;
            }
     
        }
        /*else if (IsPlaneVertical(hit)) // For Vertical implementationn
        {
            GameObject newAnchor = new GameObject("Anchor");
            newAnchor.transform.parent = null;
            newAnchor.transform.position = hit.pose.position;
            newAnchor.AddComponent<ARAnchor>();

            if (activePrefab == 1)
            {
                GameObject content = Instantiate(paintingPrefab, newAnchor.transform);
                content.transform.localPosition = contentOffset;
                activePrefab = 3;
            }

        }*/

        ResetButtonColors();

    }

    private bool IsPlaneHorizontal(ARRaycastHit hit)
    {
        Vector3 normal = hit.pose.rotation * Vector3.up; 
        return Vector3.Dot(normal, Vector3.up) > 0.5f; 
    }

    /*private bool IsPlaneVertical(ARRaycastHit hit)
    {
        Vector3 normal = hit.pose.rotation * Vector3.up;
        return Mathf.Abs(Vector3.Dot(normal, Vector3.up)) < 0.5f && Mathf.Abs(Vector3.Dot(normal, Vector3.down)) < 0.5f;
    }*/


    public void OnToggleButtonPressed()
    {
        isPlaneVisible = !isPlaneVisible;

        if (isPlaneVisible == true)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
        }
    }

    public void OnChairButtonPressed()
    {
        activePrefab = 0;
        HighlightSelectedButton(chairButton);
    }

    public void OnToiletButtonPressed()
    {
        activePrefab = 1;
        HighlightSelectedButton(toiletButton);
    }

    public void OnTableButtonPressed()
    {
        activePrefab = 2;
        HighlightSelectedButton(tableButton);
    }

    private void ResetButtonColors()
    {
        chairButton.image.color = normalColor;
        toiletButton.image.color = normalColor;
        tableButton.image.color = normalColor;
    }

    private void HighlightSelectedButton(Button selectedButton)
    {
        ResetButtonColors();

        selectedButton.image.color = Color.green;
    }
}
