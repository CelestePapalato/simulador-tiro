using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Models;
using Proyecto26;
using UnityEditor;

public class DBConnector : MonoBehaviour
{
    [SerializeField]
    private string basePath;
    [SerializeField]
    private string environment;
    private RequestHelper currentRequest;

    public static DBConnector Instance;

    public static event Action<System.Exception> onError;

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

    [ContextMenu("A ver bruh")]
    public void Post(object obj)
    {
        currentRequest = new RequestHelper
        {
            Uri = basePath + environment,
            Body = obj,
            EnableDebug = true
        };
        RestClient.Post<Post>(currentRequest)
        .Then(res => {

            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();

            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => { this.LogMessage("Error", err.Message); onError?.Invoke(err); });
    }

    [ContextMenu("Gente esto anda??")]
    public void Get()
    {   /*
        RestClient.Get(basePath + "test.json").Then(res => {
            EditorUtility.DisplayDialog("Response", res.Text, "Ok");
        }).Catch(err => this.LogMessage("Error", err.Message));
        */
    }
}
