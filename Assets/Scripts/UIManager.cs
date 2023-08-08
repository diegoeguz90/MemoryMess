using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button startBtn, exitBtn, settingsBtn, backSettingsMenu;
    [SerializeField] GameObject mainMenu, settingsMenu;

    // singleton
    public static UIManager instance;

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
        backSettingsMenu.onClick.AddListener(onClickBackSettings);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        SettingsManager.instance.LoadSettings();
    }

    void onClickSettingsBtn()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void onClickBackSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        SettingsManager.instance.SaveSettings();
    }

    void onClickStartBtn()
    {
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
