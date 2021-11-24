using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public delegate void ScaleChangeHandler();

    public ScaleChangeHandler OnScaleChangeEvent;

    public static Agent Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

}
