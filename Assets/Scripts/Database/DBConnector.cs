using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Models;
using Proyecto26;
using UnityEditor;

namespace DB
{
    [Serializable]
    public class SimulationDataPost
    {
        public int id;

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
    }
}


public class DBConnector : MonoBehaviour
{
    [SerializeField]
    private string basePath;
    [SerializeField]
    private string environment;
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void Post()
    {

        // We can add default query string params for all requests
        RestClient.DefaultRequestParams["param1"] = "My first param";
        RestClient.DefaultRequestParams["param3"] = "My other param";

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/posts",
            Params = new Dictionary<string, string> {
                { "param1", "value 1" },
                { "param2", "value 2" }
            },
            Body = new Post
            {
                title = "foo",
                body = "bar",
                userId = 1
            },
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
}
