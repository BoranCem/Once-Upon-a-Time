using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public CharacterData speaker;

    [TextArea(3,6)]
    public string text;
}