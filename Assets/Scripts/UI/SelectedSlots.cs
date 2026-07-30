using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour
{
    public TMP_Text ingredientName;

    public void SetIngredient(IngredientData ingredient)
    {
        ingredientName.text = ingredient.ingredientName;

    }

    public void Clear()
    {
        ingredientName.text = "Empty";
    }
}