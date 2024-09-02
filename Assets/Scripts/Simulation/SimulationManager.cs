using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField]
    private float targetMass;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float breakTorque;
    [SerializeField]
    private float fireForce;
    [SerializeField]
    private float projectileMass;

    public static event Action<float> onTargetMassUpdated;
    public static event Action<float> onBreakForceUpdated;
    public static event Action<float> onBreakTorqueUpdated;
    public static event Action<float> onFireForceUpdated;
    public static event Action<float> onProjectileMassUpdated;

    public static event Action onShoot;

    public static SimulationManager Instance;

    public float TargetMass
    {
        get => targetMass;
        set
        {
            if (value > 0)
            {
                targetMass = value;
                onTargetMassUpdated(targetMass);
            }
        }
    }
    public float BreakForce
    {
        get => breakForce;
        set
        {
            if (value > 0)
            {
                breakForce = value;
                onBreakForceUpdated(breakForce);
            }
        }
    }
    public float BreakTorque
    {
        get => breakTorque;
        set
        {
            if (value > 0)
            {
                breakTorque = value;
                onBreakTorqueUpdated(breakTorque);
            }
        }
    }

    public float FireForce
    {
        get => fireForce; 
        set {
            if (value >= 0)
            {
                fireForce = value;
                onFireForceUpdated(fireForce);
            }
        }
    }

    public float ProjectileMass
    {
        get => projectileMass;
        set {
            if (value > 0)
            {
                projectileMass = value;
                onProjectileMassUpdated(projectileMass);
            }
        }
    }

    public void StringToTargetMass(string input)
    {
        float value = -1;
        if (!float.TryParse(input, out value))
        {
            if (string.Equals("infinity", input))
            {
                value = Mathf.Infinity;
            }
        }
        TargetMass = value;
    }

    public void StringToBreakForce(string input)
    {
        float value = -1;
        if (!float.TryParse(input, out value))
        {
            if (string.Equals("infinity", input))
            {
                value = Mathf.Infinity;
            }
        }
        BreakForce = value;
    }

    public void StringToBreakTorque(string input)
    {
        float value = -1;
        if (!float.TryParse(input, out value))
        {
            if (string.Equals("infinity", input.ToLower()))
            {
                value = Mathf.Infinity;
            }
        }
        BreakTorque = value;
    }

    public void StringToFireForce(string input)
    {
        float force;
        if (float.TryParse(input, out force))
        {
            FireForce = force;
        }
    }

    public void StringToProjectileMass(string input)
    {
        float mass;
        if (float.TryParse(input, out mass))
        {
            ProjectileMass = mass;
        }
    }

    private void Start()
    {
        Instance = this;
        onTargetMassUpdated(targetMass);
        onProjectileMassUpdated(projectileMass);
        onFireForceUpdated(fireForce);
        onBreakForceUpdated(breakForce);
        onBreakTorqueUpdated(breakTorque);
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        SimulationData.AddNewData();
        onShoot();
    }

    [ContextMenu("PrintData")]
    public void PrintData() { 
        SimulationData.PrintData();
    }
}
