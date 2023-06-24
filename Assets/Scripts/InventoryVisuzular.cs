using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryTypes
{
    AmmoPounch,
    RifleRounds,
    PistolRounds
}

public class InventoryVisuzular : MonoBehaviour
{
    public GameObject ReferenceObject;
    public InventoryTypes Type;
    public List<Sprite> Sprites;
    public Image SpriteRenderer;
    
    void UpdateSprite(int value)
    {
        value = Mathf.Clamp(value, 0, Sprites.Count - 1);
        SpriteRenderer.sprite = Sprites[value];
    }
    
    private void FixedUpdate()
    {
        if (Type == InventoryTypes.AmmoPounch)
        {
            UpdateSprite(Camera.main.GetComponent<PlayerCamera>().Ammo);
        }
        else if (Type == InventoryTypes.RifleRounds)
        {
            UpdateSprite(ReferenceObject.GetComponent<RifleState>().AmmoCount);
        }
        else if (Type == InventoryTypes.PistolRounds)
        {
            UpdateSprite(ReferenceObject.GetComponent<WesternState>().AmmoCount);
        }
    }
}