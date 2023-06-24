using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleState : MonoBehaviour
{
    public int DamageValue = 5;
    public int AmmoCount;
    public bool isMagInsideGun;
    public bool isBoltReady;

    public void Fire(RaycastHit Hit)
    {
        if (isMagInsideGun && isBoltReady)
        {
            if (AmmoCount <= 0)
            {
                return;
            }
           
            Debug.Log("FIRING AT: "  + Hit.collider.gameObject.name);
            // FIRE!!!
            if (Hit.collider.gameObject.GetComponent<EnemyController>())
            {
                Hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(DamageValue);
            }
            
            AmmoCount--;
            isBoltReady = false;

            if (AmmoCount <= 0)
            {
                isBoltReady = false;
            }
        }
    }

    public void ReadyBolt()
    {
        isBoltReady = true;
    }

    public void ToggleMag()
    {
        isMagInsideGun = !isMagInsideGun;
    }

    public void LoadBullet()
    {
        if (Camera.main.GetComponent<PlayerCamera>().Ammo <= 0)
        {
            return;
        }

        if (AmmoCount >= 3)
        {
            return;
        }
        
        if (!isMagInsideGun)
        {
            AmmoCount++;
            Camera.main.GetComponent<PlayerCamera>().Ammo--;
        }
    }
}
