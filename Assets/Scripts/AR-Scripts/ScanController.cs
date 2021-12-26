using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanController : MonoBehaviour
{   public bool isScanning { set; get; }
    [HideInInspector] public WorldSetup WorldSetup;


    private void Awake()
    {
        isScanning = false;
    }

    
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        isScanning = false;
        gameObject.SetActive(false);
    }

    public void EnableScan()
    {
        isScanning = true;
        WorldSetup.UpdateBoolChecks();
    }

    public void DisableScan()
    {
        isScanning = false;
        WorldSetup.UpdateBoolChecks();
    }
}
