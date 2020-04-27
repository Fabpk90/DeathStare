using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Player.Stare
{
    public class StareVignetteManager : MonoBehaviour
    {
        public float vignettingStartTime;
        public AnimationCurve curve;
        public float vignettingStopTime;

        public StareHandler stare;

        private float alphaLerp;

        public Image imageTop;
        public Image imageBottom;

        private float deltaNeg;
        private bool isStaring;
        
        private void OnEnable()
        {
            stare.OnStareStart += OnStareStart;
            stare.OnStareStop += OnStareStop;
        }

        private void Start()
        {
            alphaLerp = 0.0f;
        }

        private void Update()
        {
            if (isStaring)
            {
                alphaLerp += Time.deltaTime / vignettingStartTime;
                alphaLerp = Mathf.Clamp01(alphaLerp);

                var c = imageTop.color;
                
                c.a = curve.Evaluate(alphaLerp);
                imageTop.color = imageBottom.color = c;
            }
            else
            {
                alphaLerp -= Time.deltaTime / vignettingStopTime;
                alphaLerp = Mathf.Clamp01(alphaLerp);
                
                var c = imageTop.color;

                c.a = curve.Evaluate(alphaLerp);

                imageTop.color = imageBottom.color = c;
            }
        }

        public Vector2 GetViewHeight()
        {
            //this is dynamic because may be we could animate the stare
            //if not, pre compute this and send it
            if (isStaring) //TODO: change this to the alpha maybe, with a threshold
            {
                Vector2 v = Vector2.zero;
                var pixelWidth = stare.camera.pixelWidth;
                v.x = imageBottom.rectTransform.rect.height / pixelWidth;//the bottom limit
                v.y = 1 - (imageTop.rectTransform.rect.height / pixelWidth);//the top limit

                return v;
            }
            return Vector2.up;
        }

        private void OnStareStop(object sender, EventArgs e)
        {
            isStaring = false;
        }

        private void OnStareStart(object sender, EventArgs e)
        {
            isStaring = true;
        }
        
        private void OnDisable()
        {
            stare.OnStareStart -= OnStareStart;
            stare.OnStareStop -= OnStareStop;
        }
    }
}