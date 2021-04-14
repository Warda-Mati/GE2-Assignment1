using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinController : MonoBehaviour
{
    public bool goingToDive;
    public bool diving;
    public int splashesCounter;
    public GameObject splashPrefab;
    private GameObject camera;
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<StateMachine>().ChangeState(new FollowPathState());
    }

    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        StartCoroutine(goToDive());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator goToDive()
    {
        while (true)
        {
            if (camera.GetComponent<CameraFollow>().cameraTarget[camera.GetComponent<CameraFollow>().index] ==
                gameObject)
            {
                if (Input.GetKey(KeyCode.P))
                {
                    Debug.Log("Got it");
                    goingToDive = true;
                }

                yield return null;
            }

            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            goingToDive = false;
            if (splashesCounter < 3)
            {
                GetComponent<Seek>().enabled = false;
                diving = true;
                splashesCounter += 1;
                //Debug.Log("Water Collided");
                Vector3 pos = new Vector3(transform.position.x, other.transform.position.y+0.6f, transform.position.z);
                GameObject splash = Instantiate(splashPrefab, pos, Quaternion.Euler(90,0,0));
                splash.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                splashesCounter = 0;
                diving = false;
            }
        }
    }
}
