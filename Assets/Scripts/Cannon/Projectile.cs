using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 scale;
    public float Scale
    {
        get => Scale;
        private set
        {
            if(value > 0)
            {
                scale /= Scale;
                Scale = value;
                scale *= Scale;
            }
        }
    }
    public Rigidbody Rigidbody { get; private set; }

    void Start()
    {
        scale = transform.localScale;
        Scale = 1f;
    }

}
