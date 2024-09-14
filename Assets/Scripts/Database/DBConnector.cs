using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Models;
using Proyecto26;
using UnityEditor;
using static System.Net.WebRequestMethods;

public class DBConnector : MonoBehaviour
{
    private readonly string basePath = "https://simulador-tiro-default-rtdb.firebaseio.com/";
    private readonly string environment = "test";
    private RequestHelper currentRequest;

    public static DBConnector Instance;

    public static event Action<System.Exception> onFailure;
    public static event Action onPut;
    public static event Action<SimulationTracker.Data> onGet;

    private void Awake()
    {
        Instance = this;
    }

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void Put(object obj)
    {
        currentRequest = new RequestHelper
        {
            Uri = basePath + environment + "/Data.json",
            Body = obj,
            EnableDebug = true
        };
        RestClient.Put<Post>(currentRequest)
        .Then(res => {

            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();

            this.LogMessage("Success", JsonUtility.ToJson(res, true));
            onPut?.Invoke();
        })
        .Catch(err =>
        {
            this.LogMessage("Error", err.Message);
            onFailure?.Invoke(err);
        });
    }

    [ContextMenu("Gente esto anda?? PARTE 1")]
    public void Get()
    {
        ResponseHelper response = null;
        Exception exception = null;

        RestClient.Get(basePath + environment + "/Data.json").Then(res => {
            //EditorUtility.DisplayDialog("Response", res.Text, "Ok");
            response = res;
        }).Catch(err =>
        {
            this.LogMessage("Error", err.Message);
            exception = err;
        });
    }

    [ContextMenu("Gente esto anda??")]
    public void GetTrackerData()
    {
        RestClient.Get(basePath + environment + "/Data.json", GetHelper);
    }

    private void GetHelper(RequestException exception, ResponseHelper response)
    {
        Debug.Log(response.StatusCode + " data: " + response.Text);
        SimulationTracker.Data data = JsonUtility.FromJson<SimulationTracker.Data>(response.Text);
        onGet?.Invoke(data);
    }
}
