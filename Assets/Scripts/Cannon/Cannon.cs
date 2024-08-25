using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    Projectile projectile;
    [SerializeField]
    Transform projectileSpawnPoint;
    [SerializeField]
    private float fireForce;
    [SerializeField]
    private float projectileMass;

    public float FireForce { get => fireForce; set => fireForce = (value >= 0) ? value : fireForce; }

    public float ProjectileMass { get => projectileMass; set => projectileMass = (value > 0) ? value : projectileMass; }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        Vector3 force = projectileSpawnPoint.up * FireForce;
        Projectile instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
        instance.FireForce = force;
        instance.Mass = projectileMass;
    }

    public void StringToFireForce(string input)
    {
        float force;
        if(float.TryParse(input, out force))
        {
            FireForce = force;
        }
    }

    public void StringToMass(string input)
    {
        float mass;
        if (float.TryParse(input, out mass))
        {
            ProjectileMass = mass;
        }
    }
}
