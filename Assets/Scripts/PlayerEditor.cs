using UnityEngine;
using UnityEditor;
using Unity.Properties;

[CustomEditor(typeof(Player))]

public class PlayerEditor : Editor
{

    private SerializedProperty healthProperty;
    private SerializedProperty speedProperty;
    private SerializedProperty listProperty;

    private void OnEnable()
    {
        healthProperty = serializedObject.FindProperty("health");
        speedProperty = serializedObject.FindProperty("speed");
        listProperty = serializedObject.FindProperty("myList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(healthProperty, 0f, 100f);
        EditorGUILayout.Slider(speedProperty, 0f, 25f);

        EditorGUILayout.LabelField($"hp - {healthProperty.floatValue}, speed - {speedProperty.floatValue}");

        serializedObject.ApplyModifiedProperties();
    }


}
