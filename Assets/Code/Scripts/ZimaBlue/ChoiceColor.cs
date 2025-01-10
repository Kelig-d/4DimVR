using System;
using UnityEngine;
using UnityEngine.Events;
public class ChoiceColor : MonoBehaviour
{
    public LineRenderer projectile;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
            Debug.Log("OnTriggerEnter");
            Pressed();
        
    }

    public void Pressed()
    { 
        LineRenderer spawProjectile = projectile;
        SetSingleColor2(spawProjectile,color);
    }

    public void SetSingleColor2(LineRenderer lineRenderer, Color newcolor){
        lineRenderer.startColor = newcolor;
        lineRenderer.endColor = newcolor;
    }

}
