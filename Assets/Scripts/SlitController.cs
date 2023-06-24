using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlitController : MonoBehaviour
{
    private bool IsShut;

    private void Start()
    {
        IsShut = true;
    }

    public void Interact()
    {
        IsShut = !IsShut;
        
        //GetComponent<MeshCollider>().enabled = IsOpen;
        GetComponent<MeshRenderer>().enabled = IsShut;
        transform.parent.GetComponent<MeshRenderer>().enabled = IsShut;
    }
}
