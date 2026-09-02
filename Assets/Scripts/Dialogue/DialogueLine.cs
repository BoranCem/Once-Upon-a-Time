using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public CharacterData speaker;

    [TextArea]
    public string text;

    public DialogueAction action;

    public RecipeData expectedRecipe;
}