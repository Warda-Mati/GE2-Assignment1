using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    private Node child;

    public Node node
    {
        get { return child; }
    }

    public Inverter(Node node)
    {
        child = node;
    }

    public override NodeState Evaluate()
    {
        switch (child.Evaluate())
        {
            case NodeState.FAILURE:
                _state = NodeState.SUCCESS;
                return _state;
            case NodeState.SUCCESS:
                _state = NodeState.FAILURE;
                return _state;
            case NodeState.RUNNING:
                _state = NodeState.RUNNING;
                return _state;
        }
        _state = NodeState.SUCCESS;
        return _state;
    }

       
    
}