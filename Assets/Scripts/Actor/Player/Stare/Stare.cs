using System;
using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public List<IHittable> targetToAttack;
   public int damagePerSecond;

   public bool isStaring;
   private List<IHittable> hitsToRemove;

   private void Awake()
   {
      hitsToRemove = new List<IHittable>(25);  
      targetToAttack = new List<IHittable>(25);
   }

   public bool CheckForThingsInSight()
   {
      //TODO: make this thing per second
      print(targetToAttack.Count);
      if (targetToAttack.Count > 0)
      {
         bool found = false;
         
         foreach (IHittable hittable in targetToAttack)
         {
            Debug.DrawRay(transform.position, hittable.GetPosition() - transform.position, Color.red, 2.0f);
            var position = transform.position;
            
            //we check if we still see the hit, also checking different prospective to make sure
            if (Physics.Raycast(position, hittable.GetPosition() - transform.position)
            || Physics.Raycast(position - new Vector3(0 , -0.25f, 0.0f), hittable.GetPosition() - position)
            || Physics.Raycast(position - new Vector3(-0.25f , 0, 0.0f), hittable.GetPosition() - position)
            || Physics.Raycast(position - new Vector3(0.25f , 0, 0.0f), hittable.GetPosition() - position))
            {
               //TODO: check if the player has been already hit
               hittable.TakeDamage(damagePerSecond);
               found = true;
            }
            else
            {
               //TODO: what ?
               hitsToRemove.Add(hittable);
               print("Remove hitcast + " + hittable);
            }
         }

         foreach (IHittable hittable in hitsToRemove)
         {
            targetToAttack.Remove(hittable);
         }
         
         hitsToRemove.Clear();
         return found;
      }
      
      return false;
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
         //if we can really see the hit object
         if (Physics.Raycast(transform.position, hit.GetPosition() - transform.position, out var hitInfo))
         {
            IHittable hit0 = hitInfo.transform.GetComponent<IHittable>();

            if (hit0 == hit)
            {
               print("Adding " + hit0);
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
         print("Exiting " + hit);
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
