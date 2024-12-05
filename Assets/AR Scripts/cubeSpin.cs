using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpin : MonoBehaviour
{
    [SerializeField] GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(Vector3.up, 30f * Time.deltaTime);
    }
}
