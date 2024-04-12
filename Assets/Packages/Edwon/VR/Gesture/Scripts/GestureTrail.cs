using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Edwon.VR.Gesture
{

    public class GestureTrail : MonoBehaviour
    {
        CaptureHand registeredHand;
        int lengthOfLineRenderer = 50;
        List<Vector3> displayLine;
        LineRenderer currentRenderer;

        public bool listening = false;

        bool currentlyInUse = false;

        // Use this for initialization
        void Start()
        {
            currentlyInUse = true;
            displayLine = new List<Vector3>();
            currentRenderer = CreateLineRenderer(Color.magenta, Color.magenta);
        }

        void OnEnable()
        {
            if(registeredHand != null)
            {
                SubscribeToEvents();
            }
        }

        void SubscribeToEvents()
        {
            registeredHand.StartCaptureEvent += StartTrail;
            registeredHand.ContinueCaptureEvent += CapturePoint;
            registeredHand.StopCaptureEvent += StopTrail;
        }

        void OnDisable()
        {
            if (registeredHand != null)
            {
                UnsubscribeFromEvents();
            }
        }

        void UnsubscribeFromEvents()
        {
            registeredHand.StartCaptureEvent -= StartTrail;
            registeredHand.ContinueCaptureEvent -= CapturePoint;
            registeredHand.StopCaptureEvent -= StopTrail;
        }

        void UnsubscribeAll()
        {

        }

        void OnDestroy()
        {
            currentlyInUse = false;
        }

        LineRenderer CreateLineRenderer(Color c1, Color c2)
        {
            GameObject myGo = new GameObject("Trail Renderer");
            myGo.transform.parent = transform;
            myGo.transform.localPosition = Vector3.zero;

            LineRenderer lineRenderer = myGo.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.startColor = c1;
            lineRenderer.endColor = c2;
            lineRenderer.startWidth = 0.01F;
            lineRenderer.endWidth = 0.05F;
            lineRenderer.positionCount = 0;
            lineRenderer.useWorldSpace = false;
            return lineRenderer;
        }

        public void StartTrail()
        {
            currentRenderer.startColor = Color.magenta;
            currentRenderer.endColor = Color.magenta;
            displayLine.Clear();
            listening = true;
        }

        public void CapturePoint(Vector3 handPoint)
        {
            //display line appears to be made up of World Points instead of localized ones.
            displayLine.Add(handPoint);
            currentRenderer.positionCount = displayLine.Count;
            currentRenderer.SetPositions(displayLine.ToArray());
        }

        public void StopTrail()
        {
            currentRenderer.startColor =Color.blue;
            currentRenderer.endColor = Color.cyan;
            listening = false;
        }

        public void ClearTrail()
        {
            currentRenderer.positionCount = 0;
        }

        public bool UseCheck()
        {
            return currentlyInUse;
        }

        public void AssignHand(CaptureHand captureHand)
        {
            currentlyInUse = true;
            registeredHand = captureHand;
            SubscribeToEvents();

        }

    }
}
