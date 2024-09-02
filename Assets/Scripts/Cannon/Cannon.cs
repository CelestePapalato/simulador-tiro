using System;
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


    private void OnEnable()
    {
        SimulationManager.onFireForceUpdated += UpdateFireForce;
        SimulationManager.onProjectileMassUpdated += UpdateProjectileMass;
        SimulationManager.onShoot += Shoot;
    }

    private void OnDisable()
    {
        SimulationManager.onFireForceUpdated -= UpdateFireForce;
        SimulationManager.onProjectileMassUpdated -= UpdateProjectileMass;
        SimulationManager.onShoot -= Shoot;
    }

    private void UpdateFireForce(float value)
    {
        FireForce = value;
    }

    private void UpdateProjectileMass(float value)
    {
        ProjectileMass = value;
    }

    private void Shoot()
    {
        Vector3 force = projectileSpawnPoint.up * FireForce;
        Projectile instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
        instance.FireForce = force;
        instance.Mass = projectileMass;
    }

}
