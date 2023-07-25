using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button StartBtn, ExitBtn;

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
        StartBtn.onClick.AddListener(onClickStartBtn);
        ExitBtn.onClick.AddListener(onClickExitBtn);
    }

    void onClickStartBtn()
    {
        Debug.Log("Start button clicked!");
    }

    void onClickExitBtn()
    {
        Debug.Log("Exit button clicked!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
