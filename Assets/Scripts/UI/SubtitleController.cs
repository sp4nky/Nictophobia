using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour
{
    [Header("Text to show")]
    [Tooltip("Recomendations: Max of 200 character per sentences")]
    public Text subtitles;
    public Image background;

    private bool isPlaying;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void WriteDialogue(DialogueData dialogueData)
    {
        if(!isPlaying)
            StartCoroutine(ShowDialogue(dialogueData));
    }

    IEnumerator ShowDialogue(DialogueData dialogueData)
    {
        isPlaying = true;
        EnableSubtitles();
        foreach (DialogueData.DialoguePart dialogue in dialogueData.dialogues)
        {
            foreach (DialogueData.Texts simpleDialogue in dialogue.texts)
            {
                subtitles.text = dialogue.characterName + ": ";
                subtitles.text += simpleDialogue.text;
                yield return new WaitForSeconds(simpleDialogue.timeToShow);
            }
        }
        DisableSubtitles();
        isPlaying = false;
        yield return null;

    }

    public void EnableSubtitles()
    {
        background.enabled = true;
        subtitles.enabled = true;
    }

    public void DisableSubtitles()
    {
        background.enabled = false;
        subtitles.enabled = false;
    }
}
