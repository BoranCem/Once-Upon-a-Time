using UnityEngine;

[CreateAssetMenu(fileName = "New Scene", menuName = "Bar Game/Scene")]
public class SceneData : ScriptableObject
{
    [Header("Characters")]
    public CharacterData[] characters;

    [Header("Dialogue Before Cocktail")]
    public DialogueData dialogueBefore;

    [Header("Dialogue After Cocktail")]
    public DialogueData dialogueAfter;
}