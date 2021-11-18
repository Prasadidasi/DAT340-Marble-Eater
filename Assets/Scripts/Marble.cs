using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
public class Marble : MonoBehaviour
{
    private float _radius = 1;
    private Rigidbody _rigidbody;
    public int Life { get; set; }
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Life = 1;
        _radius = 1;
        transform.localScale *= _radius;
        _rigidbody.AddRelativeForce(new Vector3(Random.value, Random.value, 0));
    }

    void Update()
    {
        // Do Something
    }

}
