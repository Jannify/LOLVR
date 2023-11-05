using System.IO;
using Edwon.VR;
using UnityEngine;

namespace LOLVR
{
    public class ConfigManager : MonoBehaviour
    {
        private static ConfigValues config;
        private static string filePath;

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
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            try
            {
                string text = JsonUtility.ToJson(config);
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
                champion = LOLVR.Champion.ANIVIA.ToString(),
                monitorSize = 2f
            };
        }

        private static string GetPath(string filename) => Path.Combine(Application.persistentDataPath, "data", filename);
    }
}
