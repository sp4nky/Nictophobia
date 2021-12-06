using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider voicesSlider;
    public Slider musicSlider;


    void Update()
    {
        UpdateVolumeValues();
    }

    private void UpdateVolumeValues()
    {
        AudioManager.instance.setSoundEffectsVolume(sfxSlider.value);
        AudioManager.instance.setMusicVolume(musicSlider.value);
        AudioManager.instance.setVoicesVolume(voicesSlider.value);
    }
}
