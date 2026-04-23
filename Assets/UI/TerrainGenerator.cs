using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int genWidth;
    [SerializeField] private int genHeight;
}

[CustomEditor(typeof(TerrainGenerator)), CanEditMultipleObjects]

public class TerrainGeneratorEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        //Create root
        VisualElement root = new VisualElement();

        //Load in UXML from path and attach to the root
        VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UXML/terrainGeneratorEditor.uxml");
        asset.CloneTree(root);

        //return the root
        return root;
    }
}
