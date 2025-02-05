using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckLight : MonoBehaviour
{

    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Light[] lights = FindObjectsOfType<Light>();
        float totalIntensity = 0f;

        foreach (Light light in lights)
        {
            float distance = Vector3.Distance(item.transform.position, light.transform.position);
            if (distance <= light.range)
            {
                // Calculer la contribution en fonction de la distance
                totalIntensity += light.intensity / (distance * distance);
            }
        }
    }
}
