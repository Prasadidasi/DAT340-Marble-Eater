using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class ARLight : MonoBehaviour
{
    private Light myLight;
    public ARCameraManager manager;

    void Start()
    {
        myLight = this.GetComponent<Light>();
        RenderSettings.ambientMode = AmbientMode.Skybox;
    }

    void OnEnable()
    {
        manager.frameReceived += handleChange;
    }
    void OnDisable()
    {
        manager.frameReceived -= handleChange;
    }

    void handleChange(ARCameraFrameEventArgs args)
    {
        myLight.intensity = args.lightEstimation.averageBrightness.Value;
        myLight.colorTemperature =
                args.lightEstimation.averageColorTemperature.Value;
        myLight.color = args.lightEstimation.colorCorrection.Value;
        myLight.transform.rotation =
             Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        myLight.intensity = args.lightEstimation.mainLightIntensityLumens.Value;

        RenderSettings.ambientProbe =
                args.lightEstimation.ambientSphericalHarmonics.Value;

    }
}
