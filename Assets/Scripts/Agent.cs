using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Delegation Pattern for decoupling the particle system and the player marble.
public class Agent : MonoBehaviour
{
    public delegate void PlayerScaleChangeHandler(float scale);

    public delegate void WorldScaleChangeHandler(float scale);

    public PlayerScaleChangeHandler OnPlayerScaleChangeEvent;
    public WorldScaleChangeHandler OnWorldScaleChangeEvent;
    public float WorldYScale { get; set; }
    public float PlayerMarbleScale { get; set; }
    public int PlayerMarbleLives { get; set; }
    public int GameStartTimer { get; set; }
    public int KilledMarbles { get; set; }
    
    public float WorldYScale { get; set; }
    //0 means player has started
    //-1 means player died
    //1 means player resurrected after dying
    public int playerStatus { get; set; }

    public static Agent Instance;
    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;
        Instance.PlayerMarbleScale = 1.5f;
        Instance.PlayerMarbleLives = 0;
        Instance.GameStartTimer = 0;
        Instance.KilledMarbles = 0;
        Instance.playerStatus = 0;
        Instance.WorldYScale = 1f;
        Debug.Log("Agent Initialized!");
    }
    
}
