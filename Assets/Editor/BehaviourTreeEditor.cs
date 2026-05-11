using Codice.CM.Common.Update.Partial;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    BehaviourTreeView bTreeView;
    VisualElement testChild1;
    Label descriptionLabel;
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
        TwoPaneSplitView splitView2 = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Vertical);


        descriptionLabel = new Label();
        testChild1 = new VisualElement();
        bTreeView = new BehaviourTreeView();

        root.Add(splitView);

        splitView.Add(splitView2);
        splitView.Add(bTreeView);

        splitView2.Add(descriptionLabel);
        splitView2.Add(testChild1);

        newTree.Initialize();

        //BTInfRepeater treeRoot = ScriptableObject.CreateInstance<BTInfRepeater>();

        //BTree newTree = ScriptableObject.CreateInstance<BTree>();
        BTRootNode treeRoot = newTree.CreateNode(typeof(BTRootNode), null) as BTRootNode;
        newTree.SetRoot(treeRoot);

        BehaviourTreeItem rootNode = bTreeView.CreateTreeView(newTree);

        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

        bTreeView.SetRootItems(nodesList);

        bTreeView.makeItem = () => new Label();

        bTreeView.bindItem = (VisualElement element, int index) =>
        {
            (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
            CreateManipulator(element);
        };

        bTreeView.destroyItem = (VisualElement element) =>
        {
            element = null;
        };

        bTreeView.selectionChanged += UpdateSelection;
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

            evt.menu.AppendAction("Delete Node", (x) => RemoveNode(element));
        }));
    }

    public void CreateNode(System.Type type)
    {
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        BTAction testAction = selectedNode.m_Node as BTAction;
        if (testAction != null)
        {
            descriptionLabel.text = ("Action nodes cannot have children");
        }
        else
        {
            BTDecorator testDecorator = selectedNode.m_Node as BTDecorator;
            BTRootNode testRoot = selectedNode.m_Node as BTRootNode;
            if ((testRoot != null || testDecorator != null) && selectedNode.m_Node.m_Children.Count == 1)
            {
                descriptionLabel.text = ("too many children");

            }
            else
            {
                BTNode newNode = newTree.CreateNode(type, selectedNode.m_Node);
                int newItemID = selectedNode.m_ID + IDAdd;
                BehaviourTreeItem newItem = new BehaviourTreeItem(newItemID, type.Name, newNode, selectedNode);
                TreeViewItemData<BehaviourTreeItem> newTreeItem = new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem);
                nodesList.Add(newTreeItem);
                bTreeView.AddItem<BehaviourTreeItem>(new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem), selectedNode.m_ID, (nodesList.IndexOf(newTreeItem)), false);
                IDAdd++; ;
                selectedNode.m_ChildList++;
                bTreeView.Rebuild();
            }
        }
    }

    public void RemoveNode(VisualElement element)
    {
        Debug.Log(element.ToString());
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        BTRootNode testForRoot = selectedNode.m_Node as BTRootNode;
        if (testForRoot != null)
        {
            descriptionLabel.text = ("Cannot delete root node");
        }
        else
        {
            bTreeView.TryRemoveItem(selectedNode.m_ID);
            bTreeView.Rebuild();

            newTree.RemoveNode(selectedNode.m_Node, selectedNode.m_Parent.m_Node);
        }
    }

    public void UpdateSelection(IEnumerable<object> testItem)
    {
        if (bTreeView.selectedItem != null)
            descriptionLabel.text = ((bTreeView.selectedItem as BehaviourTreeItem).m_Node.GetDescription());
    }
}
