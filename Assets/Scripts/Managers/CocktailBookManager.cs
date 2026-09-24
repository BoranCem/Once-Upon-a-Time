using System.Collections.Generic;
using UnityEngine;

public class CocktailBookManager : MonoBehaviour
{
    public List<RecipeData> discoveredRecipes = new();

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
}