using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField]
    private PrefabInstancer targetsInstancer;

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

    public static event Action<float> OnTargetMassUpdated;
    public static event Action<float> OnBreakForceUpdated;
    public static event Action<float> OnBreakTorqueUpdated;
    public static event Action<float> OnFireForceUpdated;
    public static event Action<float> OnProjectileMassUpdated;

    public static event Action OnShoot;

    public static SimulationManager Instance { get; private set; }

    public float TargetMass
    {
        get => targetMass;
        set
        {
            if (value > 0)
            {
                targetMass = value;
                OnTargetMassUpdated(targetMass);
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
                OnBreakForceUpdated(breakForce);
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
                OnBreakTorqueUpdated(breakTorque);
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
                OnFireForceUpdated(fireForce);
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
                OnProjectileMassUpdated(projectileMass);
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
        SendSimulationVariables();
    }

    private void SendSimulationVariables()
    {
        OnTargetMassUpdated?.Invoke(targetMass);
        OnProjectileMassUpdated?.Invoke(projectileMass);
        OnFireForceUpdated?.Invoke(fireForce);
        OnBreakForceUpdated?.Invoke(breakForce);
        OnBreakTorqueUpdated?.Invoke(breakTorque);
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        ReloadTargets();
        SimulationTracker.Instance.AddNewData();
        OnShoot();
    }

    [ContextMenu("Reload Targets")]
    public void ReloadTargets()
    {
        targetsInstancer?.InstantiatePrefab();
    }

    [ContextMenu("Post Data")]
    public void PostData()
    {
        SimulationTracker.Instance.PutData();
    }
}
