using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private TextMeshProUGUI text;  

    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> _arObjects;

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChange;

        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        }

        text.text = "No Image Tracked";
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChange;
    }

    private void OnTrackedImageChange(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].SetActive(false);
        }

        if (eventArgs.updated.Count == 0 && eventArgs.added.Count == 0)
        {
            text.text = "No Image Tracked";
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited ||
            trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].SetActive(false);
            text.text = "No Image Tracked";  
            return;
        }

        if (_arObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject obj = _arObjects[trackedImage.referenceImage.name];
            obj.SetActive(true);
            Vector3 offset = new Vector3(0, 0f, -0.5f); 
            obj.transform.position = trackedImage.transform.position + offset;
            text.text = "Image Tracked!";  
        }
    }
}
