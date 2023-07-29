using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    [System.Serializable]
    class PlayerSettings
    {
        public int rowSize;
        public int columnSize;
    }

    public int rowSize { get; set; }
    public int columnSize { get; set; }

    // singleton
    public static SettingsHandler instance;

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
        settings.rowSize = rowSize;
        settings.columnSize = columnSize;

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

            rowSize = settings.rowSize;
            columnSize = settings.columnSize;
        }
    }
}
