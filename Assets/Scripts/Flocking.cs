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
    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 pos = Random.insideUnitSphere;
            pos = new Vector3(pos.x * area.x, pos.y * area.y, pos.y * area.y);
            Vector3 fishPos = transform.position + pos;
            allFish[i] = Instantiate(fish, fishPos, transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Seperation()
    {
        for (int i = 0; i < tagged.Count; i++)
        {
            entity = tagged[i];
        }

        if (entity != null)
        {
            toEntity = agent.pos - entity.pos;
            steeringForce += toEntity.normalize / toEntity
        }
    }*/
    
}
