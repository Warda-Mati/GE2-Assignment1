using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public bool isNearFish;
    public FishBoid targetFish;
    private Node root;

    private PursueNode pursueNode;
    public GameObject harpoonGun;
    public GameObject harpoon;
    // Start is called before the first frame update
    private void Awake()
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
        pursueNode = new PursueNode(this, targetFish);

        Sequence mainSequence = new Sequence(new List<Node> {wanderingNode, pursueNode});

        root = new Selector(new List<Node> {mainSequence});
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" && isNearFish == false)
        {
            isNearFish = true;
            //targetFish = other.gameObject.GetComponent<FishBoid>();
            pursueNode.targetFish =  other.gameObject.GetComponent<FishBoid>();
            targetFish = other.gameObject.GetComponent<FishBoid>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fish" && isNearFish)
        {
            isNearFish = false;
            //pursueNode.targetFish = null;
        }
    }
    
    public void ShootHarpoons()
    {
        GameObject shotHarpoon =
            GameObject.Instantiate(harpoon, harpoonGun.transform.position, harpoon.transform.rotation);
    }
}
