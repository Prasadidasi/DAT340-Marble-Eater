using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleParticleSystem : MonoBehaviour
{
    [SerializeField] private Marble marblePrefab;
    [SerializeField, Range(30, 200)] private int marbleNum = 60;
    private Marble[] _marbles;

    void Awake()
    {
        _marbles = new Marble[marbleNum];
        for (int i = 0; i < marbleNum; i++)
        {
            _marbles[i] = Instantiate(marblePrefab, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //DO Something
    }
}