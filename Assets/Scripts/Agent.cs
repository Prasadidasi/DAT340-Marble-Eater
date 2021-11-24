using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Delegation Pattern for decoupling the particle system and the player marble.
public class Agent : MonoBehaviour
{
    public delegate void ScaleChangeHandler(float scale);

    public ScaleChangeHandler OnScaleChangeEvent;

    public static Agent Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

}
