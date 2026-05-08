using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/BehaviourTreeEditor")]
    public static void ShowExample()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        root.Add(splitView);

        VisualElement testChild1 = new VisualElement();
        BehaviourTreeView bTreeView = new BehaviourTreeView();

        splitView.Add(testChild1);
        splitView.Add(bTreeView);

        BTInfRepeater treeRoot = ScriptableObject.CreateInstance<BTInfRepeater>();

        BTree tree = ScriptableObject.CreateInstance<BTree>();

        tree.SetRoot(treeRoot);

        bTreeView.SetRootItems(bTreeView.CreateTreeView(tree));

        bTreeView.makeItem = () => new Label();

        bTreeView.bindItem = (VisualElement element, int index) => (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
    }
}
