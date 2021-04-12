using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinFlip : MonoBehaviour
{
    public float rotation = 90;
    public bool rotating;
    public float speed = 0.2f;

    private Quaternion rot;

    public GameObject splashPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rot = new Quaternion(rotation, 0.0f, 0.0f,0.0f);
        //StartCoroutine(RotateImage());
        
        
        //Debug.Log(transform.rotation.x + " > " + y);
    }

    // Update is called once per frame
    void Update()
    {
      
        float moveSpeed = 0.15f;
        float y = -270;
        Vector3 target = new Vector3(y,0,0);
        transform.localEulerAngles = Vector3.Lerp(transform.position,target, moveSpeed * Time.time);
  
        transform.position += (transform.forward * (moveSpeed*4) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            Debug.Log("Water Collided");
            Vector3 pos = new Vector3(transform.position.x, other.transform.position.y+0.5f, transform.position.z);
            GameObject splash = Instantiate(splashPrefab, pos, Quaternion.Euler(90,0,0));
            splash.GetComponent<ParticleSystem>().Play();
        }
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
