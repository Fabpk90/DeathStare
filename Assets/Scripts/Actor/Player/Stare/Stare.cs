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
   private HashSet<HealthManager> hittedDuringThisFrame;

   public float stareForce;

   private void Awake()
   {
      hittedDuringThisFrame = new HashSet<HealthManager>();
      hitsToRemove = new List<IHittable>(25);  
      targetToAttack = new List<IHittable>(25);
      
//      print(HittablePoints.Count);
   }

   /// <summary>
   /// Check if the actor is staring at something
   /// </summary>
   /// <returns> true if he/she is, false otherwise</returns>
   public bool CheckForThingsInSight()
   {
      hittedDuringThisFrame.Clear();

      bool found = false;
      
      foreach (HittablePoint point in HittablePoints)
      {
         if (point)
         {
            Vector3 viewportPoint = camera.WorldToViewportPoint(point.GetPosition());
            //print(viewportPoint);

            if (viewportPoint.z > 0 
                && viewportPoint.x > 0 && viewportPoint.x < 1
                && viewportPoint.y > 0 && viewportPoint.y < 1
                && !hittedDuringThisFrame.Contains(point.healthManager))
            {
               Vector3 repulsionForce = point.GetPosition() - transform.position;
               
               point.AddForce(repulsionForce.normalized * stareForce);
                  
               hittedDuringThisFrame.Add(point.healthManager);
               found = true;

               point.TakeDamage(damagePerSecond);
            }
         }
      }

      return found;
   }

   private void FixedUpdate()
   {
      if (isStaring)
      {
         CheckForThingsInSight();
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
