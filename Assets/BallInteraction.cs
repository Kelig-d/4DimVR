using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BallInteraction : MonoBehaviour
{
    public GameObject bounceBall;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Interaction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction(ActivateEventArgs args)
    {
        GameObject otherBall = Instantiate(bounceBall);
        otherBall.transform.position = transform.position;
        otherBall.GetComponent<Rigidbody>().AddForce(transform.up * 10);
    }
}
