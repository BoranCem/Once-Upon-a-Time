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
        Debug.Log("Select 3 ingredients first!");
        return;
    }

    Debug.Log("Mixing drink...");
}

}