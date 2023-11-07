using System.IO;
using Edwon.VR;
using LOLVR.Enums;
using UnityEngine;

namespace LOLVR.Config
{
    public class ConfigManager : MonoBehaviour
    {
        private static ConfigValues config;
        private static string filePath;

        public delegate void ConfigChangedHandler();
        public static event ConfigChangedHandler OnConfigChanged;

        public static VRKeyCodes LeftClick
        {
            set => config.leftClick = value;
            get => config.leftClick;
        }
        public static VRKeyCodes RightClick
        {
            set => config.rightClick = value;
            get => config.rightClick;
        }
        public static Handedness MainHand
        {
            set => config.mainHand = value;
            get => config.mainHand;
        }
        public static string Champion
        {
            set => config.champion = value;
            get => config.champion;
        }
        public static float MonitorSize
        {
            set => config.monitorSize = value;
            get => config.monitorSize;
        }

        public static KeyboardKeyCodes PotionKey
        {
            set => config.potionKey = value;
            get => config.potionKey;
        }

        private void Awake() => Load();

        private static void Load()
        {
            filePath = GetPath("settings.json");
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);

                try
                {
                    config = JsonUtility.FromJson<ConfigValues>(text);
                }
                catch
                {
                    Debug.LogError("Cannot load data from JSON text. Generating default settings.");
                    SetStandardConfig();
                    Save();
                }
            }
            else
            {
                Debug.LogWarning($"No File at {filePath}. Generating default settings.");
                SetStandardConfig();
                Save();
            }
        }

        public static void Save()
        {
            try
            {
                string text = JsonUtility.ToJson(config);

                if (OnConfigChanged != null && File.Exists(filePath))
                {
                    string oldText = File.ReadAllText(filePath);
                    if(text != oldText)
                    {
                        OnConfigChanged();
                        Debug.Log("Updated config with new settings");
                    }
                }

                File.WriteAllText(filePath, text);
            }
            catch
            {
                Debug.LogError("Cannot save data as JSON text");
            }
        }

        private static void SetStandardConfig()
        {
            config = new ConfigValues
            {
                leftClick = VRKeyCodes.RIGHT_SECONDARY,
                rightClick = VRKeyCodes.RIGHT_PRIMARY,
                mainHand = Handedness.Right,
                champion = Enums.Champion.ANIVIA.ToString(),
                monitorSize = 2f,
                potionKey = KeyboardKeyCodes.N1
            };

            OnConfigChanged?.Invoke();
        }

        private static string GetPath(string filename) => Path.Combine(Application.persistentDataPath, "data", filename);
    }
}
