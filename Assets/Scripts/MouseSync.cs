using UnityEngine;

namespace LOLVR
{
    public class MouseSync : MonoBehaviour
    {
        [SerializeField] private VRKeyCodes leftClick;
        [SerializeField] private VRKeyCodes rightClick;

        private void Update()
        {
            if (Input.GetKeyDown((KeyCode)leftClick)) SendAction.SimulateMouseLeftDown();
            if (Input.GetKeyDown((KeyCode)rightClick)) SendAction.SimulateMouseRightDown();
            if (Input.GetKeyUp((KeyCode)leftClick)) SendAction.SimulateMouseLeftUp();
            if (Input.GetKeyDown((KeyCode)rightClick)) SendAction.SimulateMouseRightUp();
        }
    }
}