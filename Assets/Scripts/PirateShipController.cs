using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PirateShipController : MonoBehaviour
{
    public GameObject CannonObj;
    public GameObject cannonBallPrefab;
    private List<Transform> cannons = new List<Transform>();
    public bool piratesNearby = false;

    public GameObject fireParticle;
    
    private void Awake()
    {
        GetComponent<StateMachine>().ChangeState(new ShipMoving());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CannonObj.transform.childCount; i++)
        {
            cannons.Add(CannonObj.transform.GetChild(i));
        }

        StartCoroutine(FireCannon());
    }

    IEnumerator FireCannon()
    {
        while (true)
        {
            Transform chosenCannon = cannons[Random.Range (0, cannons.Count)];
            Instantiate(fireParticle, chosenCannon.position, chosenCannon.rotation);
            Instantiate(cannonBallPrefab, chosenCannon.position, chosenCannon.rotation);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pirates")
        {
            piratesNearby = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
