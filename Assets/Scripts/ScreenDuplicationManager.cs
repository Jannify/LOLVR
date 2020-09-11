using UnityEngine;

namespace LOLVR
{
    public class ScreenDuplicationManager : MonoBehaviour
    {
        [SerializeField] private VdmDesktopManager desktopManager;

        private void Start()
        {
            desktopManager.ScreenScaleFactor = ConfigManager.MonitorSize / 1000f;
        }
    }
}