using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HandCursor : MonoBehaviour
{
    public Sprite OpenHand;
    public Sprite CloseHand;
    private SpriteRenderer HandRenderer;

    private void Start()
    {
        HandRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Debug.Log(Input.mousePosition);
        GetComponent<RectTransform>().localPosition = Input.mousePosition;
        HandRenderer.sprite = Input.GetButton("Interact") ? CloseHand : OpenHand;
    }
}
