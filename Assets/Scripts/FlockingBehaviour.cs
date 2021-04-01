using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingBehaviour : SteeringBehavior
{
    public List<GameObject> agents;
    
    public override Vector3 Calculate()
    {
        return Seperation() + Cohesion(); //Alignment();
    }

    public Vector3 Seperation()
    {
        Vector3 steeringForce = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            GameObject entity = agents[i];
            if (entity != gameObject)
            {
                Vector3 toEntity = transform.position - entity.transform.position;
                steeringForce += Vector3.Normalize(toEntity) / toEntity.magnitude;
            }
        }
        
        return steeringForce;
    }

    public Vector3 Cohesion()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 centreOfMass = Vector3.zero;
        int neighbourCount = 0;
        foreach (GameObject entity in agents)
        {
            if (entity != gameObject)
            {
                centreOfMass += entity.transform.position;
                neighbourCount++;
            }   
        }
        if (neighbourCount > 0)
        {
            centreOfMass /= (float)neighbourCount;
            if (centreOfMass.sqrMagnitude == 0)
            {
                steeringForce = Vector3.zero;
            }
            else
            {
                
                steeringForce = Vector3.Normalize(boid.SeekForce(centreOfMass));     
                
            }
        }
        Debug.Log(steeringForce);
        checkNaN(steeringForce);
        return steeringForce;
    }

    public Vector3 Alignment()
    {
        Vector3 steeringForce = Vector3.zero;
        int neighbourCount = 0;

        foreach (GameObject agent in agents)
        {
            if (agent != gameObject)
            {
                steeringForce += agent.transform.position;
                neighbourCount++;   
            }
        }

        if (neighbourCount > 0)
        {
            steeringForce /= (float) neighbourCount;
            steeringForce = steeringForce - transform.forward;
        }
       
        return steeringForce;
    }
    
    static public bool checkNaN(Vector3 v)
    {
        if (float.IsNaN(v.x))
        {
            Debug.Log("Nan x");
            return true;
        }
        if (float.IsNaN(v.y))
        {
            Debug.Log("Nan y");
            return true;
        }
        if (float.IsNaN(v.z))
        {
            Debug.Log("Nan z");
            return true;
        }
        return false;
    }

}
