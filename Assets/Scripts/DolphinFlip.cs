using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinFlip : MonoBehaviour
{
  
    public float moveSpeed = 0.2f;
    public float rotSpeed;
    private Quaternion rot;

    public float y = -270;

    public float time;
    // Start is called before the first frame update
  

    void Start()
    {
        time = 0;
        //StartCoroutine(RotateImage());
        
        
        //Debug.Log(transform.rotation.x + " > " + y);
    }

    public void OnEnable()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        Vector3 target = new Vector3(y,transform.position.y,transform.position.z);
        transform.localEulerAngles = Vector3.Lerp(transform.position,target, rotSpeed * time);
        transform.position += (transform.forward * (moveSpeed) * Time.deltaTime);
    }

   
   


    /*IEnumerator RotateImage()
    {
        float moveSpeed = 0.00005f;
        float y = -240;
        Quaternion target = new Quaternion(y,transform.rotation.y,transform.rotation.z,0);
        Debug.Log(transform.rotation.x + " > " + y);
        //while(transform.rotation.x > y)
        //{
            transform.rotation = Quaternion.Slerp(transform.rotation, target, moveSpeed * Time.time);
        //yield return null;
        //}
        //yield return null;
    }*/
}
