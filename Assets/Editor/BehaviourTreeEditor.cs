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
    int IDAdd = 1;
    private VisualTreeAsset m_VisualTreeAsset = default;

    List<TreeViewItemData<BehaviourTreeItem>> nodesList = new List<TreeViewItemData<BehaviourTreeItem>>();
    List<Label> labelList = new List<Label>();

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
        BTRootNode treeRoot = newTree.CreateNode(typeof(BTRootNode)) as BTRootNode;
        newTree.SetRoot(treeRoot);

        BehaviourTreeItem rootNode = bTreeView.CreateTreeView(newTree);

        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

        bTreeView.SetRootItems(nodesList);

        bTreeView.makeItem = () => new Label();

        bTreeView.bindItem = (VisualElement element, int index) =>
        {
            Debug.Log(element.ToString());
            (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
            CreateManipulator(element, index);
        };

        bTreeView.destroyItem = (VisualElement element) =>
        {
            Debug.Log(element.ToString());
            element = null;
        };

        bTreeView.unbindItem = (VisualElement element, int index) =>
        {
        };      
        //CreateManipulator(bTreeView.GetItemDataForIndex<BehaviourTreeItem>(0));
    }

    void CreateManipulator(VisualElement element, int index)
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

            evt.menu.AppendAction("Delete Node", (x) => RemoveNode(element, index));
        }));
    }

    public void CreateNode(System.Type type)
    {
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        BTNode newNode = newTree.CreateNode(type);
        int newItemID = selectedNode.m_ID + IDAdd;
        BehaviourTreeItem newItem = new BehaviourTreeItem(newItemID, type.Name, newNode);
        TreeViewItemData<BehaviourTreeItem> newTreeItem = new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem);
        nodesList.Add(newTreeItem);
        //Debug.Log(nodesList.IndexOf(newTreeItem));
        bTreeView.AddItem<BehaviourTreeItem>(new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem), selectedNode.m_ID, (nodesList.IndexOf(newTreeItem)), false);
        IDAdd++;
        //bTreeView.SetRootItems(nodesList);
        bTreeView.Rebuild();
    }

    public void RemoveNode(VisualElement element, int index)
    {
        Debug.Log(element.ToString());
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;

        bTreeView.TryRemoveItem(selectedNode.m_ID);
        bTreeView.Rebuild();

        newTree.RemoveNode(selectedNode.m_Node);
    }
}
