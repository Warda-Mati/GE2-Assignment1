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
    private ShootNode shootNode;
    
    public GameObject harpoonGun;
    public GameObject harpoon;

    public GameObject arm;

    public GameObject tank;
    public bool fishCollected = false;
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

        shootNode = new ShootNode(this);

        CollectNode collectNode = new CollectNode(this);

        Sequence mainSequence = new Sequence(new List<Node> {wanderingNode, pursueNode,shootNode,collectNode});

        root = new Selector(new List<Node> {mainSequence});
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" && isNearFish == false)
        {
            isNearFish = true;
            pursueNode.targetFish =  other.gameObject.GetComponent<FishBoid>();
            shootNode.targetFish = pursueNode.targetFish;
            targetFish = other.gameObject.GetComponent<FishBoid>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fish" && isNearFish)
        {
            isNearFish = false;
        }
    }
    
    public void ShootHarpoons()
    {
        arm.GetComponent<HarmonicWave>().enabled = false;
        arm.transform.localRotation = Quaternion.identity;
        Instantiate(harpoon, harpoonGun.transform.position, harpoonGun.transform.rotation);
    }
}
