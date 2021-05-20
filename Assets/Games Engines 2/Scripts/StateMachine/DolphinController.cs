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
    public GameObject camera;
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

    private void OnEnable()
    {
        StartCoroutine(goToDive());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("dolphin controller");
        if(Input.GetKey(KeyCode.P))
            Debug.Log("P has been clicked");
        //Debug.Log(camera.GetComponent<CameraFollow>().cameraTarget[camera.GetComponent<CameraFollow>().index]);
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
        }
    }

    public void goToPath()
    {
        splashesCounter = 0;
        diving = false;
        GetComponent<StateMachine>().ChangeState(new FollowPathState());
    }
}

class MovetoDive : State
{
    public override void Enter()
    {
        owner.GetComponent<MoveToLand>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<DolphinController>().diving)
        {
            owner.ChangeState(new DiveState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<MoveToLand>().enabled = false;
    }
}



class DiveState : State
{
    public override void Enter()
    {
        owner.GetComponent<DolphinFlip>().enabled = true;
    }

    public override void Think()
    {
        // finished
        if (owner.GetComponent<DolphinController>().splashesCounter == 3)
        {
            owner.GetComponent<DolphinController>().Invoke("goToPath",2);
            //owner.ChangeState(new FollowPathState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<DolphinFlip>().enabled = false;
    }
}