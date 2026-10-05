using System.Collections.Generic;
using UnityEngine;

public class CocktailBookManager : MonoBehaviour
{
    [Header("Recipes")]
    public List<RecipeData> allRecipes = new();

    public List<RecipeData> discoveredRecipes = new();

    [Header("UI")]
    public Transform recipeGrid;
    public CocktailBookEntry recipeEntryPrefab;

    public void DiscoverRecipe(RecipeData recipe)
    {
        if (recipe == null)
            return;

        if (!discoveredRecipes.Contains(recipe))
        {
            discoveredRecipes.Add(recipe);

            Debug.Log("Cocktail discovered: " + recipe.recipeName);
        }
    }

    public bool IsDiscovered(RecipeData recipe)
    {
        return discoveredRecipes.Contains(recipe);
    }

    public void OpenBook()
    {
        CreateRecipeEntries();
    }

    private void CreateRecipeEntries()
    {
        foreach (Transform child in recipeGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (RecipeData recipe in allRecipes)
        {
            CocktailBookEntry entry =
                Instantiate(recipeEntryPrefab, recipeGrid);

            entry.Setup(recipe, IsDiscovered(recipe));
        }
    }
}