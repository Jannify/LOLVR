using LOLVR.Config;
using UnityEngine;

namespace LOLVR.Input
{
    public class ScreenDuplicationManager : MonoBehaviour
    {
        [SerializeField] private VdmDesktopManager desktopManager;

        private void Start()
        {
            OnConfigChanged();
        }

        private void OnEnable()
        {
            ConfigManager.OnConfigChanged += OnConfigChanged;
        }

        private void OnDisable()
        {
            ConfigManager.OnConfigChanged -= OnConfigChanged;
        }

        private void OnConfigChanged()
        {
            desktopManager.ScreenScaleFactor = ConfigManager.MonitorSize / 1000f;
        }
    }
}
