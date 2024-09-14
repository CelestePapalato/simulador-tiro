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
        SimulationTracker.OnNewSimulation += AddNewSlot;
        SimulationTracker.OnDataGet += AddArray;
        SimulationTracker.OnDataPutSuccess += UpdateData;
    }

    private void OnDisable()
    {
        SimulationTracker.OnNewSimulation -= AddNewSlot;
        SimulationTracker.OnDataGet -= AddArray;
        SimulationTracker.OnDataPutSuccess -= UpdateData;
    }

    private void AddArray(SimulationData[] dataArray)
    {
        foreach(var item in dataArray)
        {
            AddNewSlot(item);
        }
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
