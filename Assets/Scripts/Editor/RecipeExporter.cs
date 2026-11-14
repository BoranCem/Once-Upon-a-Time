
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

public class RecipeExporter : EditorWindow
{
    [MenuItem("Tools/Cocktail Book/Export All Recipes")]
    public static void ExportRecipes()
    {
        string path = EditorUtility.SaveFilePanel(
            "Export Cocktail Recipes",
            "",
            "CocktailRecipes",
            "csv"
        );

        if (string.IsNullOrEmpty(path))
            return;

        StringBuilder csv = new StringBuilder();

        csv.AppendLine(
            "Recipe Name,Primary,Secondary,Third"
        );

        string[] guids = AssetDatabase.FindAssets("t:RecipeData");

        int count = 0;

        foreach (string guid in guids)
        {
            string assetPath =
                AssetDatabase.GUIDToAssetPath(guid);

            RecipeData recipe =
                AssetDatabase.LoadAssetAtPath<RecipeData>(
                    assetPath
                );

            if (recipe == null)
                continue;

            string recipeName = GetFieldValue(recipe, "recipeName")?.ToString() ?? "";

            string primary = GetIngredientName(GetFieldValue(recipe, "primary"));
            string secondary = GetIngredientName(GetFieldValue(recipe, "secondary"));
            string third = GetIngredientName(GetFieldValue(recipe, "third"));
            
            csv.AppendLine(
                Escape(recipeName) + "," +
                Escape(primary) + "," +
                Escape(secondary) + "," +
                Escape(third)
            );

            count++;
        }

        File.WriteAllText(
            path,
            "\uFEFF" + csv.ToString(),
            Encoding.UTF8
        );

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog(
            "Recipe Export Complete",
            count + " recipes exported successfully!",
            "OK"
        );

        Debug.Log(
            count + " recipes exported to: " + path
        );
    }

    private static object GetFieldValue(
        object target,
        string fieldName)
    {
        if (target == null)
            return null;

        FieldInfo field = target.GetType().GetField(
            fieldName,
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );

        return field != null
            ? field.GetValue(target)
            : null;
    }

    private static string GetIngredientName(object ingredient)
    {
        if (ingredient == null)
            return "";

        object name = GetFieldValue(
            ingredient,
            "ingredientName"
        );

        return name != null ? name.ToString() : "";
    }

    private static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
