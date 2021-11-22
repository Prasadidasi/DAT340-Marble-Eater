using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour
{
    public bool Alive { get; set; }
    public Vector3 direction = new Vector3(0,0,0);
    public bool GameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        float scale = 2*Random.value + 0.3f;
        GetComponent<Renderer>().material.SetColor("_Color", randomColor);
        GetComponent<Transform>().localScale *= scale;
        GetComponent<Rigidbody>().mass = scale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       

        gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime, ForceMode.Force);
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        direction = direction * - 1;

        if (GameStart == false) return;
        if (other.gameObject.tag != "Marble") return;

        if (transform.localScale.x > other.transform.localScale.x) {
            Destroy(other.gameObject);
            //Kill(other.gameObject);
            
            //Mix the scales
            float newScale = transform.localScale.x + (other.transform.localScale.x * 0.33f);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(other.gameObject.name + " is Destroyed");

            //Mix the colors
            Color newColor = (GetComponent<Renderer>().material.color + other.gameObject.GetComponent<Renderer>().material.color * 0.33f)/2;
            GetComponent<Renderer>().material.color = newColor;

            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + other.gameObject.GetComponent<Rigidbody>().mass * 0.33f);
        }
    }
    private void Kill(GameObject marble)
    {
        if (marble.CompareTag("Marble"))
        {
            marble.GetComponent<MoveMarble>().GameStart = false;
            marble.GetComponent<MoveMarble>().Alive = false;
            marble.GetComponent<MeshRenderer>().enabled = false;
            marble.GetComponent<Collider>().enabled = false;
            Debug.Log(marble.gameObject.name + "is Killed");
        }
    }
}
