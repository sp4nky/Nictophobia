using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    public GameObject mainPanel;
    public Button btnResume;
    public Button btnExit;

    [Header("Volume Settings")]
    //public Slider volumeGeneral;
    public Slider volumeSFX;
    public Slider volumeMusic;
    public Slider volumeVoices;
    private bool isPaused;

    void Start()
    {
        btnResume.onClick.AddListener(Resume);
        btnExit.onClick.AddListener(GameController.Instance.Exit); 

        //volumeGeneral.value = AudioManager.instance.masterVolume;
        /*volumeSFX.value = AudioManager.instance.soundEffectsVolume;
        volumeMusic.value = AudioManager.instance.musicVolume;
        volumeVoices.value = AudioManager.instance.voiceVolume;*/
    }

   
    private void Update()
    {
        bool pause = Input.GetButtonDown("Cancel");
        if (pause && !isPaused)
        {
            Pause();
        }

        UpdateVolumeValues();
    }

    private void UpdateVolumeValues()
    {
        //AudioManager.instance.setMasterVolume(volumeGeneral.value);
        AudioManager.instance.setSoundEffectsVolume(volumeSFX.value);
        AudioManager.instance.setMusicVolume(volumeMusic.value);
        AudioManager.instance.setVoicesVolume(volumeVoices.value);
    }

    private void Pause()
    {
        isPaused = true;
        EnablePauseScreen();
        GameController.Instance.PauseGame();
    }
    public void EnablePauseScreen()
    {
        mainPanel.SetActive(true);
    }

    public void Resume()
    {
        GameController.Instance.ResumeGame();
        isPaused = false;
        mainPanel.SetActive(false);
    }

}
