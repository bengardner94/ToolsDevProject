using UnityEngine;

public class BehaviourTreeRun : MonoBehaviour
{
    public BTree tree;

    // Update is called once per frame
    void Update()
    {
        tree.Process();
    }
}
