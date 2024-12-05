using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static System.Net.Mime.MediaTypeNames;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panelUI;

    private ARTrackedImageManager _arTrackedImageManager;
    private ARAgentManager arManager;

    private Dictionary<string, GameObject> _arObjects;


    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        panelUI.SetActive(false);

        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        }

        text.text = "Beacon Deactivated";
    }

    private void Update()
    {
        if (arManager == null) {
            arManager = FindObjectOfType<ARAgentManager>();
        }
    }


    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    public void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage image in eventArgs.added)
        {
            UpdateTrackedImage(image);
            Debug.Log("Tracked New Image: " + image.referenceImage.name);
        }

        foreach (ARTrackedImage image in eventArgs.updated)
        {
            UpdateTrackedImage(image);
            Debug.Log("Updated Image: " + image.referenceImage.name);
        }

        foreach (ARTrackedImage image in eventArgs.removed)
        {
            _arObjects[image.referenceImage.name].SetActive(false);
            Debug.Log("Removed Image: " + image.referenceImage.name);
        }

        if (eventArgs.updated.Count == 0 && eventArgs.added.Count == 0)
        {
            text.text = "Beacon Deactivated";
            panelUI.SetActive(true);

            if (arManager != null)
            {
                Debug.Log("No images tracked, stopping all agents");
                arManager.StopAllAgents();
            }
            else
            {
                Debug.LogError("ARManager is null, unable to stop agents");
            }
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited ||
            trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].SetActive(false);
            text.text = "Beacon Deactivated";
            panelUI.SetActive(true);

            if (arManager != null)
            {
                Debug.Log("Image tracking lost, stopping agents");
                arManager.StopAllAgents();
            }
            return;
        }

        if (_arObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject obj = _arObjects[trackedImage.referenceImage.name];
            obj.SetActive(true);
            Vector3 offset = new Vector3(0, 1f, 0f);
            obj.transform.position = trackedImage.transform.position + offset;
            text.text = "Beacon Activated!";
            panelUI.SetActive(false);
        }
    }

}