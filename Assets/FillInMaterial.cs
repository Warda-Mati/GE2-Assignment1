using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    public Material mat;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer renderer = transform.GetChild(i).gameObject.GetComponent<Renderer>();
            renderer.material = mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
