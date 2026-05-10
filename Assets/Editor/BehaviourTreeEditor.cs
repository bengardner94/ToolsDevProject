using Codice.CM.Common.Update.Partial;
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
    BTree newTree;
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
        var visualTreeAsset = EditorGUIUtility.Load("Assets/Editor/BehaviourTreeEditor") as VisualTreeAsset;
        newTree = Selection.activeObject as BTree;
        VisualElement root = rootVisualElement;

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        testChild1 = new VisualElement();
        bTreeView = new BehaviourTreeView();

        root.Add(splitView);

        splitView.Add(testChild1);
        splitView.Add(bTreeView);

        newTree.Initialize();

        //BTInfRepeater treeRoot = ScriptableObject.CreateInstance<BTInfRepeater>();

        //BTree newTree = ScriptableObject.CreateInstance<BTree>();
        Debug.Log("hello");
        BTRootNode treeRoot = newTree.CreateNode(typeof(BTRootNode)) as BTRootNode;
        newTree.SetRoot(treeRoot);

        BehaviourTreeItem rootNode = bTreeView.CreateTreeView(newTree);

        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

        bTreeView.SetRootItems(nodesList);

        VisualElement label = new Label();

        bTreeView.makeItem = () => label;

        bTreeView.bindItem = (VisualElement element, int index) =>
        {
            Debug.Log("running");
            (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
        };
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
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        Debug.Log(selectedNode.m_ID);
        BTNode newNode = newTree.CreateNode(type);
        //Debug.Log(selectedNode.m_ID);
        int newItemIndex = selectedNode.m_ID + 1;
        BehaviourTreeItem newItem = new BehaviourTreeItem(newItemIndex, type.Name, newNode);
        TreeViewItemData<BehaviourTreeItem> newTreeItem = new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem);
        nodesList.Add(newTreeItem);
        //Debug.Log(nodesList.IndexOf(newTreeItem));
        //Debug.Log(selectedNode.m_ID);
        bTreeView.AddItem<BehaviourTreeItem>(new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem), selectedNode.m_ID, (nodesList.IndexOf(newTreeItem)), false);
        //bTreeView.SetRootItems(nodesList);
        bTreeView.Rebuild();
    }

    public void RemoveNode()
    {
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        bTreeView.Remove(selectedNode);

        newTree.RemoveNode(selectedNode.m_Node);
    }
}
