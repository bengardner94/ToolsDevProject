using System;
using UnityEngine.UIElements;

[UxmlElement]
public partial class BehaviourTreeElement : VisualElement
{
    public BTree m_Tree;

    public virtual void Bind(BTree tree)
    {
        m_Tree = tree;
    }

    public virtual void Reset()
    {
        m_Tree = null;
    }
}