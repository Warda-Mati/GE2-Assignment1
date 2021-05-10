using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttack : MonoBehaviour
{
    
    public float rotSpeed;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    private void OnEnable()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.identity, rotSpeed * time);
        if (transform.rotation == Quaternion.identity)
        {
            GetComponent<FishBoid>().maxSpeed = 0;
            GetComponent<FireCannon>().enabled = true;
        }
        //transform.position += (transform.forward * (moveSpeed) * Time.deltaTime);
    }
}
