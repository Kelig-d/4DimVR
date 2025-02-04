using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabFrag : MonoBehaviour
{

    public enum Name { Berceau, ZimmaBlue, Mi7, ChronoPhagos , BerceauMI7TEST , Mi7MI7TEST };
    public Name Dimension;
    public GameObject anchor;
    private XRGrabInteractable grabInteractable;
    private TeleportationManager script;

    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<TeleportationManager>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // S'abonner aux �v�nements
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }
    }

    // M�thode appel�e lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        
        script.ChangeDimension(Dimension.ToString());
    }

    private void OnRelease(SelectExitEventArgs args)
    { 
        transform.position = anchor.transform.position;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
