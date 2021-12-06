using UnityEngine;

public class AudioMessenger : MonoBehaviour
{
    /// <summary>
    /// Sets the Master volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volume"></param>
    public void setMasterVolume(float volume)
    {
        AudioManager.instance.setMasterVolume(volume);
    }

    /// <summary>
    /// Sets the Music volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volume"></param>
    public void setMusicVolume(float volume)
    {
        AudioManager.instance.setMusicVolume(volume);
    }

    /// <summary>
    /// Sets the Sound Effects volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volume"></param>
    public void setSoundEffectsVolume(float volume)
    {
        AudioManager.instance.setSoundEffectsVolume(volume);
    }

    /// <summary>
    /// Sets the Voices volume of the game and refresh the audio sources.
    /// </summary>
    /// <param name="volume"></param>
    public void setVoicesVolume(float volume)
    {
        AudioManager.instance.setVoicesVolume(volume);
    }
}