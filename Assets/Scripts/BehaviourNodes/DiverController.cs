using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public bool isNearFish;
    public FishBoid targetFish;
    private Node root;

    private WanderingNode wanderingNode;
    private PursueNode pursueNode;
    private ShootNode shootNode;
    private CollectNode collectNode;
    private GoToBoatNode GoToBoatNode;
    private GoToStart GoToStart;

    private Node[] _nodes;
    
    public GameObject harpoonGun;
    public GameObject harpoon;

    public GameObject arm;

    public GameObject tank;
    public bool fishCollected = false;

    public GameObject boat;

    public Vector3 startPos;

    public Node[] sequences;
    // Start is called before the first frame update
    private void Awake()
    {
        ConstructBehaviourTree();
        startPos = this.transform.position;
        _nodes = new Node[6];
        _nodes[0] = wanderingNode;
        _nodes[1] = pursueNode;
        _nodes[2] = shootNode;
        _nodes[3] = collectNode;
        _nodes[4] = GoToBoatNode;
        _nodes[5] = GoToStart;
        StartCoroutine(showStates());
    }
    
    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }

    void ConstructBehaviourTree()
    {
        wanderingNode = new WanderingNode(this);
        pursueNode = new PursueNode(this, targetFish);

        shootNode = new ShootNode(this);

        collectNode = new CollectNode(this);

        GoToBoatNode = new GoToBoatNode(this, boat);
        GoToStart = new GoToStart(this);

        
        
        
        Sequence wanderSequence = new Sequence(new List<Node> {wanderingNode});
        Sequence collectSequence = new Sequence(new List<Node> {pursueNode,collectNode});
        Sequence dropFishSequence = new Sequence(new List<Node> {GoToBoatNode, GoToStart});

        sequences = new[] {wanderSequence, collectSequence, dropFishSequence};
        //Sequence diverAction = new Sequence(new List<Node>
        //    {wanderingNode, pursueNode, shootNode, collectNode, goToBoatNode, goToStart});

        //root = new Selector(new List<Node> {diverAction});
        root = new Sequence(new List<Node> {wanderSequence,collectSequence,dropFishSequence});
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" && isNearFish == false)
        {
            isNearFish = true;
            pursueNode.targetFish =  other.gameObject.GetComponent<FishBoid>();
            //shootNode.targetFish = pursueNode.targetFish;
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

    IEnumerator showStates()
    {
        while (true)
        {
            String[] nodeNames = new[] {"wandering","pursue","shoot","collect","boat","start"};
            String[] sequenceNames = {"wander", "collect", "go"};
            for (int i = 0; i < _nodes.Length; i++)
            {
                Debug.Log("Node " + nodeNames[i] + " -> " + _nodes[i].nodeState);
            }
          
            yield return new WaitForSeconds(2);
        }
    }
}
