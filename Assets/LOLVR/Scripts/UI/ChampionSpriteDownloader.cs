using System;
using System.Collections;
using System.IO;
using System.Linq;
using LOLVR.Enums;
using UnityEngine;
using UnityEngine.Networking;

namespace LOLVR.UI
{
    public class ChampionSpriteDownloader : MonoBehaviour
    {
        private void Start() => StartCoroutine(DownloadAllChampionIcons());

        public static Sprite GetIcon(string championName)
        {
            Texture2D texture = new Texture2D(120, 120);
            try
            {
                texture.LoadImage(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "data", "ChampionIcons", championName + ".png")));
            }
            catch
            {
                // ignored
            }
            return Sprite.Create(texture, new Rect(0, 0, 120, 120), new Vector2(0.5f, 0.5f));

        }

        private IEnumerator DownloadAllChampionIcons()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "data", "ChampionIcons", Champion.ANIVIA + ".png")))
            {
                foreach (string championName in Enum.GetNames(typeof(Champion)))
                {
                    string url = $"https://ddragon.leagueoflegends.com/cdn/10.18.1/img/champion/{ToPascalCase(championName)}.png";
                    string savePath = Path.Combine(Application.persistentDataPath, "data", "ChampionIcons", championName + ".png");
                    yield return StartCoroutine(DownloadImage(url, savePath));
                }
                gameObject.BroadcastMessage("ReloadOptions");
            }
        }

        private static IEnumerator DownloadImage(string url, string savePath)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError(uwr.error);
                }
                else
                {
                    Debug.Log($"Successfully downloaded image with {url}");
                    byte[] results = uwr.downloadHandler.data;
                    SaveImage(savePath, results);
                }
            }
        }

        private static void SaveImage(string path, byte[] imageBytes)
        {
            //Create Directory if it does not exist
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            try
            {
                File.WriteAllBytes(path, imageBytes);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed To Save Data to: {path.Replace("/", "\\")} with error code: {e.Message}");
            }
        }

        private static string ToPascalCase(string text)
        {
            return string.Join("", text.Split('_').Select(w => w.Trim()).Where(w => w.Length > 0)
                                       .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
        }
    }
}
