using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Bar Game/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;

    public IngredientData primary;

    public IngredientData secondary;

    public IngredientData third;
}