using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Bar Game/Ingredient")]
public class IngredientData : ScriptableObject
{
    [Header("Information")]
    public string ingredientName;

    public Sprite icon;

    [Header("Taste")]
    [Range(0,10)] public int sweetness;

    [Range(0,10)] public int sourness;

    [Range(0,10)] public int bitterness;

    [Range(0,10)] public int strength;

    [Range(0,10)] public int freshness;
}