using BasicSurvivalKit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class SoundBoard : MonoBehaviour
{
    #region Public Variables

    [Header("Main Settings")]
    [Tooltip("Audio Source to control.")]
    public AudioSource source;
    [ReadOnly]
    public string clip;

    [Header("Sound Settings")]
    [Tooltip("Sound Name used by the Audio Manager.")]
    public string soundName;
    public enum soundTypes { music, soundEffect, voice }
    [Tooltip("Sound type of this audio.")]
    public soundTypes soundType = soundTypes.music;
    [Range(0f, 1f), Tooltip("Base volume of this sound.")]
    public float volume = 1;

    [Header("Extra Settings")]
    public DefaultFadeTimeSettings defaultFadeTimeSettings;
    [Tooltip("Audio clips used by PlayRandomClip and PlayClip functions.")]
    public AudioClip[] clips = new AudioClip[0];
    [Tooltip("Plays looped random Clips")]
    public bool PlayRandomEnable = false;
    [Tooltip("Settings used by PlayVariantClip function.")]
    public VariantSettings variantSettings;
    #endregion

    #region Private Variables

    private float originalPitch;
    private float originalVolume;
    private float myVolume;
    private List<SoundBoard> mySound;

    #endregion

    #region Default Functions

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
        originalVolume = volume;
        originalPitch = source.pitch;
        myVolume = volume;
    }

    private void Start()
    {
        switch (soundType)
        {
            case soundTypes.music:
                mySound = AudioManager.instance.musics;
                break;
            case soundTypes.soundEffect:
                mySound = AudioManager.instance.soundEffects;
                break;
            case soundTypes.voice:
                mySound = AudioManager.instance.voices;
                break;
        }

        mySound.Add(this);

        if (source)
        {
            AudioManager.instance.ApplyVolume(this);
        }

      
    }

    private void Update()
    {
        if (myVolume != volume) { myVolume = volume; AudioManager.instance.ApplyVolume(this); }
        if (PlayRandomEnable && !source.isPlaying) { PlayRandomClip(); };
    }
    /// <summary>
    /// Destroy this SoundBoard from the AudioManager.
    /// </summary>
    private void OnDestroy()
    {
        if (mySound != null) { mySound.Remove(this); }
    }

    #endregion

    /// <summary>
    /// Plays a random clip from Clips array
    /// </summary>
    public void PlayRandomClip()
    {
        if (clips.Length <= 0) { Debug.LogError("SoundBoard Script: Random Clips array of the " + gameObject.name + " GameObject doesn't have any clip attached."); return; }

        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    public void PlayVariantClip()
    {
        float minPitch = (originalPitch - variantSettings.pitchRange >= -3) ? originalPitch - variantSettings.pitchRange : -3;
        float maxPitch = (originalPitch + variantSettings.pitchRange <= 3) ? originalPitch + variantSettings.pitchRange : 3;
        float pitch = Random.Range(minPitch, maxPitch);

        float minVolume = (originalVolume - variantSettings.volumeRange >= 0) ? originalVolume - variantSettings.volumeRange : 0;
        float maxVolume = (originalVolume + variantSettings.volumeRange <= 1) ? originalVolume + variantSettings.volumeRange : 1;
        float volume = Random.Range(minVolume, maxVolume);

        source.pitch = pitch;
        source.volume = volume;
        source.Play();
    }

    /// <summary>
    /// Plays a clip from Clips array
    /// </summary>
    /// <param name="clip"></param>
    public void PlayClip(float clip) { PlayClip((int)clip); }

    /// <summary>
    /// Plays a clip from Clips array
    /// </summary>
    /// <param name="clip"></param>
    public void PlayClip(int clip)
    {
        try
        {
            source.clip = clips[clip];
            source.Play();
        }
        catch (UnityException) { Debug.LogError("SoundBoard Script: Clips array from " + gameObject.name + " doesn't have the clip specified on PlayClip function."); }
    }

    /// <summary>
    /// Returns the Pitch and Volume to his original values
    /// </summary>
    public void ReturnToOriginalSettings()
    {
        source.pitch = originalPitch;
        source.volume = originalVolume;
    }

    #region Volume Control

    /// <summary>
    /// Sets the base volume of this sound.
    /// </summary>
    /// <param name="newVolume"></param>
    public void setVolume(float newVolume)
    {
        if (newVolume >= 0 && newVolume <= 1) { volume = newVolume; }
        else if (newVolume < 0) { volume = 0; }
        else if (newVolume > 1) { volume = 1; }

        myVolume = volume;

        AudioManager.instance.ApplyVolume(this);
    }

    /// <summary>
    /// Resets the volume of this SoundBoard to his original volume.
    /// </summary>
    public void ResetVolume()
    {
        volume = originalVolume;

        AudioManager.instance.ApplyVolume(this);
    }

    #endregion

    #region Fade Effect

    /// <summary>
    /// Makes a fade out effect with the Default time.
    /// </summary>
    public void FadeOut()
    {
        Fade(0, defaultFadeTimeSettings.defaultFadeOutTime);
    }

    /// <summary>
    /// Makes a fade out effect.
    /// </summary>
    /// <param name="time"></param>
    public void FadeOut(float time)
    {
        Fade(0, time);
    }

    /// <summary>
    /// Makes a fade effect to the original volume.
    /// </summary>
    public void FadeToOriginalVolume()
    {
        Fade(originalVolume, defaultFadeTimeSettings.defaultFadeToOriginalTime);
    }

    /// <summary>
    /// Makes a fade effect to the original volume with the Default time.
    /// </summary>
    /// <param name="time"></param>
    public void FadeToOriginalVolume(float time)
    {
        Fade(originalVolume, time);
    }

    /// <summary>
    /// Makes a fade effect to the specified volume and the Default time.
    /// </summary>
    /// <param name="toVolume"></param>
    public void Fade(float toVolume)
    {
        Fade(toVolume, defaultFadeTimeSettings.defaultFadeTime);
    }

    /// <summary>
    /// Makes a fade effect to the specified volume.
    /// </summary>
    public void Fade(float toVolume, float time)
    {
        if (toVolume < 0) { toVolume = 0; }
        else if (toVolume > 1) { toVolume = 1; }

        StartCoroutine(AudioManager.instance.FadeEffect(this, toVolume, time));
    }

    #endregion

    private void OnValidate()
    {
        if (source)
        {
            if (source.clip != null)
            {
                clip = source.clip.name;
                if (soundName == "") { soundName = source.clip.name; }
            }
            else { source = null; }
        }
    }

    [System.Serializable]
    public class DefaultFadeTimeSettings
    {
        public float defaultFadeTime = 1;
        public float defaultFadeOutTime = 1;
        public float defaultFadeToOriginalTime = 1;
    }

    [System.Serializable]
    public class VariantSettings
    {
        [Range(0, 6), Tooltip("Pitch range covered by PlayVariantClip function to both sides.")]
        public float pitchRange = 1;
        [Range(0, 1), Tooltip("Volume range covered by PlayVariantClip function to both sides.")]
        public float volumeRange = 0.2f;
    }
}