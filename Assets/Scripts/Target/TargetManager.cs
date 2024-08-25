using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private float targetMass;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float breakTorque;

    public static event Action<float> TargetMassUpdated;
    public static event Action<float> BreakForceUpdated;
    public static event Action<float> BreakTorqueUpdated;

    public float TargetMass
    {
        get => targetMass;
        set
        {
            if (value > 0)
            {
                targetMass = value;
                TargetMassUpdated(targetMass);
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
                BreakForceUpdated(breakForce);
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
                BreakTorqueUpdated(breakTorque);
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

    private void Start()
    {
        TargetMassUpdated(targetMass);
        BreakForceUpdated(breakForce);
        BreakTorqueUpdated(breakTorque);
    }
}
