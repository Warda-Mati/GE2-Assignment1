using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShipController : MonoBehaviour
{
    public GameObject CannonObj;
    public GameObject cannonBallPrefab;
    private List<Transform> cannons = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CannonObj.transform.childCount; i++)
        {
            cannons.Add(CannonObj.transform.GetChild(i));
        }

        StartCoroutine(FireCannon());
    }

    IEnumerator FireCannon()
    {
        while (true)
        {
            Transform chosenCannon = cannons[Random.Range (0, cannons.Count)];
            Instantiate(cannonBallPrefab, chosenCannon.position, chosenCannon.rotation);
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
