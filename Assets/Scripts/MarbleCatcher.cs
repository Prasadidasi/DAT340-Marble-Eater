
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
    private bool isPlayerDead;
    private int playerStatus;

    private void Start()
    {
        playerMarble.GetComponent<Rigidbody>().MovePosition(marbleHolder.transform.position);
        isPlayerDead = false;
        playerStatus = getPlayerStatus();
    }
    void Update()
    {
        playerStatus = getPlayerStatus();
        if (playerStatus == -1)
        {
            playerMarble.GetComponent<Rigidbody>().MovePosition(marbleHolder.transform.position);
            playerMarble.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerMarble.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            playerMarble.transform.localScale = playerMarble.GetComponent<MovePlayerMarble>().initialSize;
        }

        if (isPlayerDead)
            return;

        if (rigidBody)
        {
            rigidBody.MovePosition(Vector3.Lerp(rigidBody.position, marbleHolder.transform.position, Time.deltaTime*lerpSpeed));

            if (Input.GetMouseButtonUp(0))
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

    //Observer event, get player state at all times
    private int getPlayerStatus()
    {
        if (Agent.Instance.playerStatus == -1)
            isPlayerDead = true;
        else
            isPlayerDead = false;
        return Agent.Instance.playerStatus;
    }
}
