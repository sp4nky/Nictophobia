using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    public DialoguePart[] dialogues;


    [Serializable]
    public class DialoguePart
    {
        public string characterName;
        public Texts[] texts;
    }
    
    [Serializable]
    public class Texts
    {
        public string text;
        public float timeToShow;
    }
}
