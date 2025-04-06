using MelonLoader;
using PTAPluginLoader;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[assembly: MelonInfo(typeof(PTALoader), "PTAc-h Plugin Loader", "0.3", "PTac-h")]

namespace PTAPluginLoader
{
    [System.Serializable]
    public class PrefabList
    {
        public List<string> prefabs;
    }

    public class PTALoader : MelonMod
    {
        private Dictionary<string, List<string>> PTAplugins = new Dictionary<string, List<string>>();
        private List<GameObject> PTAObjects = new List<GameObject>();

        private readonly string pluginFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "PTA-Plugins");

        public override void OnInitializeMelon()
        {
            DiscoverPlugins();
            LoadPlugins();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been initialized!");
            InstantiatePlugins();
            base.OnSceneWasInitialized(buildIndex, sceneName);
        }

        private void InstantiatePlugins()
        {
            foreach (GameObject obj in PTAObjects)
            {
                MelonLogger.Msg($"Instantiating {obj.name}");
                GameObject.Instantiate(obj);
            }
        }

        private void DiscoverPlugins()
        {
            if (!Directory.Exists(pluginFolderPath))
            {
                Directory.CreateDirectory(pluginFolderPath);
                return;
            }

            foreach (string jsonFile in Directory.GetFiles(pluginFolderPath, "*.json"))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(jsonFile);
                string bundlePath = Path.Combine(pluginFolderPath, fileNameWithoutExtension);

                if (!File.Exists(bundlePath))
                {
                    MelonLogger.Warning($"Bundle file not found for {jsonFile}, expected at: {bundlePath}");
                    continue;
                }

                try
                {
                    string jsonContent = File.ReadAllText(jsonFile);
                    PrefabList prefabList = JsonUtility.FromJson<PrefabList>(jsonContent);

                    if (prefabList != null && prefabList.prefabs != null && prefabList.prefabs.Count > 0)
                    {
                        PTAplugins[bundlePath] = prefabList.prefabs;
                        MelonLogger.Msg($"Discovered bundle: {bundlePath} with prefabs: {string.Join(", ", prefabList.prefabs)}");
                    }
                    else
                    {
                        MelonLogger.Warning($"No prefabs listed in: {jsonFile}");
                    }
                }
                catch (System.Exception ex)
                {
                    MelonLogger.Error($"Failed to parse JSON file {jsonFile}: {ex.Message}");
                }
            }
        }

        private void LoadPlugins()
        {
            foreach (var kvp in PTAplugins)
            {
                string bundlePath = kvp.Key;
                List<string> prefabNames = kvp.Value;

                MelonLogger.Msg($"Loading PTA Plugin Bundle: {bundlePath}");

                AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
                if (bundle == null)
                {
                    MelonLogger.Error($"Failed to load AssetBundle from path: {bundlePath}");
                    continue;
                }

                foreach (string prefabName in prefabNames)
                {
                    GameObject PTAObj = bundle.LoadAsset<GameObject>(prefabName);

                    if (PTAObj != null)
                    {
                        PTAObjects.Add(PTAObj);
                        MelonLogger.Msg($"Loaded prefab: {prefabName}");
                    }
                    else
                    {
                        MelonLogger.Warning($"Failed to load prefab '{prefabName}' from bundle {bundlePath}");
                    }
                }
            }
        }
    }
}
