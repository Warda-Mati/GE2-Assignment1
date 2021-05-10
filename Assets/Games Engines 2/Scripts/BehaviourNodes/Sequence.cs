using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> childNodes = new List<Node>();

    public Sequence(List<Node>childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        bool node_running = false;
        foreach (Node node in childNodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    _state = NodeState.FAILURE;
                    return _state;
                case NodeState.SUCCESS:
                    break;
                case NodeState.RUNNING:
                    node_running = true;
                    break;
                default:
                    _state = NodeState.RUNNING;
                    return _state;
            }
        }

        _state = node_running ? NodeState.RUNNING : NodeState.SUCCESS;
        return _state;
    }
}
