using System;
using System.Collections.Generic;
using Actor;
using Actor.Hittable;
using UnityEngine;

public class StareHandler : MonoBehaviour
{
   public static List<HittablePoint> HittablePoints = new List<HittablePoint>(200);
   
   public List<IHittable> targetToAttack;
   public int damagePerSecond;

   public bool isStaring;
   private List<IHittable> _hitsToRemove;

   public Camera camera;
   private HashSet<HealthManager> _hitDuringThisFrame;
   public List<PlayerController> playersHitDuringThisFrame;
   private List<PlayerController> _playerKilledDuringFrame;

   public float stareForce;

   private PlayerController _controller;

   public event EventHandler OnStareStart;
   public event EventHandler<int> OnStareTouch;
   public event EventHandler OnStareStop;
   
   private void Awake()
   {
      _controller = GetComponentInParent<PlayerController>();
      _hitDuringThisFrame = new HashSet<HealthManager>();
      _hitsToRemove = new List<IHittable>(25);  
      targetToAttack = new List<IHittable>(25);
      playersHitDuringThisFrame = new List<PlayerController>(3);
      _playerKilledDuringFrame = new List<PlayerController>(3);
   }

   /// <summary>
   /// Check if the actor is staring at something
   /// </summary>
   /// <returns> true if he/she is, false otherwise</returns>
   public bool CheckForThingsInSight()
   {
      _hitDuringThisFrame.Clear();
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
                && !_hitDuringThisFrame.Contains(point.healthManager))
            {
               RaycastHit hitInfo;
               if(Physics.Raycast(transform.position, (point.GetPosition() - transform.position).normalized, out hitInfo))
               {
                  IHittable hit = hitInfo.transform.GetComponent<IHittable>();

                  if (hit != null)
                  {
                     Vector3 repulsionForce = hit.GetPosition() - transform.position;
            
                     point.AddForce(repulsionForce.normalized * stareForce);
               
                     _hitDuringThisFrame.Add(point.healthManager);
                     found = true;
                     
                     
                     PlayerController p = point.healthManager.GetComponent<PlayerController>();

                     if (p)
                     {
                        //we hit a player !
                        if (p != _controller)
                        {
                           playersHitDuringThisFrame.Add(p);
                        }
                     }
                     else
                     {
                        hit.TakeDamage(_controller.GetPlayerIndex(), damagePerSecond * Time.deltaTime);
                     }
                  }
               }
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
      if(!isStaring) return;
      
      foreach (PlayerController playerController in playersHitDuringThisFrame)
      {
         if (playerController)
         {
            //is he/she staring at us ?
            if (!playerController.stareHandler.playersHitDuringThisFrame.Contains(_controller))
            {
                    //Sound
                    switch (playerController.GetPlayerIndex())
                    {
                        case (0):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Stan", "False");
                            break;
                        case (1):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Marta", "False");
                            break;
                        case (2):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Medusa", "False");
                            break;
                        case (3):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Don", "False");
                            break;
                    }
                    //Sound
                    //WE DESTROY DAT MF !
                    if (playerController.GetComponent<HealthManager>().TakeDamage(_controller.GetPlayerIndex(), damagePerSecond * Time.deltaTime))
               {
                  //the mf is ded
                  _playerKilledDuringFrame.Add(playerController);
               }

            }
            else // it's staring back at us !
            {
                    print("Duel with " + playerController.GetComponent<HealthManager>().transform.gameObject);
                    //Sound
                    switch (playerController.GetPlayerIndex())
                    {
                        case (0):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Stan", "True");
                            break;
                        case (1):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Marta", "True");
                            break;
                        case (2):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Medusa", "True");
                            break;
                        case (3):
                            AkSoundEngine.SetState("STATE_Music_DuelState_Don", "True");
                            break;
                    }
                    //Sound
                }
            }
      }

      foreach (PlayerController controller in _playerKilledDuringFrame)
      {
         playersHitDuringThisFrame.Remove(controller);
      }
      
      _playerKilledDuringFrame.Clear();
   }


   public bool StartStare()
   {
      isStaring = true;

      OnStareStart?.Invoke(this, null);
      
      return CheckForThingsInSight();
   }

   public void StopStare()
   {
      isStaring = false;
      OnStareStop?.Invoke(this, null);
   }
}
