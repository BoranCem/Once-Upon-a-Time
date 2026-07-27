using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    public Sprite portrait;

    public Color nameColor = Color.white;
}