using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace LOLVR
{
    public class XRTransformStabilizerSafe: XRTransformStabilizer
    {
        private Transform thisTransform;

        private new void OnEnable()
        {
            base.OnEnable();
            thisTransform = transform;
        }

        protected new void Update()
        {
            Vector3 savedPosition = thisTransform.position;
            Quaternion savedRotation = thisTransform.rotation;

            base.Update();

            if (thisTransform.position == Vector3.zero)
            {
                transform.SetPositionAndRotation(savedPosition, savedRotation);
            }
        }
    }
}
