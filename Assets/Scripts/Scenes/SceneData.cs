using UnityEngine;

[CreateAssetMenu(fileName = "New Scene", menuName = "Bar Game/Scene")]
public class SceneData : ScriptableObject
{
    [Header("Characters In Scene")]
    public CharacterData[] characters;

    [Header("Dialogue")]
    public DialogueData dialogue;
}