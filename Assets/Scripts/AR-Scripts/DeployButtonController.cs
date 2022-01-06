using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployButtonController : MonoBehaviour
{
    [HideInInspector] public WorldSetup WorldSetup;
    [HideInInspector] public bool deployPressed = false;
    [HideInInspector] public bool isDeployed;
   

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deployPressed)
        {
           
            isDeployed = true;
            WorldSetup.UpdateBoolChecks();
            deployPressed = false;
        }
        else
        {
            isDeployed = false;
        }
    }

    public void toggleDeploy() //For debugging
    {
        deployPressed = !deployPressed;
       
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
