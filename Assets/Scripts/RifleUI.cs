using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleUI : MonoBehaviour
{
    public RifleState RifleState;
    public Image MagRender;
    public Image BoltRender;
    public Sprite MagIsLoaded;
    public Sprite MagIsNotLoaded;
    public Sprite BoltIsNotReady;
    public Sprite BoltIsReady;
    
    
    private void FixedUpdate()
    {
        MagRender.sprite = RifleState.isMagInsideGun ? MagIsLoaded : MagIsNotLoaded;
        BoltRender.sprite = RifleState.isBoltReady ? BoltIsReady : BoltIsNotReady;
    }
}
