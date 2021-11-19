using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour
{
    public Vector3 direction = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        float scale = Random.value + 0.3f;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", randomColor);
        gameObject.GetComponent<Transform>().localScale *= scale;
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
        //Vector3.Scale(direction, new Vector3(-1, -1, -1));
        //direction = direction * new Vector3(0.2f, 1, 0);
        //Debug.Log("hit");
    }
}
