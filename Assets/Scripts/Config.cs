using System;
using Edwon.VR;

namespace LOLVR
{
    [Serializable]
    public struct Config
    {
        public VRKeyCodes leftClick;
        public VRKeyCodes rightClick;
        public Handedness mainHand;
        public string champion;
        public float monitorSize;
    }
}