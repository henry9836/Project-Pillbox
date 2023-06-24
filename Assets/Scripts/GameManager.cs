using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
   public PlayerCamera Player;
   public GameObject LoseScreen;
   public GameObject WinScreen;
   public SunScript SunBehaviour;
   public List<GameObject> Worms;
   public List<Transform> Spawns;
   public float SunTime = 300.0f;

   public float TimeTillNextWorm = 15.0f;
   public float MulitplerAfterEachWorm = 0.8f;
   public float MinTimeBettweenWorms = 1.0f;
   private float SpawnCooldownTimer;
   
   
   public void Start()
   {
      WinScreen.SetActive(false);
      LoseScreen.SetActive(false);
      SunBehaviour.RotationSpeed = 1.0f / SunTime;
      SpawnCooldownTimer = TimeTillNextWorm;
   }

   public void Restart()
   {
      SceneManager.LoadScene("Pillbox", LoadSceneMode.Single);   
   }
   
   private void Update()
   {
      SpawnCooldownTimer += Time.deltaTime;
      
      if (SunBehaviour.CycleDone)
      {
         WinScreen.SetActive(true);
      }

      if (Player.Dead)
      {
         LoseScreen.SetActive(true);
      }
      else
      {
         if (SpawnCooldownTimer >= TimeTillNextWorm)
         {
            //Spawn wormies
            Instantiate(Worms[Random.Range(0, Worms.Count)], Spawns[Random.Range(0, Spawns.Count)]);

            TimeTillNextWorm *= MulitplerAfterEachWorm;

            Mathf.Clamp(TimeTillNextWorm, MinTimeBettweenWorms, Mathf.Infinity);
            
            SpawnCooldownTimer = 0.0f;
         }
      }
      
   }
}
