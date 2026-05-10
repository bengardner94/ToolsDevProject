using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    BehaviourTreeView bTreeView;
    VisualElement testChild1;
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
        BTree newTree = Selection.activeObject as BTree;
        VisualElement root = rootVisualElement;

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        testChild1 = new VisualElement();
        bTreeView = new BehaviourTreeView();

        root.Add(splitView);

        splitView.Add(testChild1);
        splitView.Add(bTreeView);

        //BTInfRepeater treeRoot = ScriptableObject.CreateInstance<BTInfRepeater>();

        //BTree newTree = ScriptableObject.CreateInstance<BTree>();

        if (newTree.m_Nodes != null)
        {
            Debug.Log("hello");
            BTInfRepeater treeRoot = newTree.CreateNode(typeof(BTInfRepeater)) as BTInfRepeater;
            newTree.SetRoot(treeRoot);

            BehaviourTreeItem rootNode = bTreeView.CreateTreeView(newTree);

            nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

            bTreeView.SetRootItems(nodesList);

            VisualElement label = new Label();

            bTreeView.makeItem = () => label;

            bTreeView.bindItem = (VisualElement element, int index) => (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
            CreateManipulator(label);
        }
        //CreateManipulator(bTreeView.GetItemDataForIndex<BehaviourTreeItem>(0));
    }

    void CreateManipulator(VisualElement element)
    {
        element.AddManipulator(new ContextualMenuManipulator((evt) =>
        {
            var actionTypes = TypeCache.GetTypesDerivedFrom<BTAction>();
            foreach (var type in actionTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => CreateNode(type));
            }

            var compositeTypes = TypeCache.GetTypesDerivedFrom<BTComposite>();
            foreach (var type in compositeTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => CreateNode(type));
            }

            var decoratorTypes = TypeCache.GetTypesDerivedFrom<BTDecorator>();
            foreach (var type in decoratorTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => CreateNode(type));
            }

            evt.menu.AppendAction("Delete Node", (x) => RemoveNode());
        }));
    }

    public void CreateNode(System.Type type)
    {
        Debug.Log("node made");
    }

    public void RemoveNode()
    {
        //BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;

        //newTree.RemoveNode(selectedNode.m_Node);
    }
}
