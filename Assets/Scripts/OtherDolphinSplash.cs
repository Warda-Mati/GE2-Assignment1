using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherDolphinSplash : MonoBehaviour
{
    public GameObject splashPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            Vector3 pos = new Vector3(transform.position.x, other.transform.position.y+0.6f, transform.position.z);
            GameObject splash = Instantiate(splashPrefab, pos, Quaternion.Euler(90,0,0));
            splash.GetComponent<ParticleSystem>().Play();
        }
    }
}
