using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternState : MonoBehaviour
{
    public int DamageValue = 1;
    public int AmmoCount;
    public bool isMagInsideGun;

    public void Fire(RaycastHit Hit)
    {
        if (isMagInsideGun)
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
        }
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

        if (AmmoCount >= 6)
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
