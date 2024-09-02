using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimulationData
{
    public static List<SimulationData> simulations = new List<SimulationData>();

    public float FireForce { get; private set; }
    public float BreakForce { get; private set; }
    public float BreakTorque { get; private set; }
    public float ProjectileMass { get; private set; }
    public float TargetMass { get; private set; }
    public int TargetsHit { get; private set; }

    private Projectile shottedProjectile;

    private Action TargetHit => () => TargetsHit++;

    public SimulationData()
    {
        BreakForce = SimulationManager.Instance.BreakForce;
        BreakTorque = SimulationManager.Instance.BreakTorque;
        TargetMass = SimulationManager.Instance.TargetMass;
        ProjectileMass = SimulationManager.Instance.ProjectileMass;
        FireForce = SimulationManager.Instance.FireForce;

        Projectile.OnProjectileInstanced += UpdateProjectileReference;

    }

    private void UpdateProjectileReference(Projectile instance)
    {
        if(instance == null) { return; }
        shottedProjectile = instance;
        Projectile.OnProjectileInstanced -= UpdateProjectileReference;
        shottedProjectile.onDestroy += DeleteProjectileReference;
        shottedProjectile.onTargetHit += TargetHit;
    }

    private void DeleteProjectileReference()
    {
        if(shottedProjectile != null)
        {
            shottedProjectile.onDestroy -= DeleteProjectileReference;
            shottedProjectile.onTargetHit -= TargetHit;
            shottedProjectile = null;
        }
    }

    public static void AddNewData()
    {
        simulations.Add(new SimulationData());
    }

    public static void PrintData()
    {
        foreach(SimulationData data in simulations)
        {
            Debug.Log(data + " #================");
            Debug.Log("Fire force: " + data.FireForce);
            Debug.Log("Projectile mass: " + data.ProjectileMass);
            Debug.Log("Target mass: " + data.TargetMass);
            Debug.Log("Break force: " + data.BreakForce);
            Debug.Log("Break torque: " + data.BreakTorque);
            Debug.Log("Hits: " + data.TargetsHit);
            Debug.Log("#================");
        }
    }
}
