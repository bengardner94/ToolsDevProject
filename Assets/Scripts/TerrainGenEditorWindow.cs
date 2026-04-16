using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGenEditorWindow : EditorWindow
{
    [MenuItem("Terrain Generator/Main window")]
    public static void ShowWindow()
    {
        TerrainGenEditorWindow window = GetWindow<TerrainGenEditorWindow>();
        window.titleContent = new GUIContent("test");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy
        Label label = new Label("Hello World!");
        root.Add(label);

        // Create button
        Button button = new Button();
        button.name = "button";
        button.text = "Button";
        root.Add(button);

        // Create toggle
        Toggle toggle = new Toggle();
        toggle.name = "toggle";
        toggle.label = "Toggle";
        root.Add(toggle);
    }
}
