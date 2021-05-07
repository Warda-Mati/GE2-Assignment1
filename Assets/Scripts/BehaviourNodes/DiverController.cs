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

    public GameObject boat;

    public Vector3 startPos;
    // Start is called before the first frame update
    private void Awake()
    {
        ConstructBehaviourTree();
        startPos = this.transform.position;
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

        GoToBoatNode goToBoatNode = new GoToBoatNode(this, boat);
        GoToStart goToStart = new GoToStart(this);

        
        Sequence wanderSequence = new Sequence(new List<Node> {wanderingNode});
        Sequence collectSequence = new Sequence(new List<Node> {pursueNode, shootNode, collectNode});
        Sequence dropFishSequence = new Sequence(new List<Node> {goToBoatNode, goToStart});
        

        root = new Sequence(new List<Node> {wanderSequence,collectSequence,dropFishSequence});
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
