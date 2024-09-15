using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationTracker : MonoBehaviour
{
    public static SimulationTracker Instance;

    public static event Action<SimulationData[]> OnDataGet;
    public static event Action<SimulationData> OnNewSimulation;
    public static event Action<bool> OnDataPut;
    public static event Action OnDataPutSuccess;
    public static event Action OnDataPutFailure;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
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

    private void Start()
    {
        DBConnector.Instance.GetTrackerData();
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

    private List<SimulationData> newSimulations = new List<SimulationData>();
    private List<SimulationData> simulationsHistory = new List<SimulationData>();
    public List<SimulationData> Simulations { get { return simulationsHistory.Concat(newSimulations).ToList(); } }

    private void AddSimulations(Data simulationTracker)
    {
        DBConnector.onGet -= AddSimulations;
        simulationsHistory.AddRange(simulationTracker.simulations);
        foreach(var simulation in simulationsHistory)
        {
            simulation.isPosted = true;
            simulation.StopHitCounting();
        }
        OnDataGet?.Invoke(simulationsHistory.ToArray());
    }

    public void AddNewData()
    {
        if (newSimulations.Count > 0)
        {
            newSimulations[newSimulations.Count - 1].StopHitCounting();
        }
        SimulationData simulationData = new SimulationData();
        newSimulations.Add(simulationData);
        OnNewSimulation?.Invoke(simulationData);
    }

    public void PutData()
    {
        if (!newSimulations.Any())
        {
            Debug.Log("no new data");
            OnDataPut?.Invoke(false);
            return;
        }
        if (!DBConnector.Instance)
        {
            Debug.Log("No Database Connector instance exists");
            OnDataPut?.Invoke(false);
            OnDataPutFailure?.Invoke();
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
        simulationsHistory.AddRange(aux);
        OnDataPut?.Invoke(true);
        OnDataPutSuccess?.Invoke();
    }
}
