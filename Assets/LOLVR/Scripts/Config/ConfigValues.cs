using System;
using Edwon.VR;
using LOLVR.Enums;

namespace LOLVR.Config
{
    [Serializable]
    public struct ConfigValues
    {
        public VRKeyCodes leftClick;
        public VRKeyCodes rightClick;
        public Handedness mainHand;
        public string champion;
        public float monitorSize;
        public KeyboardKeyCodes potionKey;
    }
}
