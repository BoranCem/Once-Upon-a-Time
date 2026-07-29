using System.Collections.Generic;
using UnityEngine;

public class CocktailManager : MonoBehaviour
{
    [Header("Ingredients")]
    public List<IngredientData> ingredients = new();

    [Header("UI")]
    public Transform ingredientGrid;

    public IngredientButton ingredientButtonPrefab;

    private List<IngredientData> selectedIngredients = new();

    void Start()
    {
        CreateIngredientButtons();
    }

    private void CreateIngredientButtons()
    {
        foreach (IngredientData ingredient in ingredients)
        {
            IngredientButton button =
                Instantiate(ingredientButtonPrefab, ingredientGrid);

            button.Setup(ingredient, this);
        }
    }

    public void SelectIngredient(IngredientData ingredient)
    {
        if (selectedIngredients.Count >= 3)
        {
            Debug.Log("3 ingredient already selected.");
            return;
        }

        selectedIngredients.Add(ingredient);

        Debug.Log("Selected: " + ingredient.ingredientName);
    }
}