using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // singleton
    public static SettingsManager instance;

    [System.Serializable]
    public class PlayerSettings
    {
        public int tMemorize;
        public int tOrganize;
        public int scenario;
    }

    public int tMemorize { get; set; }
    public int tOrganize { get; set; }
    public int scenario { get; set; }

    public delegate void LoadData();
    public static event LoadData OnLoadData;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        // persistence
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSettings()
    {
        PlayerSettings settings = new PlayerSettings();
        settings.tMemorize = tMemorize;
        settings.tOrganize = tOrganize;
        settings.scenario = scenario;

        string json = JsonUtility.ToJson(settings);

        File.WriteAllText(Application.persistentDataPath + "/userSettings.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/userSettings.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerSettings settings = JsonUtility.FromJson<PlayerSettings>(json);

            tMemorize = settings.tMemorize;
            tOrganize = settings.tOrganize;
            scenario = settings.scenario;

            OnLoadData();
        }
    }
}
