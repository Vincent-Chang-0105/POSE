using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRaycast : MonoBehaviour
{
    public float rayDistance = 10f;

    private RaycastHit hitInfo;

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        Ray ray = new Ray(rayOrigin, rayDirection);

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hitInfo, rayDistance))
        {
            Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
        }
    }

    public RaycastHit GetRaycastHit()
    {
        return hitInfo;
    }
}
