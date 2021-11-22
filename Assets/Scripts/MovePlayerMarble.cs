using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerMarble : MonoBehaviour
{
    private Vector3 direction = new Vector3(0, 0, 0);
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        direction = direction * -1;

        if (other.gameObject.tag != "Marble") return;

        if (transform.localScale.x > other.transform.localScale.x)
        {
            Destroy(other.gameObject);
            //Kill(other.gameObject);

            //Mix the scales
            float newScale = transform.localScale.x + (other.transform.localScale.x * 0.33f);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(other.gameObject.name + " is Destroyed");

            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + other.gameObject.GetComponent<Rigidbody>().mass * 0.33f);
        }
    }
}
