using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    List<TreeViewItemData<BehaviourTreeItem>> nodesList = new List<TreeViewItemData<BehaviourTreeItem>>();

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

        VisualElement testChild1 = new VisualElement();
        BehaviourTreeView bTreeView = new BehaviourTreeView();

        root.Add(splitView);

        splitView.Add(testChild1);
        splitView.Add(bTreeView);

        BTInfRepeater treeRoot = ScriptableObject.CreateInstance<BTInfRepeater>();

        BTree tree = ScriptableObject.CreateInstance<BTree>();

        tree.SetRoot(treeRoot);

        BehaviourTreeItem rootNode = bTreeView.CreateTreeView(tree);

        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

        bTreeView.SetRootItems(nodesList);

        bTreeView.makeItem = () => new Label();

        bTreeView.bindItem = (VisualElement element, int index) => (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;

        VisualElement label = new Label("test2");
        testChild1.Add(label);
        CreateManipulator(label);

        //CreateManipulator(bTreeView.GetItemDataForIndex<BehaviourTreeItem>(0));
    }

    void CreateManipulator(VisualElement element)
    {
        element.AddManipulator(new ContextualMenuManipulator((evt) =>
        {
            var actionTypes = TypeCache.GetTypesDerivedFrom<BTAction>();
            foreach (var type in actionTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => Debug.Log("action made"));
            }

            var compositeTypes = TypeCache.GetTypesDerivedFrom<BTComposite>();
            foreach (var type in compositeTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => Debug.Log("composite made"));
            }

            var decoratorTypes = TypeCache.GetTypesDerivedFrom<BTDecorator>();
            foreach (var type in decoratorTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => Debug.Log("decorator made"));
            }

            evt.menu.AppendAction("Delete Node", (x) => Debug.Log("node deleted"));
        }));
    }
}
