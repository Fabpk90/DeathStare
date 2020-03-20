using System.Collections;
using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public float damagePerSecond;

   public bool StareViolently(Vector3 position, Vector3 direction)
   {
      Ray r = new Ray(position, direction);

      if (Physics.Raycast(r, out var hitInfo))
      {
         var hitable = hitInfo.collider.GetComponent<IHittable>();

         hitable.TakeDamage((int)damagePerSecond);

         return true;
      }
      
      Debug.DrawRay(position, direction, Color.red, 5.0f);

      return false;
   }
}
