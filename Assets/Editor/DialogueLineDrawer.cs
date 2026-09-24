using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueLine))]
public class DialogueLineDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 2f;

        SerializedProperty speaker =
            property.FindPropertyRelative("speaker");

        SerializedProperty text =
            property.FindPropertyRelative("text");

        SerializedProperty action =
            property.FindPropertyRelative("action");

        SerializedProperty requiredSweetness =
            property.FindPropertyRelative("requiredSweetness");

        SerializedProperty requiredSourness =
            property.FindPropertyRelative("requiredSourness");

        SerializedProperty requiredBitterness =
            property.FindPropertyRelative("requiredBitterness");

        SerializedProperty requiredStrength =
            property.FindPropertyRelative("requiredStrength");

        SerializedProperty requiredFreshness =
            property.FindPropertyRelative("requiredFreshness");

        float y = position.y;

        // Speaker
        EditorGUI.PropertyField(
            new Rect(position.x, y, position.width, lineHeight),
            speaker);

        y += lineHeight + spacing;

        // Text
        float textHeight = EditorGUI.GetPropertyHeight(text);

        EditorGUI.PropertyField(
            new Rect(position.x, y, position.width, textHeight),
            text);

        y += textHeight + spacing;

        // Action
        EditorGUI.PropertyField(
            new Rect(position.x, y, position.width, lineHeight),
            action);

        y += lineHeight + spacing;

        // Show requirements only for WaitForCocktail
        if ((DialogueAction)action.enumValueIndex ==
            DialogueAction.WaitForCocktail)
        {
            EditorGUI.LabelField(
                new Rect(position.x, y, position.width, lineHeight),
                "Customer Drink Requirements",
                EditorStyles.boldLabel);

            y += lineHeight + spacing;

            EditorGUI.PropertyField(
                new Rect(position.x, y, position.width, lineHeight),
                requiredSweetness);

            y += lineHeight + spacing;

            EditorGUI.PropertyField(
                new Rect(position.x, y, position.width, lineHeight),
                requiredSourness);

            y += lineHeight + spacing;

            EditorGUI.PropertyField(
                new Rect(position.x, y, position.width, lineHeight),
                requiredBitterness);

            y += lineHeight + spacing;

            EditorGUI.PropertyField(
                new Rect(position.x, y, position.width, lineHeight),
                requiredStrength);

            y += lineHeight + spacing;

            EditorGUI.PropertyField(
                new Rect(position.x, y, position.width, lineHeight),
                requiredFreshness);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 2f;

        SerializedProperty text =
            property.FindPropertyRelative("text");

        SerializedProperty action =
            property.FindPropertyRelative("action");

        float height = 0f;

        // Speaker
        height += lineHeight + spacing;

        // Text
        height += EditorGUI.GetPropertyHeight(text) + spacing;

        // Action
        height += lineHeight + spacing;

        // Requirements
        if ((DialogueAction)action.enumValueIndex ==
            DialogueAction.WaitForCocktail)
        {
            // Header
            height += lineHeight + spacing;

            // 5 properties
            height += (lineHeight + spacing) * 5;
        }

        return height;
    }
}