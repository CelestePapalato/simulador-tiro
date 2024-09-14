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
        SimulationTracker.onDataPost += CheckPostStatus;
    }

    private void OnDisable()
    {
        SimulationTracker.onDataPost -= CheckPostStatus;
    }

    private void CheckPostStatus(bool success)
    {
        canvas.enabled = !success;
    }
}
