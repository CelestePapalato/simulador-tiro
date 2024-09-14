using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoData : MonoBehaviour
{
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        SimulationTracker.OnDataPut += CheckPostStatus;
    }

    private void OnDisable()
    {
        SimulationTracker.OnDataPut -= CheckPostStatus;
    }

    private void CheckPostStatus(bool success)
    {
        canvas.enabled = !success;
    }
}
