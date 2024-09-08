using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Models;
using Proyecto26;
using UnityEditor;
using DB;

namespace DB
{
    [Serializable]
    public class SimulationDataPost
    {
        public float rotationX;
        public float rotationY;
        public float FireForce;
        public float BreakForce;
        public float BreakTorque;
        public float ProjectileMass;
        public float TargetMass;
        public int JointsDestroyed;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }

        public SimulationDataPost(SimulationData data)
        {
            rotationX = data.RotationX;
            rotationY = data.RotationY;
            FireForce = data.FireForce;
            BreakForce = data.BreakForce;
            BreakTorque = data.BreakTorque;
            ProjectileMass = data.ProjectileMass;
            TargetMass = data.TargetMass;
            JointsDestroyed = data.JointsDestroyed;
        }
    }
}


public class DBConnector : MonoBehaviour
{
    [SerializeField]
    private string basePath;
    [SerializeField]
    private string environment;
    private RequestHelper currentRequest;

    public static DBConnector instance;

    private void Awake()
    {
        instance = this;
    }

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    [ContextMenu("A ver bruh")]
    public void Post()
    {

        SimulationData last = SimulationData.simulations[0];

        // We can add default query string params for all requests
        RestClient.DefaultRequestParams["param1"] = "My first param";
        RestClient.DefaultRequestParams["param3"] = "My other param";

        currentRequest = new RequestHelper
        {
            Uri = basePath + environment,
            Params = new Dictionary<string, string> {
                { "param1", "value 1" },
                { "param2", "value 2" }
            },
            Body = new SimulationDataPost(last),
            EnableDebug = true
        };
        RestClient.Post<Post>(currentRequest)
        .Then(res => {

            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();

            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => this.LogMessage("Error", err.Message));
    }

    [ContextMenu("Gente esto anda??")]
    public void Get()
    {
        RestClient.Get(basePath + "test.json").Then(res => {
            EditorUtility.DisplayDialog("Response", res.Text, "Ok");
        }).Catch(err => this.LogMessage("Error", err.Message));
    }
}
