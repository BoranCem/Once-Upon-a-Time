using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktailBookEntry : MonoBehaviour
{
    [Header("UI")]
    public Image cocktailIcon;
    public TMP_Text recipeName;
    public Button button;

    [Header("Unknown")]
    public Sprite unknownIcon;

    private RecipeData recipe;
    private bool discovered;
    private CocktailBookManager bookManager;

    public void Setup(
        RecipeData newRecipe,
        bool isDiscovered,
        CocktailBookManager manager)
    {
        recipe = newRecipe;
        discovered = isDiscovered;
        bookManager = manager;

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

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        bookManager.SelectRecipe(recipe);
    }
}