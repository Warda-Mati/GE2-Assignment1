using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttack : MonoBehaviour
{
    public int rotationToAttack = 0;
    public float moveSpeed = 0.2f;
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
        float y = rotationToAttack;
        time += Time.deltaTime;
        Vector3 target = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.position,target, rotSpeed * time));
        //transform.position += (transform.forward * (moveSpeed) * Time.deltaTime);
    }
}
