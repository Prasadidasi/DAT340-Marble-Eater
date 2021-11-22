
using UnityEngine;

public class Target : MonoBehaviour
{
    public float thrustForce = 50; 
    
    public void throwMarble(Rigidbody rb, Vector3 normal)
    {
        if (rb != null)
        {
            rb.AddForce(-normal*thrustForce, ForceMode.VelocityChange); //push away from normal 
            Debug.Log("Force Applied");
        }
    }
}
