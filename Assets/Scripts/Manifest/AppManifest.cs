using System;
using System.IO;
using UnityEngine;

namespace Manifest
{
    [Serializable]
    [DefaultExecutionOrder(-10)]
    public class AppManifest
    {
        public static string ManifestPath => Application.persistentDataPath + "/Manifest.json";
    
        public static AppManifest Instance { get; private set; }

        public int DeviceId;
    
        public VideoData[] Videos;

        public int DefaultVideo;

        public string ServerAddress;

        public string ServerKeyName;
    
        public string ServerKey;

        public bool SendServerKey = false;
    
        public AppManifest()
        {
            Videos = new VideoData[2];
            DefaultVideo = 0;
            ServerAddress = "http://192.168.225.2";
            ServerKey = "9df034a4-dfd5-498a-8c4a-2496a9db5663";
        }
    
        public VideoData GetDataByID(int id)
        {
            return Videos[id];
        }

        public VideoData GetDefaultData()
        {
            return GetDataByID(DefaultVideo);
        }
    
        public static AppManifest GetOrCreateManifest()
        {
            string manifest = "";
        
            if (File.Exists(ManifestPath))
            { 
                Debug.Log("Found Manifest At Path: " + ManifestPath);
                manifest = File.ReadAllText(ManifestPath);
            }
            else
            {
                Debug.Log("Creating Manifest At Path: " + ManifestPath);

                AppManifest manifestData = new AppManifest();

                manifest = JsonUtility.ToJson(manifestData);
            
                FileStream stream = File.Create(ManifestPath);
            
                StreamWriter writer = new StreamWriter(stream);
            
                writer.Write(manifest);
                writer.Close();
                stream.Close();
            }

            return JsonUtility.FromJson<AppManifest>(manifest);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance()
        {
            Instance = GetOrCreateManifest();
        }
    }
}
