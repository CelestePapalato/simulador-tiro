using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class Target : MonoBehaviour
{
    public FixedJoint joint { get; private set; }
    public Rigidbody rb { get; private set; }

    void Awake()
    {
        joint = GetComponent<FixedJoint>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SimulationManager.onTargetMassUpdated += UpdateMass;
        SimulationManager.onBreakForceUpdated += UpdateBreakForce;
        SimulationManager.onBreakTorqueUpdated += UpdateBreakTorque;
    }

    private void OnDisable()
    {
        SimulationManager.onTargetMassUpdated -= UpdateMass;
        SimulationManager.onBreakForceUpdated -= UpdateBreakForce;
        SimulationManager.onBreakTorqueUpdated -= UpdateBreakTorque;
    }

    private void UpdateMass(float mass)
    {
        rb.mass = mass;
    }

    private void UpdateBreakForce(float breakForce)
    {
        joint.breakForce = breakForce;
    }

    private void UpdateBreakTorque(float breakTorque)
    {
        if (joint == null)
        {
            return;
        }
        joint.breakTorque = breakTorque;
    }
}
