using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string speaker;

    [TextArea(3,6)]
    public string text;
}