using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationDataUI : MonoBehaviour
{
    [SerializeField]
    SimulationDataSlotUI slotPrefab;

    List<SimulationDataSlotUI> slots = new List<SimulationDataSlotUI>();

    private void OnEnable()
    {
        SimulationTracker.onNewSimulation += AddNewSlot;
        SimulationTracker.onDataPutSuccess += UpdateData;
    }

    private void OnDisable()
    {
        SimulationTracker.onNewSimulation -= AddNewSlot;
        SimulationTracker.onDataPutSuccess -= UpdateData;
    }

    private void AddNewSlot(SimulationData data)
    {
        if (data == null) { return; }
        SimulationDataSlotUI slot = Instantiate(slotPrefab, transform);
        slots.Add(slot);
        slot.SimulationData = data;
        UpdateData();
    }

    public void UpdateData()
    {
        foreach (var slot in slots)
        {
            slot.UpdateData();
        }
    }
}
