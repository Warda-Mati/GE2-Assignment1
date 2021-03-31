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
    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[flockSize];
        allfishes = new List<GameObject>();
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 pos = Random.insideUnitSphere;
            pos = new Vector3(pos.x * area.x, pos.y * area.y, pos.y * area.y);
            Vector3 fishPos = transform.position + pos;
            allFish[i] = Instantiate(fish, fishPos, transform.rotation);
            allFish[i].AddComponent<FlockingBehaviour>();
            allfishes.Add(allFish[i]);
        }

        for (int i = 0; i < allfishes.Count; i++)
        {
            allfishes[i].GetComponent<FlockingBehaviour>().agents = allfishes;
        }
        
    }

  
    
}
