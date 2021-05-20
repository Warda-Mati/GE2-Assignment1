using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
    
public class PathFinder : MonoBehaviour
{
    
    
    public float gridSize = 5.0f; 
    public string message = "";    
    public bool isThreeD = false;
    
    Dictionary<Vector3, QueueNode> open = new Dictionary<Vector3, QueueNode>(20000);
    PriorityQueue<QueueNode> openPQ = new PriorityQueue<QueueNode>();

    Dictionary<Vector3, QueueNode> closed = new Dictionary<Vector3, QueueNode>(20000);
   
    Vector3 startPos, endPos;

    public Transform start, end;

    public bool smooth = false;

    public bool usePQ = true;

    public void OnDrawGizmos()
    {
        if (! Application.isPlaying)
        {
            FindFishPath(start.position, end.position);    
        }
    }

    public void Start()
    {
        FindFishPath(start.position, end.position);
    }

    Vector3 PositionToVoxel(Vector3 v)
    {
        Vector3 ret = new Vector3();
        ret.x = ((int)(v.x / gridSize)) * gridSize;
        ret.y = ((int)(v.y / gridSize)) * gridSize;
        ret.z = ((int)(v.z / gridSize)) * gridSize;
        return ret;
    }

    public FishPath FindFishPath(Vector3 start, Vector3 end)
    {
        long oldNow = DateTime.Now.Ticks;
        bool found = false;
        
   
        this.endPos = PositionToVoxel(start); // end refers to start
        this.startPos = PositionToVoxel(end); // start refers to end

        // all lists are empited
        open.Clear();
        closed.Clear();
        openPQ.Clear();

        // creates first QueueNode, all values are sent to 0,
        // and add it to the open list
        QueueNode first = new QueueNode();
        first.f = first.g = first.h = 0.0f;
        first.pos = this.startPos;
        open[this.startPos] = first;
        openPQ.Enqueue(first);

        QueueNode current = first;
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        int maxSize = 0;
        stopwatch.Start();
        bool timeout = false;
        
        // while there is a QueueNode in the open list
        while (open.Count > 0)
        {
            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                timeout = true;
                break;
            }
            if (open.Count > maxSize)
            {
                maxSize = open.Count;
            }

            if (usePQ)
            {
                // remove the QueueNode we are on from the open list queue
                current = openPQ.Dequeue();
            }
            else
            { 
                // not using PQ hence can ignore for exam
                // Get the top of the q
                float min = float.MaxValue;
                foreach (QueueNode QueueNode in open.Values)
                {
                    if (QueueNode.f < min)
                    {
                        current = QueueNode;
                        min = QueueNode.f;
                    }
                }
            }
            // check if the current QueueNode is the end QueueNode, if so it is found
            if (current.pos.Equals(this.endPos))
            {
                found = true;
                break;
            }
            // add all adjancent QueueNodes to current
            addAdjacentQueueNodes(current);
            // removing it from the open list
            open.Remove(current.pos);
            // adding it to the closed list
            closed[current.pos] = current;
        }
        // if found, make a FishPath. 
        FishPath FishPath = GetComponent<FishPath>();
        if (found)
        {
            // backtrack from dest to start
            // add all the parents of the QueueNodes
            FishPath.waypoints.Clear();
            FishPath.waypoints.Add(this.start.position);
            while (!current.pos.Equals(this.startPos))
            {
                FishPath.waypoints.Add(current.pos);
                current = current.parent;
            }
            FishPath.waypoints.Add(current.pos);
            FishPath.waypoints.Add(this.end.position); 
            message = "A * took: " + stopwatch.ElapsedMilliseconds + " milliseconds. Open list: " + maxSize;

        }
        else
        {
            if (timeout)
            {
                message = "A* timed out after 5 seconds. Open list: " + maxSize;
            }
            else
            {
                message = "No FishPath found. Open list: " + maxSize;
            }
            
        }
        if (smooth)
        {
            SmoothFishPath(FishPath);
        }
        return FishPath;
    }

    private void addAdjacentQueueNodes(QueueNode current)
    {

        // -1 x, -1 y, -1 z, +1 x, +1 y, +1 z,
        // finding all adjacent QueueNodes
        // adding it if valid
        for(int x = -1 ; x <= 1 ; x ++)
        {
            int yrange = isThreeD ? 1 : 0;
            for(int y = - yrange ; y <= yrange ; y ++)
            {
                for(int z = -1 ; z <= 1 ; z ++)
                {
                    if (! (x == 0 && y == 0 && z == 0))
                    {
                        Vector3 pos = current.pos + new Vector3(x * gridSize, y * gridSize, z * gridSize);
                        AddIfValid(pos, current);
                    }
                }
            }    
        }        	        
    }

    private void AddIfValid(Vector3 pos, QueueNode parent)
    {
        // ray cast to track any objects
        // center of current QueueNode to center of adjacent QueueNode
        if ((!RayTrace(parent.pos, pos)))
        {
            // if is not in closed list
            if (!closed.ContainsKey(pos))
            {
                // if is not in open list
                if (!open.ContainsKey(pos))
                {
                    // create a new QueueNode, and calculate it's f, g and h score
                    QueueNode QueueNode = new QueueNode();
                    QueueNode.pos = pos;
                    QueueNode.g = parent.g + cost(QueueNode.pos, parent.pos);
                    QueueNode.h = heuristic(pos, endPos);
                    QueueNode.f = QueueNode.g + QueueNode.h;
                    // track it's parent
                    QueueNode.parent = parent;
                    // add it to the open list
                    if (usePQ)
                    {
                        openPQ.Enqueue(QueueNode);
                    }
                    open[pos] = QueueNode;
                }
                else
                {
                    // Edge relaxation?
                    // if it's in the open list, recalculate g score.
                    // if g score is lower, then update the QueueNode in open list
                    // found new parent with shorter FishPath
                    QueueNode QueueNode = open[pos];
                    float g = parent.g + cost(QueueNode.pos, parent.pos);
                    if (g < QueueNode.g)
                    {
                        QueueNode.g = g;
                        QueueNode.f = QueueNode.g + QueueNode.h;
                        QueueNode.parent = parent;
                    }
                }
            }
        }
    }

    public void SmoothFishPath(FishPath FishPath)
    {
        List<Vector3> wayPoints = FishPath.waypoints;

        if (wayPoints.Count < 3)
        {
            return;
        }

        int current;
        int middle;
        int last;

        current = 0;
        middle = current + 1;
        last = current + 2;

        while (last != wayPoints.Count)
        {

            Vector3 point0, point2;

            point0 = wayPoints[current];
            point2 = wayPoints[last];
            point0.y = 0;
            point2.y = 0;
            if ((RayTrace(point0, point2)))
            {
                current++;
                middle++;
                last++;

            }
            else
            {
                wayPoints.RemoveAt(middle);
            }
        }
    }

    private float heuristic(Vector3 v1, Vector3 v2)
    {
        // straight line difference, scaled by 10
        return 10.0f * (Math.Abs(v2.x - v1.x) + Math.Abs(v2.y - v1.y) + Math.Abs(v2.z - v1.z));
    }

    private float cost(Vector3 v1, Vector3 v2)
    {
        // returning 10 or 14 if diagonal
        int dist = (int)Math.Abs(v2.x - v1.x) + (int)Math.Abs(v2.y - v1.y) + (int)Math.Abs(v2.z - v1.z);
        return (dist == 1) ? 10 : 14;
    }

    public LayerMask obstaclesLayerMask;

    bool RayTrace(Vector3 start, Vector3 end)
    {
        Vector3 toEnd = end - start;
        return Physics.Raycast(start, toEnd, toEnd.magnitude, obstaclesLayerMask);
    }
}
