using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WesternUI : MonoBehaviour
{
    public WesternState WesternState;
    public Image MagRender;
    public Sprite MagIsLoaded;
    public Sprite MagIsNotLoaded;
    
    
    private void FixedUpdate()
    {
        MagRender.sprite = WesternState.isMagInsideGun ? MagIsLoaded : MagIsNotLoaded;
    }
}
