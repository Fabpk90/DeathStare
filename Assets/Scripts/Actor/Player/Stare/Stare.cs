using System.Collections;
using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

public class Stare : MonoBehaviour
{
   public float damagePerSecond;


   public bool StareViolently(Transform[] points, Vector3 origin, Camera cam)
   {
      print("Shooting ! " + points.Length);
      bool found = false;
      foreach (Transform point in points)
      {
         Vector3 p = cam.WorldToScreenPoint(point.position);
         
         //test if the point is visible for the camera
         if (p.x >= 0 && p.x <= cam.pixelWidth
                      && p.y >= 0 && p.y <= cam.pixelHeight
                      && p.z >= 0)
         {
            print("in da frustrum");
           
            var direction = origin - point.position;
            Debug.DrawRay(point.position, direction, Color.red, 2.0f);
            if(Physics.Raycast(origin, direction, out var hitinfo))
            {
               var hitable = hitinfo.collider.GetComponent<IHittable>();

               if (hitable != null)
               {
                  hitable.TakeDamage((int)damagePerSecond);
                  found = true;
               }
               
            }
         }
         else
         {
            print("Not in the frustrum");
         }
      }
      return found;
   }
   
   public bool StareViolently(Vector3 origin, Vector3 direction)
   {
      Ray r = new Ray(origin, direction);

      if (Physics.Raycast(r, out var hitInfo))
      {
         var hitable = hitInfo.collider.GetComponent<IHittable>();

         hitable?.TakeDamage((int)damagePerSecond);

         print(hitInfo.collider.gameObject);

         return true;
      }
      
      Debug.DrawRay(origin, direction, Color.red, 5.0f);

      return false;
   }
}
