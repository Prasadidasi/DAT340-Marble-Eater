using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour
{
    public bool Alive { get; set; }
    public Vector3 direction = new Vector3(0, 0, 0);
    public bool GameStart = false;
    public float growthRate;
    public float maxMarbleSize;
    public Transform SpawnArea;
    public bool canEatMarbles = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
        // Color randomColor = ;
        float scale = 1.5f * Random.value + 0.3f;
        // GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //ChangeColor(Agent.Instance.PlayerMarbleScale);
        GetComponent<Transform>().localScale *= scale;
        GetComponent<Rigidbody>().mass = scale;

    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        direction = direction * -1;
        if (!canEatMarbles) return;
        if (GameStart == false) return;
        if (other.gameObject.tag != "Marble" && other.gameObject.tag != "PlayerMarble")
            return;

        if (transform.localScale.x > other.transform.localScale.x)
        {
            if (gameObject.transform.localScale.x < maxMarbleSize)
            {
                Eat(other.gameObject);
                if (other.gameObject.CompareTag("Marble"))
                    Respawn(other.gameObject);
            }
        }
    }
    private void Eat(GameObject marble)
    {
        ChangeColor(Agent.Instance.PlayerMarbleScale);
        if (marble.CompareTag("PlayerMarble"))
        {
            Debug.Log("Player Eaten");
            marble.GetComponent<MovePlayerMarble>().handlePlayerDeath();
            gameObject.GetComponentInParent<PSController>().changeMarbleEating(true);
            return;
        }
        if (marble.CompareTag("Marble"))
        {
            marble.gameObject.SetActive(false);

            //Mix the scales
            float newScale = transform.localScale.x + Mathf.Abs((marble.transform.localScale.x) * growthRate);
            if (newScale > maxMarbleSize)
                newScale = maxMarbleSize;

            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(marble.gameObject.name + " is Destroyed");

            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + marble.gameObject.GetComponent<Rigidbody>().mass * growthRate);
        }
    }

    private Vector3 generateSpawnLocation()
    {
        float x = Random.Range(-1f, 1f) * SpawnArea.localScale.x / 2 + SpawnArea.position.x;
        float y = Random.Range(-1f, 1f) * SpawnArea.localScale.y / 2 + SpawnArea.position.y;
        float z = Random.Range(-1f, 1f) * SpawnArea.localScale.z / 2 + SpawnArea.position.z;
        return new Vector3(x, y, z);
    }
    public void Respawn(GameObject marble)
    {
        Vector3 spawnLoc = generateSpawnLocation();
        marble.gameObject.transform.position = spawnLoc;
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
