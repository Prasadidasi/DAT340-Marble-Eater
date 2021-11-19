using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    public int speed = 1;
    public bool randomizeMarbleDirection = false;
    public int StartupTime = 10;
    private bool GameStart = false;
    private int realStartupTime;
    // Start is called before the first frame update
    void Start()
    {
        realStartupTime = StartupTime + 3;
       

        foreach (Transform child in transform)
        {
            if(randomizeMarbleDirection)
                child.GetComponent<MoveMarble>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            child.GetComponent<MoveMarble>().direction *= speed;
        }
    }

    private void Update()
    {
        if (GameStart == false && Time.realtimeSinceStartup > realStartupTime)
        {
            GameStart = true;
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<MoveMarble>().GameStart = GameStart;
            }
            Debug.Log("Game Started!");
        }

        if (GameStart == false) Debug.Log("Game Starts In " + (realStartupTime - (int)Time.realtimeSinceStartup));

    }
}
