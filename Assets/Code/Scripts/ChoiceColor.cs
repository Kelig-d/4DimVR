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

    public void SetSingleColor(LineRenderer lineRenderer, Color newcolor){
        lineRenderer.startColor = newcolor;
        lineRenderer.endColor = newcolor;
    }

    public void SetSingleColor2(LineRenderer lineRenderer, Color newcolor){
        
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(newcolor,0);
        colorKeys[1] = new GradientColorKey(newcolor,1);

        gradient.colorKeys = colorKeys;

        lineRenderer.colorGradient = gradient;
        
    }

    public void Released()
    {/*
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");*/
    }

}
