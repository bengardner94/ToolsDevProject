using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeItem : VisualElement
{
    public Action<BehaviourTreeItem> OnItemSelected;
    public int m_ID;
    public string m_Name;
    public BTNode m_Node;
    public BehaviourTreeView m_BTreeView;
    

    public BehaviourTreeItem(int ID, string name, BTNode node, BehaviourTreeView bTreeView)
    {
        m_ID = ID;
        m_Name = name;
        m_Node = node;
        m_BTreeView = bTreeView;
    }

    void CreateManipulator()
    {
        this.AddManipulator(new ContextualMenuManipulator((evt) =>
        {
            var actionTypes = TypeCache.GetTypesDerivedFrom<BTAction>();
            foreach (var type in actionTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => m_BTreeView.CreateNode(type));
            }

            var compositeTypes = TypeCache.GetTypesDerivedFrom<BTComposite>();
            foreach (var type in compositeTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => m_BTreeView.CreateNode(type));
            }

            var decoratorTypes = TypeCache.GetTypesDerivedFrom<BTDecorator>();
            foreach (var type in decoratorTypes)
            {
                evt.menu.AppendAction(type.Name, (x) => m_BTreeView.CreateNode(type));
            }

            evt.menu.AppendAction("Delete Node", (x) => m_BTreeView.DeleteNode());
        }));
    }
}