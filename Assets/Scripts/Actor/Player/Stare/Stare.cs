using System;
using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public List<IHittable> targetToAttack;
   public int damagePerSecond;

   public bool isStaring;

   private void Awake()
   {
      targetToAttack = new List<IHittable>(25);
   }

   public bool CheckForThingsInSight()
   {
      if (targetToAttack.Count > 0)
      {
         print(targetToAttack.Count);
         foreach (IHittable hittable in targetToAttack)
         {
            hittable.TakeDamage(1);
         }
         return true;
      }
      else
      {
         return false;
      }
   }

   private void FixedUpdate()
   {
      if (isStaring)
      {
         CheckForThingsInSight();
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      IHittable hit = other.gameObject.GetComponent<IHittable>();

      if (hit != null)
      {
         if (Physics.Raycast(transform.position, other.bounds.center - transform.position, out var hitInfo))
         {
            IHittable hit0 = hitInfo.transform.GetComponent<IHittable>();

            if (hit0 == hit)
            {
               targetToAttack.Add(hit0);
            }
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      IHittable hit = other.gameObject.GetComponent<IHittable>();

      if (hit != null)
      {
         targetToAttack.Remove(hit);
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
