using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Delegation Pattern for decoupling the particle system and the player marble.
public class Agent : MonoBehaviour
{
    public delegate void ScaleChangeHandler(float scale);

    public ScaleChangeHandler OnScaleChangeEvent;
    public float PlayerMarbleScale { get; set; }
    public int PlayerMarbleLives { get; set; }
    public int GameStartTimer { get; set; }
    public int KilledMarbles { get; set; }

    public static Agent Instance;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerMarbleScale = 0;
        PlayerMarbleLives = 0;
        GameStartTimer = 0;
        KilledMarbles = 0;
        Instance = this;
    }
    
}
