using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationTracker : MonoBehaviour
{
    public static SimulationTracker Instance;

    public static event Action<Data[]> onDataGet;
    public static event Action<SimulationData> onNewSimulation;
    public static event Action<bool> onDataPut;
    public static event Action onDataPutSuccess;
    public static event Action onDataPutFailure;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DBConnector.Instance.GetTrackerData();
    }

    private void OnEnable()
    {
        DBConnector.onPut += CleanDataAfterPut;
        DBConnector.onGet += AddSimulations;
    }

    private void OnDisable()
    {
        DBConnector.onPut -= CleanDataAfterPut;
        DBConnector.onGet -= AddSimulations;
    }

    [Serializable]
    public class Data
    {
        public int TimeStarted;
        public int TimeEnded;
        public List<SimulationData> simulations;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }

        public Data()
        {
            TimeEnded = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            TimeStarted = TimeEnded - (int)Time.realtimeSinceStartup;
            simulations = SimulationTracker.Instance.Simulations;
        }
    }

    public List<SimulationData> newSimulations = new List<SimulationData>();
    public List<SimulationData> Simulations = new List<SimulationData>();

    public void AddSimulations(Data simulationTracker)
    {
        DBConnector.onGet -= AddSimulations;
        Simulations.AddRange(simulationTracker.simulations);
    }

    public void AddNewData()
    {
        if (newSimulations.Count > 0)
        {
            newSimulations[newSimulations.Count - 1].StopHitCounting();
        }
        SimulationData simulationData = new SimulationData();
        newSimulations.Add(simulationData);
        onNewSimulation?.Invoke(simulationData);
    }

    public void PutData()
    {
        if (!newSimulations.Any())
        {
            Debug.Log("no new data");
            onDataPut?.Invoke(false);
            return;
        }
        if (!DBConnector.Instance)
        {
            Debug.Log("No Database Connector instance exists");
            onDataPut?.Invoke(false);
            onDataPutFailure?.Invoke();
            return;
        }

        bool success = true;
        DBConnector.Instance.Put(new Data());
        Debug.Log("arrrr " + success);
    }
    private void CleanDataAfterPut()
    {
        SimulationData[] aux = new SimulationData[newSimulations.Count];
        foreach (var simulationData in newSimulations)
        {
            simulationData.isPosted = true;
        }
        newSimulations.CopyTo(aux, 0);
        newSimulations.Clear();
        Simulations.Concat(aux);
        onDataPut?.Invoke(true);
        onDataPutSuccess?.Invoke();
    }
}
