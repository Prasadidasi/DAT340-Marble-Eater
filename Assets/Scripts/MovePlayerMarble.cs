using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovePlayerMarble : MonoBehaviour
{
    private Vector3 direction = new Vector3(0, 0, 0);
    [SerializeField] private int lives = 5;
    void Start()
    {
        NotifyScaleChange();
        NotifyLives();
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        direction = direction * -1;

        if (!other.gameObject.CompareTag("Marble")) return;

        if (transform.localScale.x > other.transform.localScale.x)
        {
            //Mix the scales
            //Eat(other.gameObject);
            //NotifyScaleChange();
            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + other.gameObject.GetComponent<Rigidbody>().mass * 0.33f);
        }
    }
    private void Eat(GameObject marble)
    {
        if (marble.CompareTag("Marble"))
        {
            marble.gameObject.SetActive(false);

            //Mix the scales
            float newScale = transform.localScale.x + (marble.transform.localScale.x * 0.33f);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log(marble.gameObject.name + " is Destroyed");

            //Mix the colors
            Color newColor = (GetComponent<Renderer>().material.color + marble.gameObject.GetComponent<Renderer>().material.color * 0.33f) / 2;
            GetComponent<Renderer>().material.color = newColor;
            
            //Mix the mass
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + marble.gameObject.GetComponent<Rigidbody>().mass * 0.33f);
            
            //Notify the Delegate
            NotifyScaleChange();
            Agent.Instance.KilledMarbles++;
        }
    }

    private void NotifyScale()
    {
        Agent.Instance.PlayerMarbleScale = transform.localScale.x;
    }
    // Subject notifies the Agent when scale changes.
    // Agent forwards the notification to Particle System
    // then Particle System calls Observers' event function
    private void NotifyScaleChange()
    {
        if (Agent.Instance.OnScaleChangeEvent != null)
        {
            NotifyScale();
            Agent.Instance.OnScaleChangeEvent(Agent.Instance.PlayerMarbleScale);
        }
    }

    private void NotifyLives()
    {
        Agent.Instance.PlayerMarbleLives = lives;
    }
    
}
