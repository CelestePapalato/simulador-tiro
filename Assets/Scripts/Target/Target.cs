using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class Target : MonoBehaviour
{
    public FixedJoint joint { get; private set; }

    void Start()
    {
        joint = GetComponent<FixedJoint>();
    }
}
