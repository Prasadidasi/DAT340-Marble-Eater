using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.size.y/2;
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.size.x/2;
    }

    void LateUpdate()
    {
        
    }
}
