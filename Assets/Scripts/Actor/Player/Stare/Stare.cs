using System;
using System.Collections.Generic;
using Actor;
using Actor.Hittable;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public static List<HittablePoint> HittablePoints = new List<HittablePoint>(200);
   
   public List<IHittable> targetToAttack;
   public int damagePerSecond;

   public bool isStaring;
   private List<IHittable> hitsToRemove;

   public Camera camera;
   private HashSet<HealthManager> hitDuringThisFrame;
   [NonSerialized] public List<PlayerController> playersHitDuringThisFrame;

   public float stareForce;

   private PlayerController _controller;
   
   private void Awake()
   {
      _controller = GetComponentInParent<PlayerController>();
      hitDuringThisFrame = new HashSet<HealthManager>();
      hitsToRemove = new List<IHittable>(25);  
      targetToAttack = new List<IHittable>(25);
      playersHitDuringThisFrame = new List<PlayerController>(3);
   }

   /// <summary>
   /// Check if the actor is staring at something
   /// </summary>
   /// <returns> true if he/she is, false otherwise</returns>
   public bool CheckForThingsInSight()
   {
      hitDuringThisFrame.Clear();
      playersHitDuringThisFrame.Clear();

      bool found = false;
      
      foreach (HittablePoint point in HittablePoints)
      {
         if (point)
         {
            Vector3 viewportPoint = camera.WorldToViewportPoint(point.GetPosition());

            if (viewportPoint.z > 0 
                && viewportPoint.x > 0 && viewportPoint.x < 1
                && viewportPoint.y > 0 && viewportPoint.y < 1
                && !hitDuringThisFrame.Contains(point.healthManager))
            {
               Vector3 repulsionForce = point.GetPosition() - transform.position;
               
               point.AddForce(repulsionForce.normalized * stareForce);
                  
               hitDuringThisFrame.Add(point.healthManager);

               PlayerController p = point.healthManager.GetComponent<PlayerController>();

               //we hit a player !
               if (p)
               {
                  playersHitDuringThisFrame.Add(p);
               }
               else
               {
                  point.TakeDamage(damagePerSecond * Time.deltaTime);
               }
               
               found = true;

               //TODO: check if it's not a player that is staring at us
               //maybe have a list of player we are staring at
               //to rapidly check if it's a duel
               
               
            }
         }
      }

      return found;
   }

   private void Update()
   {
      if (isStaring)
      {
         CheckForThingsInSight();
      }
   }

   private void LateUpdate()
   {
      //TODO: check if the players we are staring at are staring back
      foreach (PlayerController playerController in playersHitDuringThisFrame)
      {
         if (playerController)
         {
            if (!playerController._stare.playersHitDuringThisFrame.Contains(_controller))
            {
               //WE DESTROY DAT MF !
               playerController.GetComponent<HealthManager>().TakeDamage(damagePerSecond * Time.deltaTime);
            }
            else
            {
               print("Duel with " +  playerController.GetComponent<HealthManager>().transform.gameObject);
            }
         }
         
      }
   }


   public bool StartStare()
   {
      isStaring = true;
      return CheckForThingsInSight();
   }

   public void StopStare()
   {
      isStaring = false;
   }
}
