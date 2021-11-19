
using UnityEngine;

public class Target : MonoBehaviour
{
    public float thrustForce = 50; 
    private Rigidbody rb;
    
    public void selectMarble(GameObject marble, Vector3 normal)
    {
        rb = marble.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(normal*thrustForce, ForceMode.Impulse); //push away from normal 
            Debug.Log("Force Applied");
        }
    }
}
