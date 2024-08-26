using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstancer : MonoBehaviour
{
    public GameObject prefab;

    private GameObject currentInstance;

    void Start()
    {
        InstantiatePrefab();
    }

    public void InstantiatePrefab()
    {
        if (currentInstance)
        {
            Destroy(currentInstance);
        }
        currentInstance = Instantiate(prefab, transform);
    }
}