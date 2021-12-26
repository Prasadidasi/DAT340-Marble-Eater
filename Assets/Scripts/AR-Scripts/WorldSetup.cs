using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSetup : MonoBehaviour
{
    [SerializeField] GameObject ScanUI;
    [SerializeField] GameObject WorldSettingUI;
    ScanController ScanController;
    [HideInInspector] public ARImageTracker ARImageTracker;
    [HideInInspector] public RadioButtonController RadioButtonController;

    public bool isScanning, isWorldSpawned;
    // Start is called before the first frame update
    void  Start()
    {
        ScanController = ScanUI.GetComponent<ScanController>();
        ScanController.WorldSetup = this;
        ScanController.Enable();

        ARImageTracker = GetComponent<ARImageTracker>();
        ARImageTracker.WorldSetup = this;

        RadioButtonController = WorldSettingUI.GetComponent<RadioButtonController>();
        RadioButtonController.WorldSetup = this;
        UpdateBoolChecks();
    }

    public void UpdateBoolChecks() {

        isWorldSpawned = ARImageTracker.isWorldSpawned;
        if (isWorldSpawned == true)
        {
            ScanController.Disable();
            RadioButtonController.Enable();
        }
        isScanning = ScanController.isScanning;
    }
}
