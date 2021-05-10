using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject CannonObj;
    public GameObject cannonBallPrefab;
    private List<Transform> cannons = new List<Transform>();
    public GameObject fireParticle;

    public AudioClip cannonFire;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CannonObj.transform.childCount; i++)
        {
            cannons.Add(CannonObj.transform.GetChild(i));
        }

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (GetComponent<PirateShipController>().piratesNearby)
            {
                GetComponent<AudioSource>().Pause();
                GetComponent<AudioSource>().Play();
                Transform chosenCannon = cannons[Random.Range (0, cannons.Count)];
                Instantiate(fireParticle, chosenCannon.position, chosenCannon.rotation);
                Instantiate(cannonBallPrefab, chosenCannon.position, chosenCannon.rotation);
                yield return new WaitForSeconds(1);
            }

            yield return null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
