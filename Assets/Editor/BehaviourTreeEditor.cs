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
    VisualElement inspectorView;
    Label descriptionLabel;
    BTree newTree;
    int IDAdd = 1;

    List<TreeViewItemData<BehaviourTreeItem>> nodesList = new List<TreeViewItemData<BehaviourTreeItem>>();

    [MenuItem("Window/UI Toolkit/BehaviourTreeEditor")]
    public static void ShowExample()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        //Gets the current selected asset and sets it to a behaviour tree
        newTree = Selection.activeObject as BTree;
        VisualElement root = rootVisualElement;

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        TwoPaneSplitView splitView2 = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Vertical);


        //Sets the layout of the editor window
        descriptionLabel = new Label();
        inspectorView = new VisualElement();
        bTreeView = new BehaviourTreeView();

        root.Add(splitView);

        splitView.Add(splitView2);
        splitView.Add(bTreeView);

        splitView2.Add(descriptionLabel);
        splitView2.Add(inspectorView);

        descriptionLabel.text = ("Right click on the root node to begin adding nodes");


        //Clears the tree's list of nodes + removes all nodes from the asset
        newTree.Initialize();

        //Creates a new root node that acts as the root of both the tree view and the behaviour tree
        BTRootNode treeRoot = newTree.CreateNode(typeof(BTRootNode), null) as BTRootNode;
        newTree.SetRoot(treeRoot);

        BehaviourTreeItem rootNode = bTreeView.CreateTreeView(newTree);

        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootNode.m_ID, rootNode));

        bTreeView.SetRootItems(nodesList);


        //Sets each new element to a label and sets the text depending on the name of the node
        bTreeView.makeItem = () => new Label();

        bTreeView.bindItem = (VisualElement element, int index) =>
        {
            (element as Label).text = bTreeView.GetItemDataForIndex<BehaviourTreeItem>(index).m_Name;
            CreateManipulator(element);
        };

        //called when the node is deleted
        bTreeView.destroyItem = (VisualElement element) =>
        {
            element = null;
        };

        //called when the player selects another node
        bTreeView.selectionChanged += UpdateSelection;
    }

    void CreateManipulator(VisualElement element)
    {
        //gets all types derived from the three abstract classes and appends it to the contextual menu
        element.AddManipulator(new ContextualMenuManipulator((evt) =>
        {
            var actionTypes = TypeCache.GetTypesDerivedFrom<BTAction>();
            foreach (var type in actionTypes)
            {
                evt.menu.AppendAction("Action Node: " + type.Name, (x) => CreateNode(type));
            }

            var compositeTypes = TypeCache.GetTypesDerivedFrom<BTComposite>();
            foreach (var type in compositeTypes)
            {
                evt.menu.AppendAction("Composite Node: "+ type.Name, (x) => CreateNode(type));
            }

            var decoratorTypes = TypeCache.GetTypesDerivedFrom<BTDecorator>();
            foreach (var type in decoratorTypes)
            {
                evt.menu.AppendAction("Decorator Node: " + type.Name, (x) => CreateNode(type));
            }

            evt.menu.AppendAction("Delete Node", (x) => RemoveNode());
        }));
    }

    public void CreateNode(System.Type type)
    {
        //gets the selected node and ensures that adding a new child would not cause issues
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        BTAction testAction = selectedNode.m_Node as BTAction;
        if (testAction != null)
        {
            descriptionLabel.text = ("ERROR: Action nodes cannot have children");
        }
        else
        {
            BTDecorator testDecorator = selectedNode.m_Node as BTDecorator;
            BTRootNode testRoot = selectedNode.m_Node as BTRootNode;
            if ((testRoot != null || testDecorator != null) && selectedNode.m_Node.m_Children.Count == 1)
            {
                descriptionLabel.text = ("ERROR: Decorator nodes can only have one child");

            }
            else
            {
                //Creates a new node, assigns it a unique ID and then adds it to the tree view by converting it into TreeViewItemData
                BTNode newNode = newTree.CreateNode(type, selectedNode.m_Node);
                int newItemID = selectedNode.m_ID + IDAdd;
                BehaviourTreeItem newItem = new BehaviourTreeItem(newItemID, type.Name, newNode, selectedNode);
                TreeViewItemData<BehaviourTreeItem> newTreeItem = new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem);

                nodesList.Add(newTreeItem);
                bTreeView.AddItem<BehaviourTreeItem>(new TreeViewItemData<BehaviourTreeItem>(newItem.m_ID, newItem), selectedNode.m_ID, (nodesList.IndexOf(newTreeItem)), false);
                IDAdd++;
                selectedNode.m_ChildList++;
                bTreeView.Rebuild();
            }
        }
    }

    public void RemoveNode()
    {
        //Tests if the selected object is not a root node
        BehaviourTreeItem selectedNode = bTreeView.selectedItem as BehaviourTreeItem;
        BTRootNode testForRoot = selectedNode.m_Node as BTRootNode;
        if (testForRoot != null)
        {
            descriptionLabel.text = ("ERROR: Cannot delete root node");
        }
        else
        {
            //Removes the node from both the tree view and the behaviour tree
            bTreeView.TryRemoveItem(selectedNode.m_ID);
            bTreeView.Rebuild();

            newTree.RemoveNode(selectedNode.m_Node, selectedNode.m_Parent.m_Node);
        }
    }

    public void UpdateSelection(IEnumerable<object> testItem)
    {
        inspectorView.Clear();
        if (bTreeView.selectedItem != null)
        {
            //Updates the label to be the description of the node, creates a new inspector window and adds it to the VisualElement 
            descriptionLabel.text = ((bTreeView.selectedItem as BehaviourTreeItem).m_Node.GetDescription());
            Editor editor = Editor.CreateEditor((bTreeView.selectedItem as BehaviourTreeItem).m_Node);
            IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
            inspectorView.Add(container);
        }
            
    }
}
