using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public bool isNearFish;

    private Node root;
    // Start is called before the first frame update
    void Start()
    {
        ConstructBehaviourTree();
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }

    void ConstructBehaviourTree()
    {
        WanderingNode wanderingNode = new WanderingNode(this);

        root = new Selector(new List<Node> {wanderingNode});
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" && isNearFish == false)
        {
            isNearFish = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fish" && isNearFish)
        {
            isNearFish = false;
        }
    }
}
