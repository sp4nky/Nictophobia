using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DocumentData : ScriptableObject
{
    [TextArea(5,10)]
    public String title;
    [TextArea(15,50)]
    public String body;


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