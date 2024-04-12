using LOLVR.Config;
using UnityEngine;

namespace LOLVR.Input
{
    public class MouseButtonSync : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown((KeyCode)ConfigManager.LeftClick)) SendAction.SimulateMouseLeftDown();
            else if (UnityEngine.Input.GetKeyDown((KeyCode)ConfigManager.RightClick)) SendAction.SimulateMouseRightDown();
            if (UnityEngine.Input.GetKeyUp((KeyCode)ConfigManager.LeftClick)) SendAction.SimulateMouseLeftUp();
            else if (UnityEngine.Input.GetKeyUp((KeyCode)ConfigManager.RightClick)) SendAction.SimulateMouseRightUp();
        }
    }
}
