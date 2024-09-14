using System;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class Target : MonoBehaviour
{
    public static event Action onJointBreak;
    public FixedJoint joint { get; private set; }
    public Rigidbody rb { get; private set; }

    void Start()
    {
        GetRequiredComponents();
    }

    void GetRequiredComponents()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (!joint)
        {
            joint = GetComponent<FixedJoint>();
        }
        rb.mass = SimulationManager.Instance.TargetMass;
        joint.breakForce = SimulationManager.Instance.BreakForce;
        joint.breakTorque = SimulationManager.Instance.BreakTorque;
    }

    private void OnEnable()
    {
        SimulationManager.OnTargetMassUpdated += UpdateMass;
        SimulationManager.OnBreakForceUpdated += UpdateBreakForce;
        SimulationManager.OnBreakTorqueUpdated += UpdateBreakTorque;
    }

    private void OnDisable()
    {
        SimulationManager.OnTargetMassUpdated -= UpdateMass;
        SimulationManager.OnBreakForceUpdated -= UpdateBreakForce;
        SimulationManager.OnBreakTorqueUpdated -= UpdateBreakTorque;
    }

    private void UpdateMass(float mass)
    {
        if (!rb) { return; }
        rb.mass = mass;
    }

    private void UpdateBreakForce(float breakForce)
    {
        if (!joint) { return; }
        joint.breakForce = breakForce;
    }

    private void UpdateBreakTorque(float breakTorque)
    {
        if (!joint) { return; }
        joint.breakTorque = breakTorque;
    }

    private void OnJointBreak(float breakForce)
    {
        onJointBreak?.Invoke();
        joint = null;
    }
}
