using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider tMemorizeSlider, tOrganizeSlider;
    [SerializeField] TMP_Dropdown ScenarioDropdown;

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
        SavingDataManager.instance.tMemorize = (int)tMemorizeSlider.value;
        SavingDataManager.instance.tOrganize = (int)tOrganizeSlider.value;
        SavingDataManager.instance.scenario = ScenarioDropdown.value;

        tMemorizeSlider.onValueChanged.AddListener(onMemorySliderChanged);
        tOrganizeSlider.onValueChanged.AddListener(onOrganizeSliderChanged);
        ScenarioDropdown.onValueChanged.AddListener(onScenarioDropdownChanged);
    }

    void onMemorySliderChanged(float sliderValue)
    {
        SavingDataManager.instance.tMemorize = (int)sliderValue;
    }

    void onOrganizeSliderChanged(float sliderValue)
    {
        SavingDataManager.instance.tOrganize = (int)sliderValue;
    }

    void onScenarioDropdownChanged(int dropdownValue)
    {
        SavingDataManager.instance.scenario = dropdownValue;
    }

    void SetSettings()
    {
        tMemorizeSlider.value = SavingDataManager.instance.tMemorize;
        tOrganizeSlider.value = SavingDataManager.instance.tOrganize;
        ScenarioDropdown.value = SavingDataManager.instance.scenario;
    }

    #region EventSuscription
    private void OnEnable()
    {
        SavingDataManager.OnLoadData += SetSettings;
    }
    private void OnDisable()
    {
        SavingDataManager.OnLoadData -= SetSettings;
    }
    #endregion
}
