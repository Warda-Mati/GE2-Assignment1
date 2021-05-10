using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QueueNode : IComparable<QueueNode>
{
    public Vector3 pos;
    public float f, g, h;
    public QueueNode parent;

    public int CompareTo(QueueNode b)
    {
        if ((this.f < b.f))
        {
            return -1;
        }
        if (this.f == b.f)
        {
            return 0;
        }
        return 1;
    }
}