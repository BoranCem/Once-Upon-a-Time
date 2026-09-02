using System;
using UnityEngine;

public enum PropertyLevel
{
    Low,
    Medium,
    High
}

[Serializable]
public class DialogueLine
{
    public CharacterData speaker;

    [TextArea]
    public string text;

    public DialogueAction action;

    [Header("Customer Drink Requirements")]
    public PropertyLevel requiredSweetness;
    public PropertyLevel requiredSourness;
    public PropertyLevel requiredBitterness;
    public PropertyLevel requiredStrength;
    public PropertyLevel requiredFreshness;
}