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
        foreach (Node node in childNodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    _state = NodeState.SUCCESS;
                    return _state;
                case NodeState.RUNNING:
                    _state = NodeState.RUNNING;
                    return _state;
                default:
                    continue;
            }
        }

        _state = NodeState.FAILURE;
        return _state;
    }
}
