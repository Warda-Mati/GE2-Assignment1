using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(FishBoid))]
public abstract class SteeringBehavior : MonoBehaviour
{
    public float weight = 1.0f;
    public Vector3 force;

    [HideInInspector]
    public FishBoid boid;

    public void Awake()
    {
        boid = GetComponent<FishBoid>();
    }

    public abstract Vector3 Calculate();
}
