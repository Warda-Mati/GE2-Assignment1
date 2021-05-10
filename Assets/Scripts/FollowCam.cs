using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Transform otherShip;
    public float density;

    public float speed;

    public AudioClip battle;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject.GetComponent<MoveToAttack>().enabled)
        {
            Vector3 newPos = target.transform.position;
            newPos.x += 10;
            newPos.z -= 10;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime);
            transform.LookAt(target);
        }

        if (target.gameObject.GetComponent<PirateShipController>().piratesNearby)
        {
            GetComponent<AudioSource>().Pause();
            GetComponent<AudioSource>().clip = battle;
            GetComponent<AudioSource>().Play();
        }

        if (target.tag == "sinking")
        {
            goToSink(target);
        }
        else if(otherShip.tag == "sinking")
        {
            goToSink(otherShip);
        }
        
       
    }

    void goToSink(Transform ship)
    {
        Transform deathTarget = ship.GetChild(target.childCount - 1);
        transform.position = Vector3.Lerp(transform.position, deathTarget.position, Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, deathTarget.rotation, Time.deltaTime);
    }

   
}
