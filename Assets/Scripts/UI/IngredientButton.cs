
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public TMP_Text ingredientName;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color darkColor = new Color(0.3f, 0.3f, 0.3f);

    private IngredientData ingredient;
    private CocktailManager cocktailManager;
    private Button button;
    private Image buttonImage;

    public void Setup(IngredientData newIngredient, CocktailManager manager)
    {
        ingredient = newIngredient;
        cocktailManager = manager;

        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        ingredientName.text = ingredient.ingredientName;

        button.onClick.RemoveListener(OnClick);
        button.onClick.AddListener(OnClick);

        UpdateAppearance();
    }

    private void OnClick()
    {
        cocktailManager.SelectIngredient(ingredient);
    }

    public void UpdateAppearance()
    {
        bool canSelect =
            ingredient.isAlcoholic ||
            cocktailManager.HasSelectedAlcohol();

        if (button != null)
            button.interactable = canSelect;

        if (buttonImage != null)
            buttonImage.color = canSelect ? normalColor : darkColor;

        if (ingredientName != null)
            ingredientName.color = canSelect ? Color.white : Color.gray;
    }
}
