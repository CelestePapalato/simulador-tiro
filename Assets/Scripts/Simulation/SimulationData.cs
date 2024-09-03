using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimulationData
{
    public static List<SimulationData> simulations = new List<SimulationData>();

    public float RotationX {  get; private set; }
    public float RotationY { get; private set; }
    public float RotationZ { get; private set; }

    public float FireForce { get; private set; }
    public float BreakForce { get; private set; }
    public float BreakTorque { get; private set; }
    public float ProjectileMass { get; private set; }
    public float TargetMass { get; private set; }
    public int JointsDestroyed { get; private set; }

    private Action TargetHit => () => JointsDestroyed++;
    private Action StopHitCounting => () => Target.onJointBreak -= TargetHit;

    public SimulationData()
    {
        RotationX = Pivot.Instance.RotationX;
        RotationY = Pivot.Instance.RotationY;
        BreakForce = SimulationManager.Instance.BreakForce;
        BreakTorque = SimulationManager.Instance.BreakTorque;
        TargetMass = SimulationManager.Instance.TargetMass;
        ProjectileMass = SimulationManager.Instance.ProjectileMass;
        FireForce = SimulationManager.Instance.FireForce;

        Target.onJointBreak += TargetHit;

    }

    public static void AddNewData()
    {
        if(simulations.Count > 0)
        {
            simulations[simulations.Count - 1].StopHitCounting();
        }        
        simulations.Add(new SimulationData());
    }

    public static void PrintData()
    {
        foreach(SimulationData data in simulations)
        {
            Debug.Log(data + " #================");
            Debug.Log("X Rotation: " + data.RotationX);
            Debug.Log("Y Rotation: " + data.RotationY);
            Debug.Log("Fire force: " + data.FireForce);
            Debug.Log("Projectile mass: " + data.ProjectileMass);
            Debug.Log("Target mass: " + data.TargetMass);
            Debug.Log("Break force: " + data.BreakForce);
            Debug.Log("Break torque: " + data.BreakTorque);
            Debug.Log("Joints destroyed: " + data.JointsDestroyed);
            Debug.Log("#================");
        }
    }
}
