using System;
using Edwon.VR;

namespace LOLVR
{
    [Serializable]
    public struct ConfigValues
    {
        public VRKeyCodes leftClick;
        public VRKeyCodes rightClick;
        public Handedness mainHand;
        public string champion;
        public float monitorSize;
    }
}