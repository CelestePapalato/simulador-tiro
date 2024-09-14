using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBError : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]
    TMP_Text errorText;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        if (!errorText)
        {
            errorText = GetComponentInChildren<TMP_Text>();
        }
        canvas.enabled = false;
    }

    private void OnEnable()
    {
        DBConnector.onFailure += UpdateLog;
    }

    private void OnDisable()
    {
        DBConnector.onFailure -= UpdateLog;
    }

    private void UpdateLog(System.Exception err)
    {
        errorText.text = err.Message;
        canvas.enabled = true;
    }
}
