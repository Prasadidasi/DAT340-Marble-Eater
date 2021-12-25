using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanController : MonoBehaviour
{

    public GameObject ScanUI;
    public bool buttonPressed = false;
    private ARImageTracker ARImageScript;

    private void Awake()
    {
        ScanUI.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        ARImageScript = GetComponent<ARImageTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARImageScript.isWorldSpawned)
        {
            ScanUI.SetActive(false);
        }
    }

    void EnableScan()
    {
        buttonPressed = true;
        Debug.Log("Pressed");
    }

    void DisableScan()
    {
        buttonPressed = false;
    }
}
