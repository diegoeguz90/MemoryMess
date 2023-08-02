using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider tMemorizeSlider, tOrganizeSlider;
    [SerializeField] TMP_Dropdown ScenarioDropdown;
    [SerializeField] TMP_Text showMemorizeValueTxt, ShowOrganizeValueTxt;

    // singleton 
    public static SettingsManager instance;

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
        SettingsDataManager.instance.tMemorize = (int)tMemorizeSlider.value;
        SettingsDataManager.instance.tOrganize = (int)tOrganizeSlider.value;
        SettingsDataManager.instance.scenario = ScenarioDropdown.value;

        showMemorizeValueTxt.text = tMemorizeSlider.value.ToString() + " seg";
        ShowOrganizeValueTxt.text = tOrganizeSlider.value.ToString() + " seg";

        tMemorizeSlider.onValueChanged.AddListener(onMemorySliderChanged);
        tOrganizeSlider.onValueChanged.AddListener(onOrganizeSliderChanged);
        ScenarioDropdown.onValueChanged.AddListener(onScenarioDropdownChanged);
    }

    void onMemorySliderChanged(float sliderValue)
    {
        SettingsDataManager.instance.tMemorize = (int)sliderValue;
        showMemorizeValueTxt.text = sliderValue.ToString() + " seg";
    }

    void onOrganizeSliderChanged(float sliderValue)
    {
        SettingsDataManager.instance.tOrganize = (int)sliderValue;
        ShowOrganizeValueTxt.text = sliderValue.ToString() + " seg";
    }

    void onScenarioDropdownChanged(int dropdownValue)
    {
        SettingsDataManager.instance.scenario = dropdownValue;
    }

    void SetSettings()
    {
        tMemorizeSlider.value = SettingsDataManager.instance.tMemorize;
        tOrganizeSlider.value = SettingsDataManager.instance.tOrganize;
        ScenarioDropdown.value = SettingsDataManager.instance.scenario;
    }

    #region EventSuscription
    private void OnEnable()
    {
        SettingsDataManager.OnLoadData += SetSettings;
    }
    private void OnDisable()
    {
        SettingsDataManager.OnLoadData -= SetSettings;
    }
    #endregion
}
