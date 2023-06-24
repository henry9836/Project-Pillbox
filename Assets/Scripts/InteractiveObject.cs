using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum InteractiveObjectsType
{
    None,
    AmmoCrate,
    Rifle,
    WesternPewPew,
    Hammer,
    Drink
}

public class InteractiveObject : MonoBehaviour
{
    public GameObject UIInterface;
    public InteractiveObjectsType Type;

    private void Start()
    {
        Quaternion NewRot = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        transform.rotation = NewRot;
    }

    public virtual void OnStartInteract()
    {
        if (UIInterface)
        {
            UIInterface.SetActive(true);
        }

        // Move out of site
        transform.position = -transform.up * 30.0f;
    }

    public virtual void OnEndInteract()
    {
        if (UIInterface)
        {
            UIInterface.SetActive(false);
        }
        
        // Move back
        Transform CameraTransform = Camera.main.transform;
        transform.position = CameraTransform.position + (-CameraTransform.up * 3.0f);
    }
}
