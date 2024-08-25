using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float mass = 1f;
    [SerializeField] float timeAlive = 5f;

    private Vector3 og_scale;
    private float scaleMultiplier = 1f;
    public float ScaleMultiplier
    {
        get => scaleMultiplier;
        private set
        {
            if(value > 0)
            {
                scaleMultiplier = value;
                transform.localScale = og_scale * scaleMultiplier;
            }
        }
    }

    public float Mass { get => mass; set => mass = (value > 0)? value : mass; }

    public Rigidbody Rigidbody { get; private set; }

    public Vector3 FireForce;

    void Start()
    {
        og_scale = transform.localScale;
        scaleMultiplier = 1f;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.mass = mass;
        Rigidbody.AddForce(FireForce);
        Destroy(gameObject, timeAlive);
    }

}
