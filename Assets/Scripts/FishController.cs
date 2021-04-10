using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public GameObject shark;
    public bool withinShark;
    
    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "shark")
        {
            Debug.Log("collided");
            withinShark = true;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        shark = GameObject.FindWithTag("shark");
        withinShark = false;
        GetComponent<StateMachine>().ChangeState(new MoveState());
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
