using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour
{
    [SerializeField] private Transform marblePrefab;
    [SerializeField, Range(0, 200)] private int marbleNum = 60;
    private Transform[] _marbles;
    [SerializeField] private int speed = 1;  
    [SerializeField] private int startupTime = 10;
    [SerializeField] private float GrowthRate = 0.2f;
    [SerializeField] private float maxMarbleSize= 4;
    [SerializeField] private Transform SpawnArea;
    private bool _gameStart = false;
    private int _realStartupTime;
    
    void Start()
    {
        AddObserver();
        _realStartupTime = startupTime + 3;
        _marbles = new Transform[marbleNum];
        Debug.Log("PlayerMarbleScale:"+Agent.Instance.PlayerMarbleScale);
        for (int i = 0; i < marbleNum; i++)
        {
            _marbles[i] = Instantiate(marblePrefab);
            _marbles[i].GetComponent<Transform>().parent = transform;
            _marbles[i].GetComponent<Transform>().position = _marbles[i].GetComponent<Transform>().parent.position;
            _marbles[i].GetComponent<MoveMarble>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            _marbles[i].GetComponent<MoveMarble>().direction *= speed;
            _marbles[i].GetComponent<MoveMarble>().growthRate = GrowthRate;
            _marbles[i].GetComponent<MoveMarble>().maxMarbleSize = maxMarbleSize;
            _marbles[i].GetComponent<MoveMarble>().ChangeColor(Agent.Instance.PlayerMarbleScale);
            _marbles[i].GetComponent<MoveMarble>().SpawnArea = SpawnArea;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameStart == false && Time.realtimeSinceStartup > _realStartupTime)
        {
            _gameStart = true;
            foreach (var child in _marbles)
            {
                child.gameObject.GetComponent<MoveMarble>().GameStart = _gameStart;
            }
            Debug.Log("Game Started!");
        }

        int timer = _realStartupTime - (int) Time.realtimeSinceStartup;
        if (_gameStart == false) Debug.Log("Game Starts In " + timer);
        NotifyTimer(timer);
    }
    
    //Setup the Agent
    private void AddObserver()
    {
        Agent.Instance.OnScaleChangeEvent += ForwardScaleChange;
    }
    
    // Call each marble's observer event function
    private void ForwardScaleChange(float scale)
    {
        foreach (var marble in _marbles)
        {
            if (marble.gameObject.activeInHierarchy)
            {
                marble.GetComponent<MoveMarble>().OnPlayerMarbleScaleChange(scale);
            }
        }
    }

    private void NotifyTimer(int timer)
    {
        Agent.Instance.GameStartTimer = timer;
    }
}