using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktailBookDetail : MonoBehaviour
{
    [Header("UI")]
    public Image cocktailImage;
    public TMP_Text cocktailName;
    public TMP_Text cocktailStory;
    public TMP_Text ingredientsText;

    public void ShowRecipe(RecipeData recipe)
    {
        if (recipe == null)
            return;

        cocktailImage.sprite = recipe.cocktailIcon;
        cocktailName.text = recipe.recipeName;
        cocktailStory.text = recipe.cocktailStory;

        ingredientsText.text =
            "• " + recipe.primary.ingredientName + "\n" +
            "• " + recipe.secondary.ingredientName + "\n" +
            "• " + recipe.third.ingredientName;
    }

    public void ShowUnknown()
    {
        cocktailImage.sprite = null;
        cocktailName.text = "???";
        cocktailStory.text = "This cocktail has not been discovered yet.";
        ingredientsText.text = "???";
    }
}