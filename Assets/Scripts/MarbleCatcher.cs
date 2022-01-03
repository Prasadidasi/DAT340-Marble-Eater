
using UnityEngine;

public class MarbleCatcher : MonoBehaviour
{
    public float range = 200;
    public float touchSpeedModifier = 0.05f;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Transform marbleHolder;
    public float lerpSpeed = 20f;
    [SerializeField] GameObject playerMarble;

    private Touch touch;
    private Rigidbody rigidBody;
    private SphereCollider sphereCollider;
    private bool isPlayerDead;
    private int playerStatus;

    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;
    private bool isPlayerTouching;

    private void Start()
    {
        playerMarble.GetComponent<Rigidbody>().MovePosition(marbleHolder.transform.position);
        isPlayerDead = false;
        isPlayerTouching = false;
        playerStatus = getPlayerStatus();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectHeight = playerMarble.transform.GetComponent<MeshRenderer>().bounds.size.y / 2;
        objectWidth = playerMarble.transform.GetComponent<MeshRenderer>().bounds.size.x / 2;
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

        if (Input.touchCount == 0)
        {
            isPlayerTouching = false;
        }

        if (rigidBody)
        {
            rigidBody.MovePosition(Vector3.Lerp(rigidBody.position, marbleHolder.transform.position, Time.deltaTime * lerpSpeed));
            if (touch.phase == TouchPhase.Ended)
            {
                if (sphereCollider)
                {
                    sphereCollider.enabled = true;
                }
                rigidBody.isKinematic = false;
                rigidBody.AddForce(FPSCamera.transform.forward * 40f, ForceMode.VelocityChange); //push away from normal 
                //Debug.Log("Throw Marble");
            }
            rigidBody = null;
        }

        if (Input.touchCount > 0)
        {
            isPlayerTouching = true;
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                playerMarble.transform.position = new Vector3(
                    playerMarble.transform.position.x + touch.deltaPosition.x * touchSpeedModifier,
                    playerMarble.transform.position.y + touch.deltaPosition.y * touchSpeedModifier,
                    playerMarble.transform.position.z );
            }
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
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hitMarble;
        if (Physics.Raycast(ray, out hitMarble))
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

    private void LateUpdate()
    {
        if (isPlayerTouching)
        {
            Vector3 viewPos = playerMarble.transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
            playerMarble.transform.position = viewPos;
        }
    }
}
