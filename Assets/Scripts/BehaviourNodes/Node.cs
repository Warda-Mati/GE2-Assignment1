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

        set { _state = value; }
    }
    
    

    public abstract NodeState Evaluate();
}


public enum NodeState
{
    RUNNING,SUCCESS,FAILURE
}