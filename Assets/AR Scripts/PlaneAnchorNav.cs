using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneAnchorNav : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Vector3 contentOffset = new Vector3(0, .05f, 0);

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits;
    private bool isPlaneVisible;
    private bool hasSpawned = false;

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        isPlaneVisible = true;
        hits = new List<ARRaycastHit>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasSpawned) 
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (raycastManager.Raycast(r, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                if (levelPrefab != null)
                {
                    AnchorContent(hits[0]);
                }
            }
        }

        if (!isPlaneVisible)
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
            newAnchor.transform.position = hit.pose.position;
            newAnchor.AddComponent<ARAnchor>();

            GameObject content = Instantiate(levelPrefab);
            content.transform.localPosition = hit.pose.position + contentOffset;

            hasSpawned = true; 
        }
    }

    private bool IsPlaneHorizontal(ARRaycastHit hit)
    {
        Vector3 normal = hit.pose.rotation * Vector3.up;
        return Vector3.Dot(normal, Vector3.up) > 0.5f;
    }

    public void OnToggleButtonPressed()
    {
        isPlaneVisible = !isPlaneVisible;

        if (isPlaneVisible)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
        }
    }
}
