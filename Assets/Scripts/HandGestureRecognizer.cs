using Edwon.VR;
using Edwon.VR.Gesture;
using LOLVR.InputStructs;
using UnityEngine;

namespace LOLVR
{
    public class HandGestureRecognizer : MonoBehaviour
    {
        [SerializeField] private VRGestureRig gestureRig;
        [SerializeField] private Champions selectedChampion;

        private void Start()
        {
            gestureRig.BeginDetect(selectedChampion.ToString());
        }

        private void OnEnable()
        {
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        }

        private void OnDisable()
        {
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        }

        private static void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
            Debug.Log(gestureName);
            switch (gestureName)
            {
                case "Q":
                    SendAction.SimulateKey(16);
                    break;
                case "W":
                    SendAction.SimulateKey(17);
                    break;
                case "E":
                    SendAction.SimulateKey(18);
                    break;
                case "R":
                    SendAction.SimulateKey(19);
                    break;
            }
        }
    }
}