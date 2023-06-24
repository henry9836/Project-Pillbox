using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    public float RotationSpeed = 20.0f;
    public Vector3 StartVector;
    public Vector3 EndVector;
    public bool CycleDone;
    private float timer;
    private Quaternion StartQuat;
    private Quaternion EndQuat;

    private void Start()
    {
        CycleDone = false;
        timer = 0.0f;
        StartQuat = Quaternion.Euler(StartVector);
        EndQuat = Quaternion.Euler(EndVector);
        transform.rotation = Quaternion.Euler(StartVector);
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(StartQuat, EndQuat, timer);
        timer += Time.deltaTime * RotationSpeed;
        
        //Check if angle is same if so we have stopped
        if (timer >= 1.0f)
        {
            CycleDone = true;
        }
    }
}
