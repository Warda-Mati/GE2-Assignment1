using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float density;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);
        RenderSettings.fogDensity = density;
        RenderSettings.fog = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
