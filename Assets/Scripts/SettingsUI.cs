using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] Slider tMemorizeSlider, tOrganizeSlider;
    [SerializeField] TMP_Dropdown ScenarioDropdown;
    [SerializeField] TMP_Text showMemorizeValueTxt, ShowOrganizeValueTxt;

    // singleton 
    public static SettingsUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.instance.tMemorize = (int)tMemorizeSlider.value;
        SettingsManager.instance.tOrganize = (int)tOrganizeSlider.value;
        SettingsManager.instance.scenario = ScenarioDropdown.value;

        showMemorizeValueTxt.text = tMemorizeSlider.value.ToString() + " seg";
        ShowOrganizeValueTxt.text = tOrganizeSlider.value.ToString() + " seg";

        tMemorizeSlider.onValueChanged.AddListener(onMemorySliderChanged);
        tOrganizeSlider.onValueChanged.AddListener(onOrganizeSliderChanged);
        ScenarioDropdown.onValueChanged.AddListener(onScenarioDropdownChanged);
    }

    void onMemorySliderChanged(float sliderValue)
    {
        SettingsManager.instance.tMemorize = (int)sliderValue;
        showMemorizeValueTxt.text = sliderValue.ToString() + " seg";
    }

    void onOrganizeSliderChanged(float sliderValue)
    {
        SettingsManager.instance.tOrganize = (int)sliderValue;
        ShowOrganizeValueTxt.text = sliderValue.ToString() + " seg";
    }

    void onScenarioDropdownChanged(int dropdownValue)
    {
        SettingsManager.instance.scenario = dropdownValue;
    }

    void SetSettings()
    {
        tMemorizeSlider.value = SettingsManager.instance.tMemorize;
        tOrganizeSlider.value = SettingsManager.instance.tOrganize;
        ScenarioDropdown.value = SettingsManager.instance.scenario;
    }

    #region EventSuscription
    private void OnEnable()
    {
        SettingsManager.OnLoadData += SetSettings;
    }
    private void OnDisable()
    {
        SettingsManager.OnLoadData -= SetSettings;
    }
    #endregion
}
