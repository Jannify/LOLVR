using Edwon.VR;
using Edwon.VR.Gesture;
using LOLVR.Config;
using LOLVR.Enums;
using UnityEngine;

namespace LOLVR.Input
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

        private void OnEnable()
        {
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
            ConfigManager.OnConfigChanged += OnConfigChanged;
        }

        private void OnDisable()
        {
            GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;
            ConfigManager.OnConfigChanged -= OnConfigChanged;
        }

        private static void OnConfigChanged()
        {
            if (instance.gestureRig.GetNeuralNetName() != ConfigManager.Champion)
            {
                instance.gestureRig.SwitchNeuralNet(ConfigManager.Champion);
            }
        }

        private static void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
            switch (gestureName)
            {
                case "Q":
                    Debug.Log("Q");
                    SendAction.SimulateKey(KeyboardKeyCodes.Q);
                    break;
                case "W":
                    Debug.Log("W");
                    SendAction.SimulateKey(KeyboardKeyCodes.W);
                    break;
                case "E":
                    Debug.Log("E");
                    SendAction.SimulateKey(KeyboardKeyCodes.E);
                    break;
                case "R":
                    Debug.Log("R");
                    SendAction.SimulateKey(KeyboardKeyCodes.R);
                    break;
            }
        }
    }
}
