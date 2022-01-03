using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployButtonController : MonoBehaviour
{
    [HideInInspector] public WorldSetup WorldSetup;
    [HideInInspector] public bool deployPressed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleDeploy() //For debugging
    {
        deployPressed = !deployPressed;
        Debug.Log("Deploy Pressed: " + deployPressed);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
