using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class CameraFollow : MonoBehaviour
{
    public List<GameObject> cameraTarget = new List<GameObject>();
    public int speed;
    public Vector3 spacing;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        for (int i = 0; i < cameraTarget.Count; i++)
        {
            if (cameraTarget[i].tag == "flock")
            {
                Random randomFish = new Random();
                int chosenFish = randomFish.Next(0, cameraTarget[i].GetComponent<Flocking>().flockSize);
                Debug.Log(cameraTarget[i].GetComponent<Flocking>().allfishes[chosenFish].name);
                cameraTarget[i] = cameraTarget[i].GetComponent<Flocking>().allfishes[chosenFish];
            }
        }

        StartCoroutine(switchCamera());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTarget[index].transform);
        Vector3 toTarget = cameraTarget[index].transform.position - spacing;
        Vector3 toPos = Vector3.Lerp(toTarget, transform.position, speed * Time.deltaTime);
        transform.position = toPos;

    }

    IEnumerator switchCamera()
    {
        while (true)
        {
            if (Input.GetKey("l"))
                index = (index+ 1) % cameraTarget.Count;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
