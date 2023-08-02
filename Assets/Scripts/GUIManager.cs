using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject topTimer;
    [SerializeField] GameObject middleText;
    [SerializeField] GameObject bottomTimer;
    [SerializeField] Button restartBtn;
    [SerializeField] List<GameObject> scenario;

    private TMP_Text topTxt;
    private TMP_Text middleTxt;
    private TMP_Text bottomTxt;
    private int scenarioIndex;

    private void Start()
    {
        topTxt = topTimer.GetComponentInChildren<TMP_Text>();
        middleTxt = middleText.GetComponentInChildren<TMP_Text>();
        bottomTxt = bottomTimer.GetComponentInChildren<TMP_Text>();
        scenarioIndex = SettingsDataManager.instance.scenario;

        restartBtn.onClick.AddListener(onClickRestartBtn);

        foreach (GameObject matrix in scenario)
        {
            matrix.SetActive(false);
        }
        scenario[scenarioIndex].SetActive(true);

        topTimer.SetActive(false);
        middleText.SetActive(false);
        bottomTimer.SetActive(false);
    }

    private void Update()
    {
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.instructions1)
        {
            topTimer.SetActive(false);
            middleText.SetActive(true);
            bottomTimer.SetActive(false);
            middleTxt.text = "Recuerda donde va cada elemento!";
        }
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.memorize)
        {
            topTimer.SetActive(true);
            middleText.SetActive(false);
            bottomTimer.SetActive(false);
            topTxt.text = GameTimeManager.Instance.memorizeTimer._waitTime.ToString("0") + " seg";
        }
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.instructions2)
        {
            topTimer.SetActive(false);
            middleText.SetActive(true);
            bottomTimer.SetActive(false);
            middleTxt.text = "Regresa los elementos a su lugar!";
        }
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.play)
        {
            topTimer.SetActive(false);
            middleText.SetActive(false);
            bottomTimer.SetActive(true);
            bottomTxt.text = GameTimeManager.Instance.organiceTimer._waitTime.ToString("0") + " seg";
        }
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.results)
        {
            topTimer.SetActive(false);
            middleText.SetActive(true);
            bottomTimer.SetActive(false);
            middleTxt.text = "Puntaje: " + GameManager.Instance.returnScore();
        }
    }

    void onClickRestartBtn()
    {
        SceneManager.LoadScene(0);
    }
}
