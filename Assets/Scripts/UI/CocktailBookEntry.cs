using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktailBookEntry : MonoBehaviour
{
    public Image cocktailIcon;
    public TMP_Text recipeName;

    public Sprite unknownIcon;

    public void Setup(RecipeData recipe, bool discovered)
    {
        if (discovered)
        {
            cocktailIcon.sprite = recipe.cocktailIcon;
            recipeName.text = recipe.recipeName;
        }
        else
        {
            cocktailIcon.sprite = unknownIcon;
            recipeName.text = "???";
        }
    }
}