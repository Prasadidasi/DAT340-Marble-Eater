
using UnityEngine;

public class MarbleCatcher : MonoBehaviour
{
    public float range = 200;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Transform marbleHolder;

    private Rigidbody rigidBody;
    private Target target;
    private Vector3 normal;

    // Update is called once per frame
    void Update()
    {
        if (rigidBody)
        {
            rigidBody.MovePosition(marbleHolder.transform.position);

            if (Input.GetButtonUp("Fire1") && target)
            {
                rigidBody.isKinematic = false;
                target.throwMarble(rigidBody, normal);
                rigidBody = null;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (rigidBody)
            {
                rigidBody.isKinematic = false;
                rigidBody = null;
                target = null;
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
            Debug.Log(hitMarble.transform.name);
            target = hitMarble.transform.GetComponent<Target>();
            if (target != null)
            {
                rigidBody = hitMarble.collider.gameObject.GetComponent<Rigidbody>();
                normal = hitMarble.normal;
                if (rigidBody)
                {
                    rigidBody.isKinematic = true;
                    //target.throwMarble(rigidBody, normal);
                }
            }
        }

    }
}
