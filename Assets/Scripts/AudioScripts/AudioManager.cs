using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<SoundBoard> musics;
    public List<SoundBoard> soundEffects;
    public List<SoundBoard> voices;
    [HideInInspector] public List<AudioSliderControl> audioSliders;

    public float masterVolume { get; private set; } = 1;
    public float musicVolume { get; private set; } = 1;
    public float soundEffectsVolume { get; private set; } = 1;
    public float voiceVolume { get; private set; } = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
        RefreshVolumes();
    }

    #region AudioSource Control

    /// <summary>
    /// Plays the sound specified.
    /// </summary>
    /// <param name="soundName"></param>
    public void Play(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);

        if (!sound) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't exist."); return; }
        else if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't have an AudioSource attached to it."); return; }

        sound.source.Play();
    }

    /// <summary>
    /// Plays a random clip of the Random Clips array
    /// </summary>
    /// <param name="soundName"></param>
    public void PlayRandomClip(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);

        if (!sound) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't exist."); return; }
        else if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't have an AudioSource attached to it."); return; }

        sound.PlayRandomClip();
    }

    /// <summary>
    /// Stops the sound specified.
    /// </summary>
    /// <param name="soundName"></param>
    public void Stop(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);

        if (!sound) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't exist."); return; }
        else if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't have an AudioSource attached to it."); return; }

        sound.source.Stop();
    }

    /// <summary>
    /// Sets ON the loop state of the specified sound.
    /// </summary>
    /// <param name="soundName"></param>
    public void Loop(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);

        if (!sound) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't exist."); return; }
        else if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't have an AudioSource attached to it."); return; }

        sound.source.loop = true;
    }

    /// <summary>
    /// Sets OFF the loop state of the specified sound.
    /// </summary>
    /// <param name="soundName"></param>
    public void UnLoop(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);

        if (!sound) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't exist."); return; }
        else if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + soundName + " doesn't have an AudioSource attached to it."); return; }

        sound.source.loop = false;
    }

    /// <summary>
    /// Mutes all sounds of some type
    /// </summary>
    /// <param name="volumeType"></param>
    public void MuteAll(string volumeType)
    {
        foreach (SoundBoard sound in getSoundsList(volumeType)) { sound.source.mute = true; }
    }

    /// <summary>
    /// Unmutes all sounds of some type
    /// </summary>
    /// <param name="volumeType"></param>
    public void UnmuteAll(string volumeType)
    {
        foreach (SoundBoard sound in getSoundsList(volumeType)) { sound.source.mute = false; }
    }

    /// <summary>
    /// Gets all SoundBoards of some type
    /// </summary>
    /// <param name="volumeType"></param>
    /// <returns></returns>
    private List<SoundBoard> getSoundsList(string volumeType)
    {
        List<SoundBoard> sounds;
        switch (volumeType)
        {
            case "musics": sounds = musics; break;
            case "soundEffects": sounds = soundEffects; break;
            case "voices": sounds = voices; break;
            default: Debug.LogError($"{this} Script: volume type specified on MuteAll function doesn't exist."); return null;
        }

        return sounds;
    }

    #endregion

    #region Fade Effects Control

    /// <summary>
    /// Makes a fade out effect on a Default time.
    /// </summary>
    /// <param name="soundName"></param>
    public void FadeOut(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.FadeOut();
    }

    /// <summary>
    /// Makes a fade out effect.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="time"></param>
    public void FadeOut(string soundName, float time)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.FadeOut(time);
    }

    /// <summary>
    /// Makes a fade effect to the original volume.
    /// </summary>
    /// <param name="soundName"></param>
    public void FadeToOriginalVolume(string soundName)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.FadeToOriginalVolume();
    }

    /// <summary>
    /// Makes a fade effect to the original volume on a Default time.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="time"></param>
    public void FadeToOriginalVolume(string soundName, float time)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.FadeToOriginalVolume(time);
    }

    /// <summary>
    /// Makes a fade effect to the specified volume on a Default time.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="toVolume"></param>
    public void Fade(string soundName, float toVolume)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.Fade(toVolume);
    }

    /// <summary>
    /// Makes a fade effect to the specified volume.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="toVolume"></param>
    /// <param name="time"></param>
    public void Fade(string soundName, float toVolume, float time)
    {
        SoundBoard sound = FindSoundBoard(soundName);
        sound.Fade(toVolume, time);
    }

    /// <summary>
    /// Makes a CrossFade effect between two sounds.
    /// </summary>
    /// <param name="fromSoundName"></param>
    /// <param name="toSoundName"></param>
    /// <param name="time"></param>
    public void CrossFade(string fromSoundName, string toSoundName, float time)
    {
        SoundBoard fromSound = FindSoundBoard(fromSoundName);
        SoundBoard toSound = FindSoundBoard(toSoundName);

        fromSound.FadeOut(time);
        toSound.FadeToOriginalVolume(time);
    }

    /// <summary>
    /// Fade effect.
    /// </summary>
    /// <param name="toVolume"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeEffect(SoundBoard sound, float toVolume, float time)
    {
        float timer = time;
        float fromVolume = sound.volume;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            if (timer < 0) { timer = 0; }
            sound.volume = Mathf.Lerp(toVolume, fromVolume, timer / time);
            ApplyVolume(sound);
        }
    }

    #endregion

    /// <summary>
    /// Find a SoundBoard by his name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SoundBoard FindSoundBoard(string name)
    {
        SoundBoard sound;

        sound = musics.Find(SoundBoard => SoundBoard.soundName == name);
        if (sound != null) { return sound; }
        sound = soundEffects.Find(SoundBoard => SoundBoard.soundName == name);
        if (sound != null) { return sound; }
        sound = voices.Find(SoundBoard => SoundBoard.soundName == name);
        if (sound != null) { return sound; }
        else { Debug.LogError("AudioManager Script: SoundBoard name doesn't exist."); return null; }
    }

    #region Volume control

    /// <summary>
    /// Sets the Master volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volumenValue"></param>
    public void setMasterVolume(float volumenValue)
    {
        SetVolume(volumenValue, "Master");
    }

    /// <summary>
    /// Sets the Music volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volumenValue"></param>
    public void setMusicVolume(float volumenValue)
    {
        SetVolume(volumenValue, "Music");
    }

    /// <summary>
    /// Sets the Sound Effects volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volumenValue"></param>
    public void setSoundEffectsVolume(float volumenValue)
    {
        SetVolume(volumenValue, "Sound Effects");
    }

    /// <summary>
    /// Sets the Voices volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volumenValue"></param>
    public void setVoicesVolume(float volumenValue)
    {
        SetVolume(volumenValue, "Voices");
    }

    /// <summary>
    /// Set a volume value to a volume.
    /// </summary>
    /// <param name="volumenValue"></param>
    /// <param name="soundType"></param>
    private void SetVolume(float volumenValue, string soundType)
    {
        switch (soundType)
        {
            case "Master":
                masterVolume = volumenValue;
                break;
            case "Music":
                musicVolume = volumenValue;
                break;
            case "Sound Effects":
                soundEffectsVolume = volumenValue;
                break;
            case "Voices":
                voiceVolume = volumenValue;
                break;
        }

        RefreshVolumes();
    }

    /// <summary>
    /// Refresh ALL audio sources volumes and audio sliders values.
    /// </summary>
    public void RefreshVolumes()
    {
        foreach (SoundBoard sound in musics)
        {
            if (sound.source) { sound.source.volume = sound.volume * musicVolume * masterVolume; }
        }

        foreach (SoundBoard sound in soundEffects)
        {
            if (sound.source) { sound.source.volume = sound.volume * soundEffectsVolume * masterVolume; }
        }

        foreach (SoundBoard sound in voices)
        {
            if (sound.source) { sound.source.volume = sound.volume * voiceVolume * masterVolume; }
        }

        foreach (AudioSliderControl slider in audioSliders)
        {
            slider.Refresh();
        }
    }

    /// <summary>
    /// Refresh a specific audio source volume.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="volumeType"></param>
    public void ApplyVolume(SoundBoard sound)
    {
        if (!sound.source) { Debug.LogError("AudioManager Script: SoundBoard " + sound.soundName + " doesn't have an AudioSource component attached to it."); return; }

        float volume = 0;

        switch (sound.soundType)
        {
            case SoundBoard.soundTypes.music:
                volume = musicVolume;
                break;
            case SoundBoard.soundTypes.soundEffect:
                volume = soundEffectsVolume;
                break;
            case SoundBoard.soundTypes.voice:
                volume = voiceVolume;
                break;
        }

        sound.source.volume = sound.volume * volume * masterVolume;
    }

    #endregion

}