using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationDataSlotUI : MonoBehaviour
{
    private SimulationData simulationData;

    public SimulationData SimulationData
    {
        get => simulationData;
        set
        {
            simulationData = (value != null) ? value : simulationData;
            UpdateData();
        }
    }

    [SerializeField]
    TMP_Text xRotation;
    [SerializeField]
    TMP_Text yRotation;
    [SerializeField]
    TMP_Text fireForce;
    [SerializeField]
    TMP_Text breakForce;
    [SerializeField]
    TMP_Text breakTorque;
    [SerializeField]
    TMP_Text projectileMass;
    [SerializeField]
    TMP_Text targetMass;
    [SerializeField]
    TMP_Text jointsDestroyed;

    public void UpdateData()
    {
        xRotation.text = (simulationData != null) ? simulationData.RotationX.ToString() : null;
        yRotation.text = (simulationData != null) ? simulationData.RotationY.ToString() : null;
        fireForce.text = (simulationData != null) ? simulationData.FireForce.ToString() : null;
        breakForce.text = (simulationData != null) ? simulationData.BreakForce.ToString() : null;
        breakTorque.text = (simulationData != null) ? simulationData.BreakTorque.ToString() : null;
        projectileMass.text = (simulationData != null) ? simulationData.ProjectileMass.ToString() : null;
        targetMass.text = (simulationData != null) ? simulationData.TargetMass.ToString() : null;
        jointsDestroyed.text = (simulationData != null) ? simulationData.JointsDestroyed.ToString() : null;
    }
}
