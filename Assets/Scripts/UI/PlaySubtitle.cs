using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle : MonoBehaviour
{
    public SubtitleController subtitleController;
    [Tooltip("When audio is playing write the dialogue subtitles")]
    public List<DialogueData> subtitles = new List<DialogueData>();
    private AudioSource audioSource;

    private int index;
    private bool isPlayingSubtitle;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioSource.playOnAwake)
            PlayDialogue();
    }

    private void FixedUpdate()
    {
        if (audioSource.isPlaying && !isPlayingSubtitle)
        {
            isPlayingSubtitle = true;
            PlayDialogue();
        }
        else if(!audioSource.isPlaying)
            isPlayingSubtitle = false;
    }

    public void PlayDialogue()
    {
        if (index >= subtitles.Count) return;
        subtitleController.WriteDialogue(subtitles[index]);
        index++;
    }
}
