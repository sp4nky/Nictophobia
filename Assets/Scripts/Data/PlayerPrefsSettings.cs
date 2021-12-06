using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsSettings : MonoBehaviour
{

    public Slider sfxSlider;
    public Slider voicesSlider;
    public Slider musicSlider;

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        PlayerPrefs.SetFloat("voices", voicesSlider.value);
        PlayerPrefs.SetFloat("music", musicSlider.value);
    }

    public void LoadVolumeSettings()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", 1);
        voicesSlider.value = PlayerPrefs.GetFloat("voices", 1);
        musicSlider.value = PlayerPrefs.GetFloat("music", 1);
    }
}
