using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    List<ARAgent> agents;
    private CubeRaycast cubeRaycast;  

    private TextMeshProUGUI cuberayText;

    // Start is called before the first frame update
    void Start()
    {
        agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());

        if (cubeRaycast == null)
        {
            cubeRaycast = FindObjectOfType<CubeRaycast>();
        }

        GameObject cuberayTextObj = GameObject.Find("cuberayText");
        if (cuberayTextObj != null)
        {
            cuberayText = cuberayTextObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("cuberayText GameObject not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cubeRaycast == null)
        {
            cubeRaycast = FindObjectOfType<CubeRaycast>();
            if (cuberayText != null)
            {
                cuberayText.text = "No Cube Found";
            }
        }

        if (cubeRaycast != null)
        {
            if (cuberayText != null)
            {
                cuberayText.text = "Cube Found";
            }

            RaycastHit hitInfo = cubeRaycast.GetRaycastHit();
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Plane"))
            {
                MoveAllAgents(hitInfo.point);
                Debug.Log("Agents Moving to point");
            }
            else
            {
                Debug.Log("No Plane Detected");
            }
        }
    }

    public void MoveAllAgents(Vector3 position)
    {
        foreach (ARAgent agent in agents)
        {
            agent.MoveAgent(position);
        }
    }

    public void StopAllAgents()
    {
        foreach (ARAgent agent in agents)
        {
            agent.StopAgent();
        }
    }
}
