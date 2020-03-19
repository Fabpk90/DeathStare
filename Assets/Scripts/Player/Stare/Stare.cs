using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public float damagePerSecond;

   public bool StareViolently(Vector3 position, Vector3 direction)
   {
      Ray r = new Ray(position, direction);

      if (Physics.Raycast(r, out var hitInfo))
      {
         print("hit !");
         return true;
      }
      
      Debug.DrawRay(position, direction, Color.red, 5.0f);

      return false;
   }
}
