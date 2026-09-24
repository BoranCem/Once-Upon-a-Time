using System.Collections.Generic;
using UnityEngine;

public class CocktailManager : MonoBehaviour
{
    [Header("Ingredients")]
    public List<IngredientData> ingredients = new();

    [Header("UI")]
    public Transform ingredientGrid;

    [Header("Selected Slots")]
    public SelectedSlot slot1;
    public SelectedSlot slot2;
    public SelectedSlot slot3;

    [Header("Recipes")]
    public List<RecipeData> recipes = new();

    [Header("Dialogue")]
    public DialogueManager dialogueManager;

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
            Debug.Log("Maximum ingredient selected.");
            return;
        }

        selectedIngredients.Add(ingredient);

        switch (selectedIngredients.Count)
        {
            case 1:
                slot1.SetIngredient(ingredient);
                break;

            case 2:
                slot2.SetIngredient(ingredient);
                break;

            case 3:
                slot3.SetIngredient(ingredient);
                break;
        }

        Debug.Log("Selected : " + ingredient.ingredientName);
    }

    public void MixDrink()
    {
        if (selectedIngredients.Count != 3)
        {
            Debug.Log("Please select 3 ingredients.");
            return;
        }

        RecipeData recipe = FindRecipe();

        float sweetness = CalculateProperty(i => i.sweetness);
        float sourness = CalculateProperty(i => i.sourness);
        float bitterness = CalculateProperty(i => i.bitterness);
        float strength = CalculateProperty(i => i.strength);
        float freshness = CalculateProperty(i => i.freshness);

        PropertyLevel sweetnessLevel = GetPropertyLevel(sweetness);
        PropertyLevel sournessLevel = GetPropertyLevel(sourness);
        PropertyLevel bitternessLevel = GetPropertyLevel(bitterness);
        PropertyLevel strengthLevel = GetPropertyLevel(strength);
        PropertyLevel freshnessLevel = GetPropertyLevel(freshness);

        bool propertiesMatch = CheckCustomerRequirements(
            sweetnessLevel,
            sournessLevel,
            bitternessLevel,
            strengthLevel,
            freshnessLevel
        );

        Debug.Log("----- Drink Properties -----");
        Debug.Log("Sweetness: " + sweetness + " (" + sweetnessLevel + ")");
        Debug.Log("Sourness: " + sourness + " (" + sournessLevel + ")");
        Debug.Log("Bitterness: " + bitterness + " (" + bitternessLevel + ")");
        Debug.Log("Strength: " + strength + " (" + strengthLevel + ")");
        Debug.Log("Freshness: " + freshness + " (" + freshnessLevel + ")");

        Debug.Log("Customer Requirements Match: " + propertiesMatch);

        if (recipe != null)
        {
            Debug.Log("Cocktail Created: " + recipe.recipeName);
        }
        else
        {
            Debug.Log("Unknown Cocktail");
        }

        if (propertiesMatch)
        {
            Debug.Log("Customer is satisfied with the drink.");
        }
        else
        {
            Debug.Log("Customer is NOT satisfied with the drink.");
        }

        ClearSelection();

        dialogueManager.CloseCocktailPanel();
        dialogueManager.ContinueDialogue();
    }

    private RecipeData FindRecipe()
    {
        foreach (RecipeData recipe in recipes)
        {
            if (recipe.primary == selectedIngredients[0] &&
                recipe.secondary == selectedIngredients[1] &&
                recipe.third == selectedIngredients[2])
            {
                return recipe;
            }
        }

        return null;
    }

    private float CalculateProperty(System.Func<IngredientData, int> selector)
    {
        return selector(selectedIngredients[0]) * 0.60f
             + selector(selectedIngredients[1]) * 0.25f
             + selector(selectedIngredients[2]) * 0.15f;
    }

    private PropertyLevel GetPropertyLevel(float value)
    {
        if (value <= 3f)
            return PropertyLevel.Low;

        if (value < 7f)
            return PropertyLevel.Medium;

        return PropertyLevel.High;
    }

    private bool CheckCustomerRequirements(
        PropertyLevel sweetness,
        PropertyLevel sourness,
        PropertyLevel bitterness,
        PropertyLevel strength,
        PropertyLevel freshness)
    {
        DialogueLine currentLine = dialogueManager.CurrentLine;

        if (currentLine.requiredSweetness != PropertyLevel.None &&
            currentLine.requiredSweetness != sweetness)
        {
            return false;
        }

        if (currentLine.requiredSourness != PropertyLevel.None &&
            currentLine.requiredSourness != sourness)
        {
            return false;
        }

        if (currentLine.requiredBitterness != PropertyLevel.None &&
            currentLine.requiredBitterness != bitterness)
        {
            return false;
        }

        if (currentLine.requiredStrength != PropertyLevel.None &&
            currentLine.requiredStrength != strength)
        {
            return false;
        }

        if (currentLine.requiredFreshness != PropertyLevel.None &&
            currentLine.requiredFreshness != freshness)
        {
            return false;
        }

        return true;
    }

    public void ClearSelection()
    {
        selectedIngredients.Clear();

        slot1.Clear();
        slot2.Clear();
        slot3.Clear();
    }
}