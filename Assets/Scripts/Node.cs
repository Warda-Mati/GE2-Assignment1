using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NodeState _state;

    public Node()
    {
        
    }

    public NodeState nodeState
    {
        get { return _state; }
    }

    public abstract NodeState Evaluate();
}


public enum NodeState
{
    SUCCESS,RUNNING,FAILURE,
}