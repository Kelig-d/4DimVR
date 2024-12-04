using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabFrag : MonoBehaviour
{

    public enum Name { Berceau, ZimmaBlue, Mi7, ChronoPhagos };
    public Name Dimension;
    public GameObject anchor;
    private XRGrabInteractable grabInteractable;
    public ArtefactdeTp script;

    void Start()
    {
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
        Debug.Log($"{gameObject.name} a �t� saisi !");
        
        script.AddDimension(Dimension.ToString() );
    }

    private void OnRelease(SelectExitEventArgs args)
    { 
        this.transform.position = anchor.transform.position;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
