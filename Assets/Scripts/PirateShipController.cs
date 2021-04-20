using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PirateShipController : MonoBehaviour
{

    public bool piratesNearby = false;

  
    
    private void Awake()
    {
        GetComponent<StateMachine>().ChangeState(new ShipMoving());
    }
    
    // Start is called before the first frame update
   

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
