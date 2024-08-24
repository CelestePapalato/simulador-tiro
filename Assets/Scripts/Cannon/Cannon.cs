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

    public float FireForce { get => fireForce; set => fireForce = (value >= 0) ? value : fireForce; }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        Vector3 force = projectileSpawnPoint.up * FireForce;
        Projectile instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
        instance.FireForce = force;
    }

    public void StringToFireForce(string input)
    {
        float force;
        if(float.TryParse(input, out force))
        {
            FireForce = force;
        }
    }
}
