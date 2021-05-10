using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> cameraTarget = new List<Transform>();
    public float speed;
    public float angle;
    public Vector3 spacing;

    public int index;

    public bool rotated;

    private Transform toTarget;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        for (int i = 0; i < cameraTarget.Count; i++)
        {
            if (cameraTarget[i].gameObject.tag == "flock")
            {
                Random randomFish = new Random();
                int chosenFish = randomFish.Next(0, cameraTarget[i].GetComponent<Flocking>().flockSize);
                cameraTarget[i] = cameraTarget[i].GetComponent<Flocking>().allfishes[chosenFish].transform;
            }
        }
        
        StartCoroutine(switchCamera());
    }

    // Update is called once per frame
    void Update()
    {
        
        toTarget = cameraTarget[index].transform;
        toTarget.transform.LookAt(cameraTarget[index].transform.parent);
        Vector3 toPos = Vector3.Lerp(toTarget.position, transform.position, speed * Time.deltaTime);
        transform.position = toPos - spacing;

        
        
        if (Input.GetKey(KeyCode.R))
        {
            rotated = true;
            Debug.Log("rotating");
            //toTarget.transform.RotateAround(Vector3.right * angle * Time.deltaTime);
        }
        
    }

    IEnumerator switchCamera()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.L))
                index = (index+ 1) % cameraTarget.Count;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
