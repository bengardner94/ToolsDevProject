using UnityEditor;
using UnityEditor.UIElements;

[CustomEditor(typeof(BTree))]
[CanEditMultipleObjects]

public class BehaviourTreeEditor : Editor
{
    SerializedProperty treeListProperty;

    public void OnEnable()
    {
        treeListProperty = serializedObject.FindProperty("m_Nodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Foldout()
    }
}