using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public TMP_Text ingredientName;

    private IngredientData ingredient;
    private CocktailManager cocktailManager;

    public void Setup(IngredientData newIngredient, CocktailManager manager)
    {
        ingredient = newIngredient;
        cocktailManager = manager;

        ingredientName.text = ingredient.ingredientName;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        cocktailManager.SelectIngredient(ingredient);
    }
}