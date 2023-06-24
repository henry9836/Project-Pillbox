using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillboxController : MonoBehaviour
{
    public GameObject DamageOne;
    public GameObject DamageTwo;
    public float MaxHealth = 100.0f;
    public float Health = 100.0f;

    private void Start()
    {
        Health = MaxHealth;
        VisualEffects();
    }

    public void Heal()
    {
        Boom();
        Health += (MaxHealth * 0.25f);
        Health = Mathf.Clamp(Health, 0.0f, MaxHealth);
        VisualEffects();
    }

    public void Boom()
    {
        Debug.Log("Boom");
        Vector3 explosionPos = transform.position + (-transform.up * 2.0f); 
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 50.0f);
        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.name);
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(50000.0f, explosionPos, 500.0f, 50.0F);
        }
    }
    
    void VisualEffects()
    {
        if (Health <= (MaxHealth * 0.75f))
        {
            DamageOne.SetActive(true);
        }

        if (Health > (MaxHealth * 0.95f))
        {
            DamageOne.SetActive(false);
        }
        
        DamageTwo.SetActive((Health <= (MaxHealth * 0.5f)));

        if (Health <= 0.0f)
        {
            //Death
            Camera.main.GetComponent<PlayerCamera>().Dead = true;
        }
    }
    
    public void TakeDamage(float Amount)
    {
        Health -= Amount;
        VisualEffects();
    }
}
