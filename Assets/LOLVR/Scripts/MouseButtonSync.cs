using UnityEngine;

namespace LOLVR
{
    public class MouseButtonSync : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown((KeyCode)ConfigManager.LeftClick)) SendAction.SimulateMouseLeftDown();
            else if (Input.GetKeyDown((KeyCode)ConfigManager.RightClick)) SendAction.SimulateMouseRightDown();
            if (Input.GetKeyUp((KeyCode)ConfigManager.LeftClick)) SendAction.SimulateMouseLeftUp();
            else if (Input.GetKeyUp((KeyCode)ConfigManager.RightClick)) SendAction.SimulateMouseRightUp();
        }
    }
}
