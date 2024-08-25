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
        TargetManager.TargetMassUpdated += UpdateMass;
        TargetManager.BreakForceUpdated += UpdateBreakForce;
        TargetManager.BreakTorqueUpdated += UpdateBreakTorque;
    }

    private void OnDisable()
    {
        TargetManager.TargetMassUpdated -= UpdateMass;
        TargetManager.BreakForceUpdated -= UpdateBreakForce;
        TargetManager.BreakTorqueUpdated -= UpdateBreakTorque;
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
