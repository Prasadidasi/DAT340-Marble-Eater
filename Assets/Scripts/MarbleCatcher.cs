
using UnityEngine;

public class MarbleCatcher : MonoBehaviour
{
    public float range = 100;
    public Camera FPSCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Click();
        }
    }

    void Click()
    {
        RaycastHit hitMarble;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hitMarble, range))
        {
            Debug.Log(hitMarble.transform.name);
            Target target = hitMarble.transform.GetComponent<Target>();
            if (target != null)
            {
                target.selectMarble(hitMarble.transform.gameObject, hitMarble.normal);
            }
        }

    }
}
