using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Tutorial))]
public class TutorialEditor : PropertyDrawer
{
    private const float TextAreaHeight = 60.0f;
    private const float Padding = 5.0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);


        float singleLineHeight = EditorGUIUtility.singleLineHeight;
        float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

        Rect labelRect = new Rect(position.x, position.y, position.width, singleLineHeight);
        EditorGUI.LabelField(labelRect, label);

        float currentY = position.y + singleLineHeight + verticalSpacing;

        SerializedProperty textProp = property.FindPropertyRelative("TutorialText");
        Rect textRect = new Rect(position.x, currentY, position.width, TextAreaHeight);

        textProp.stringValue = EditorGUI.TextArea(textRect, textProp.stringValue, EditorStyles.textArea);

        currentY += TextAreaHeight + verticalSpacing;

        SerializedProperty startEventProp = property.FindPropertyRelative("OnTutorialStart");
        float startEventHeight = EditorGUI.GetPropertyHeight(startEventProp);
        Rect startEventRect = new Rect(position.x, currentY, position.width, startEventHeight);
        EditorGUI.PropertyField(startEventRect, startEventProp);

        currentY += startEventHeight + verticalSpacing;

        SerializedProperty finishEventProp = property.FindPropertyRelative("OnTutorialFinish");
        float finishEventHeight = EditorGUI.GetPropertyHeight(finishEventProp);
        Rect finishEventRect = new Rect(position.x, currentY, position.width, finishEventHeight);
        EditorGUI.PropertyField(finishEventRect, finishEventProp);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = 0f;

        totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        totalHeight += TextAreaHeight + EditorGUIUtility.standardVerticalSpacing;

        SerializedProperty startEventProp = property.FindPropertyRelative("OnTutorialStart");
        SerializedProperty finishEventProp = property.FindPropertyRelative("OnTutorialFinish");

        totalHeight += EditorGUI.GetPropertyHeight(startEventProp) + EditorGUIUtility.standardVerticalSpacing;
        totalHeight += EditorGUI.GetPropertyHeight(finishEventProp) + EditorGUIUtility.standardVerticalSpacing;

        return totalHeight + Padding;
    }
}
