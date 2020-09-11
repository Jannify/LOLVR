using Edwon.VR;
using Edwon.VR.Gesture;
using UnityEngine;

namespace LOLVR
{
    public class HandGestureRecognizer : MonoBehaviour
    {
        private static HandGestureRecognizer instance;
        [SerializeField] private VRGestureRig gestureRig;

        private void Awake() => instance = this;
        private void Start()
        {
            gestureRig.mainHand = ConfigManager.MainHand;
            gestureRig.BeginDetect(ConfigManager.Champion);
        }

        private void OnEnable() => GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        private void OnDisable() => GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;

        public static void ReloadNeuralNet() => instance.gestureRig.SwitchNeuralNet(ConfigManager.Champion);

        private static void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
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