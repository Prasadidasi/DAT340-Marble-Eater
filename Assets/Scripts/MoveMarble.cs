using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour
{
    public bool Alive { get; set; }
    public Vector3 direction = new Vector3(0,0,0);
    public bool GameStart = false;
    public float growthRate;
    public float maxMarbleSize;
    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
       // Color randomColor = ;
        float scale = 2*Random.value + 0.3f;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
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

            if (gameObject.transform.localScale.x < maxMarbleSize)
            {
            Eat(other.gameObject);
            Resurrect(other.gameObject);
            }
        }
    }
    private void Eat(GameObject marble)
    {
        if (marble.CompareTag("Marble"))
        {
            marble.gameObject.SetActive(false);

            //Mix the scales
            float newScale = transform.localScale.x + (marble.transform.localScale.x * growthRate);
            if (newScale > maxMarbleSize) newScale = maxMarbleSize;
            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(marble.gameObject.name + " is Destroyed");
            
            /*//Mix the colors
            Color newColor = (GetComponent<Renderer>().material.color + marble.gameObject.GetComponent<Renderer>().material.color) / 2;
            GetComponent<Renderer>().material.color = newColor;
            */

            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + marble.gameObject.GetComponent<Rigidbody>().mass * growthRate);
        }
    }
    private Vector3 generateSpawnLocation()
    {
        Transform worldBoundry = gameObject.transform.parent.transform.parent.GetComponent<Transform>(); // (>_<)
        float x = Random.Range(-1f, 1f) * worldBoundry.localScale.x / 2 + worldBoundry.position.x;
        float y = Random.Range(-1f, 1f) * worldBoundry.localScale.y / 2 + worldBoundry.position.y;
        float z = Random.Range(-1f, 1f) * worldBoundry.localScale.z / 2 + worldBoundry.position.z;
        return new Vector3(x,y,z);
    }
    private void Resurrect(GameObject marble)
    {
        Vector3 spawnLoc = generateSpawnLocation();
        marble.gameObject.transform.position = spawnLoc;
        marble.SetActive(true);        
        Vector3 newdirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = Vector3.Scale(direction, newdirection);
 
    }
}
