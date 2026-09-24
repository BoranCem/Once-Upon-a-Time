using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Bar Game/Recipe")]
public class RecipeData : ScriptableObject
{
    [Header("Recipe Information")]
    public string recipeName;

    public Sprite cocktailIcon;

    [TextArea(3, 8)]
    public string cocktailStory;

    [Header("Ingredients")]
    public IngredientData primary;
    public IngredientData secondary;
    public IngredientData third;
}