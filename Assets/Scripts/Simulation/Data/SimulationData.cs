using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimulationData
{

    public float RotationX;
    public float RotationY;
    public float FireForce;
    public float BreakForce;
    public float BreakTorque;
    public float ProjectileMass;
    public float TargetMass;
    public int JointsDestroyed;
    private Action TargetHit => () => JointsDestroyed++;
    public Action StopHitCounting => () => Target.onJointBreak -= TargetHit;

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

}