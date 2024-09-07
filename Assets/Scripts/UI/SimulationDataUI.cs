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
        SimulationData.onNewSimulation += AddNewSlot;
    }

    private void OnDisable()
    {
        SimulationData.onNewSimulation -= AddNewSlot;
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
