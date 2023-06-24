using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool Explosive;
    public int Health = 1;
    public float Damage = 2.5f;
    public float attackThreshold = 1.5f;
    private float attackTimer = 0.0f;
    private PillboxController PillboxController;
    private void Start()
    {
        // Attack the player
        PillboxController = GameObject.FindWithTag("Pillbox").GetComponent<PillboxController>();
        GetComponent<NavMeshAgent>().SetDestination(new Vector3(0.0f, 0.0f, 0.0f));
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<NavMeshAgent>().velocity == Vector3.zero)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackThreshold)
            {
                DoDamage();
                attackTimer = 0.0f;
            }
        }
    }


    void DoDamage()
    {
        PillboxController.TakeDamage(Damage);
        
        if (Explosive)
        {
            PillboxController.Boom();
            Die();
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject);
    }
}
