using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour
{
    public bool Alive { get; set; }
    public Vector3 direction = new Vector3(0,0,0);
    public bool GameStart = false;
    public float growthRate = 0.2f;
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
            Eat(other.gameObject);
            Respawn(other.gameObject);            
        }
    }
    private void Eat(GameObject marble)
    {
        if (marble.CompareTag("Marble"))
        {
            marble.gameObject.SetActive(false);

            //Mix the scales
            float newScale = transform.localScale.x + (marble.transform.localScale.x * growthRate);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(marble.gameObject.name + " is Destroyed");

            //Mix the colors
            //Color newColor = (GetComponent<Renderer>().material.color + marble.gameObject.GetComponent<Renderer>().material.color * 0.33f) / 2;
            //GetComponent<Renderer>().material.color = newColor;
            ChangeColor(Agent.Instance.Scale);
            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + marble.gameObject.GetComponent<Rigidbody>().mass * growthRate);
        }
    }
    private Vector3 generateSpawnLocation()
    {
        Transform worldBoundry = gameObject.GetComponentInParent<Transform>();
        float x = Random.Range(-1,1) * worldBoundry.localScale.x / 2;
        float y = Random.Range(-1, 1) * worldBoundry.localScale.y / 2;
        float z = Random.Range(-1, 1) * worldBoundry.localScale.z / 2;
        return new Vector3(x,y,z);
    }
    private void Respawn(GameObject marble)
    {
        marble.gameObject.transform.position = generateSpawnLocation();
        //Debug.Log("Respawn");
        marble.SetActive(true);        
        Vector3 newdirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = Vector3.Scale(direction, newdirection);
 
    }
    
    // Observer event, change color when the player marble's scale changes
    public void OnPlayerMarbleScaleChange(float scale)
    {
        ChangeColor(scale);
    }
    
    // PM > EM, Green; PM < EM, Red; PM == EM, Blue
    public void ChangeColor(float scale)
    {
        Color color = new Color(0, 0, 0);
        float localScale = transform.localScale.x;
        color.r = scale < localScale ? 1 : 0;
        color.g = scale > localScale ? 1 : 0;
        color.b = scale.Equals(localScale) ? 1 : 0;
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
