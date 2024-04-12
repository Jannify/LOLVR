using System;
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
            if (Math.Abs(ConfigManager.MonitorSize - desktopManager.ScreenScaleFactor * 1000f) > 0.01f)
            {
                desktopManager.ScreenScaleFactor = ConfigManager.MonitorSize / 1000f;
            }

        }
    }
}
