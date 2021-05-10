using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flocking : MonoBehaviour
{
    public GameObject fish;
    public int flockSize;
    public Vector3 area;

    public GameObject[] allFish;

    public List<GameObject> allfishes;

    public bool doryFlock;
    public int radius;
    // Start is called before the first frame update
    void Awake()
    {
        allFish = new GameObject[flockSize];
        allfishes = new List<GameObject>();
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            Vector3 fishPos = transform.position + pos;
            allFish[i] = Instantiate(fish, fishPos, transform.rotation);
            allFish[i].name = "Fish " + i;
            allFish[i].tag = "fish";
            allFish[i].AddComponent<FlockingBehaviour>();
            if (doryFlock)
            {
                allFish[i].transform.parent = transform.parent;
                allFish[i].GetComponent<FollowFlowField>().flowFieldGrid = transform.parent.GetComponent<FlowFieldGrid>();
                allFish[i].GetComponent<FollowFlowField>().target = transform.parent.GetComponent<FlowFieldGrid>().target;
            }
                
            else 
                allFish[i].transform.parent = transform;
            
            allfishes.Add(allFish[i]);
        }

        for (int i = 0; i < allfishes.Count; i++)
        {
            allfishes[i].GetComponent<FlockingBehaviour>().agents = allfishes;
        }
        
    }

  
    
}
