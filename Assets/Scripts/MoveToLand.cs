using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class MoveToLand : MonoBehaviour
{
    public int radius;

    public GameObject water;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-radius, radius), water.transform.position.y - 0.2f, Random.Range(-radius, radius));
        GetComponent<Seek>().enabled = true;
        GetComponent<Seek>().target = pos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}