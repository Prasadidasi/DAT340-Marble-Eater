using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour
{
    [SerializeField] private Transform marblePrefab;
    [SerializeField, Range(0, 200)] private int marbleNum = 60;
    private Transform[] _marbles;
    [SerializeField] private int speed = 0;
    [SerializeField] private int startupTime = 10;
    [SerializeField] private float GrowthRate = 0.2f;
    [SerializeField] private float maxMarbleSize = 4;
    [SerializeField] private Transform SpawnArea;
    [SerializeField] private float MarbleSizeModifier = 15;

    private bool _gameStart = false;
    private bool _playerDeadFlag = true;
    private int _realStartupTime;

    void Start()
    {
        AddObserver();
        _realStartupTime = startupTime + 3;
        _marbles = new Transform[marbleNum];
        Agent.Instance.PlayerMarbleScale /= MarbleSizeModifier;
        Debug.Log("PlayerMarbleScale:" + Agent.Instance.PlayerMarbleScale);
        marblePrefab.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.y) /
                                  MarbleSizeModifier;
       // Debug.Log("Parent scale: " + marblePrefab.localScale);
        for (int i = 0; i < marbleNum; i++){
            _marbles[i] = Instantiate(marblePrefab);
            _marbles[i].GetComponent<Transform>().parent = transform;
            _marbles[i].GetComponent<Transform>().position = _marbles[i].GetComponent<Transform>().parent.position;
            _marbles[i].GetComponent<MoveMarble>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            _marbles[i].GetComponent<MoveMarble>().direction *= speed;
            _marbles[i].GetComponent<MoveMarble>().growthRate = GrowthRate;
            _marbles[i].GetComponent<MoveMarble>().maxMarbleSize = marblePrefab.localScale.y * maxMarbleSize;
            _marbles[i].GetComponent<MoveMarble>().ChangeColor(Agent.Instance.PlayerMarbleScale);
            _marbles[i].GetComponent<MoveMarble>().SpawnArea = SpawnArea;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (getPlayerDeath() == 1 && _playerDeadFlag)
        { 
            changeMarbleEating(false);
            _playerDeadFlag = false;
            Debug.Log("Marbles Eating each other again");
        }
        if (_gameStart == false && Time.realtimeSinceStartup > _realStartupTime)
        {
            _gameStart = true;
            foreach (var child in _marbles)
            {
                child.gameObject.GetComponent<MoveMarble>().GameStart = _gameStart;
                child.gameObject.GetComponent<MoveMarble>().canEatMarbles = true;
            }
            Debug.Log("Game Started!");
        }

        int timer = _realStartupTime - (int)Time.realtimeSinceStartup;
        if (_gameStart == false)
            Debug.Log("Game Starts In " + timer);
        NotifyTimer(timer);
    }

    public void changeMarbleEating(bool isPlayerDead)
    {
        if (isPlayerDead == true)
        {
            Debug.Log("Player Died, everything stops");
            foreach (var child in _marbles)
            {
                child.gameObject.GetComponent<MoveMarble>().canEatMarbles = false;
                child.gameObject.GetComponent<Rigidbody>().AddForce(-new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) *40, ForceMode.VelocityChange);
            }
        }
        else
        {
            Debug.Log("Player respawned, Start everything back");
            foreach (var child in _marbles)
            {
                child.gameObject.GetComponent<MoveMarble>().canEatMarbles = true;
                child.gameObject.GetComponent<MoveMarble>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); ;
            }
        }
    }

    //Observer event, get player state at all times
    int getPlayerDeath()
    {
        if (Agent.Instance.playerStatus == 1)
            _playerDeadFlag = true;
        return Agent.Instance.playerStatus;
    }


    //Setup the Agent
    private void AddObserver()
    {
        Agent.Instance.OnPlayerScaleChangeEvent += ForwardScaleChange;
        Agent.Instance.OnWorldYScaleChangeEvent += OnWorldYScaleChange;
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

    private void OnWorldYScaleChange(float scale)
    {
        foreach (var marble in _marbles)
        {
            if (marble.gameObject.activeInHierarchy)
            {
                marble.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
    private void NotifyTimer(int timer)
    {
        Agent.Instance.GameStartTimer = timer;
    }
    private void OnWorldScaleChange(float scale)
    {
        Debug.LogError(scale);
        foreach (var marble in _marbles)
        {
            if (marble.gameObject.activeInHierarchy)
            {
                marble.transform.localScale = new Vector3(scale, scale, scale) / MarbleSizeModifier;
            }
        }
    }
}