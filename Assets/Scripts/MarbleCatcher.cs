
using UnityEngine;

public class MarbleCatcher : MonoBehaviour
{
    public float range = 200;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Transform marbleHolder;
    public float lerpSpeed = 20f;
    [SerializeField] GameObject playerMarble;

    private Rigidbody rigidBody;
    private SphereCollider sphereCollider;

    void Update()
    {
        if (rigidBody)
        {
            rigidBody.MovePosition(Vector3.Lerp(rigidBody.position, marbleHolder.transform.position, Time.deltaTime * lerpSpeed));

            if (Input.GetButtonUp("Fire1"))
            {
                if (sphereCollider)
                {
                    sphereCollider.enabled = true;
                }
                rigidBody.isKinematic = false;
                rigidBody.AddForce(FPSCamera.transform.forward * 40f, ForceMode.VelocityChange); //push away from normal 
                rigidBody = null;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (rigidBody)
            {
                rigidBody.isKinematic = false;
                rigidBody = null;
            }
            else
            {
                Click();
            }
        }
    }

    void Click()
    {
        RaycastHit hitMarble;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hitMarble, range))
        {
            rigidBody = playerMarble.GetComponent<Rigidbody>();
            sphereCollider = playerMarble.GetComponent<SphereCollider>();
            if (rigidBody)
            {
                rigidBody.isKinematic = true;
            }
            if (sphereCollider)
            {
                sphereCollider.enabled = false;
            }
        }
    }
}