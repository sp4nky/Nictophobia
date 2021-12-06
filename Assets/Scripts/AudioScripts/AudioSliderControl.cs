using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliderControl : MonoBehaviour
{
    public enum soundTypes { master, music, soundEffect, voice }
    [Tooltip("Sound type controlled by this slider control.")]
    public soundTypes soundType = soundTypes.master;

    private bool refreshedFrame = false;
    private Slider audioSlider;

    private void Awake()
    {
        audioSlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.audioSliders.Add(this);
        Refresh();
        audioSlider.onValueChanged.RemoveListener(setVolume);
        audioSlider.onValueChanged.AddListener(setVolume);
    }

    /// <summary>
    /// Sets the volume of the sound type specified
    /// </summary>
    /// <param name="volume"></param>
    public void setVolume(float volume)
    {
        if (!refreshedFrame)
        {
            switch (soundType)
            {
                case soundTypes.master:
                    AudioManager.instance.setMasterVolume(volume);
                    break;
                case soundTypes.music:
                    AudioManager.instance.setMusicVolume(volume);
                    break;
                case soundTypes.soundEffect:
                    AudioManager.instance.setSoundEffectsVolume(volume);
                    break;
                case soundTypes.voice:
                    AudioManager.instance.setVoicesVolume(volume);
                    break;
            }
        }
    }

    /// <summary>
    /// Refresh the audio slider value
    /// </summary>
    public void Refresh()
    {
        refreshedFrame = true;

        switch (soundType)
        {
            case soundTypes.master:
                audioSlider.value = AudioManager.instance.masterVolume;
                break;
            case soundTypes.music:
                audioSlider.value = AudioManager.instance.musicVolume;
                break;
            case soundTypes.soundEffect:
                audioSlider.value = AudioManager.instance.soundEffectsVolume;
                break;
            case soundTypes.voice:
                audioSlider.value = AudioManager.instance.voiceVolume;
                break;
        }

        if (gameObject.activeInHierarchy) { StartCoroutine(QuitRefreshedFrameOnNextFrame()); }
        else { refreshedFrame = false; }
    }

    /// <summary>
    /// Sets the refreshedFrame variable to false on the next frame
    /// </summary>
    /// <returns></returns>
    public IEnumerator QuitRefreshedFrameOnNextFrame()
    {
        yield return null;
        refreshedFrame = false;
    }

    /// <summary>
    /// Removes this AudioSliderControl from the AudioManager
    /// </summary>
    private void OnDestroy()
    {
        AudioManager.instance.audioSliders.Remove(this);
    }
}
