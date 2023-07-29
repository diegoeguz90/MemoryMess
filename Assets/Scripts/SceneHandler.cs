using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] Button startBtn, exitBtn, settingsBtn, saveSettingsBtn;
    [SerializeField] GameObject mainMenu, settingsMenu;
    [SerializeField] Slider rowSlider, columnSlider;

    private int rowSize;
    private int columnSize;

    // singleton
    public static SceneHandler instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        startBtn.onClick.AddListener(onClickStartBtn);
        exitBtn.onClick.AddListener(onClickExitBtn);
        settingsBtn.onClick.AddListener(onClickSettingsBtn);
        saveSettingsBtn.onClick.AddListener(onClickSaveSettings);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        getSlidersData();
        SettingsHandler.instance.columnSize = columnSize;
        SettingsHandler.instance.rowSize = rowSize;
    }

    private void getSlidersData() 
    {
        rowSize = (int)rowSlider.value;
        columnSize = (int)columnSlider.value;
    }
    private void setSlidersData()
    {
        rowSlider.value = rowSize;
        columnSlider.value = columnSize;
    }

    void onClickSettingsBtn()
    {
        SettingsHandler.instance.LoadSettings();
        rowSize = SettingsHandler.instance.rowSize;
        columnSize = SettingsHandler.instance.columnSize;
        setSlidersData();

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void onClickSaveSettings()
    {
        getSlidersData();
        SettingsHandler.instance.columnSize = columnSize;
        SettingsHandler.instance.rowSize = rowSize;
        SettingsHandler.instance.SaveSettings();

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    void onClickStartBtn()
    {
        SettingsHandler.instance.LoadSettings();
        rowSize = SettingsHandler.instance.rowSize;
        columnSize = SettingsHandler.instance.columnSize;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void onClickExitBtn()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
