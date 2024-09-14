using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationTracker
{
    [Serializable]
    public class Data
    {
        public int TimeStarted;
        public int TimeEnded;
        public SimulationData[] simulationDatas;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }

        public Data()
        {
            TimeEnded = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            TimeStarted = TimeEnded - (int)Time.realtimeSinceStartup;
            simulationDatas = SimulationTracker.simulations.ToArray();
        }
    }

    public static List<SimulationData> simulations = new List<SimulationData>();
    public static List<SimulationData> sentSimulations = new List<SimulationData>();
    public static event Action<SimulationData> onNewSimulation;
    public static event Action<bool> onDataPost;

    public static void AddNewData()
    {
        if (simulations.Count > 0)
        {
            simulations[simulations.Count - 1].StopHitCounting();
        }
        SimulationData simulationData = new SimulationData();
        simulations.Add(simulationData);
        onNewSimulation?.Invoke(simulationData);
    }

    public static void PostData()
    {
        if (!simulations.Any())
        {
            Debug.Log("no new data");
            onDataPost?.Invoke(false);
            return;
        }
        DBConnector.Instance?.Post(new Data());
        SimulationData[] aux = new SimulationData[simulations.Count];
        simulations.CopyTo(aux, 0);
        simulations.Clear();
        sentSimulations.Concat(aux);
        onDataPost?.Invoke(true);
    }
}
