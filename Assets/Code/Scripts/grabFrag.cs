using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabFrag : MonoBehaviour
{
    public enum Name { berceau, mi7, chronos, zima};
    public Name Dimension;
    private XRGrabInteractable grabInteractable;
    public ArtefactdeTp script;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // S'abonner aux événements
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }
    }

    // Méthode appelée lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} a été saisi !");
        
        script.GetFrag(Dimension.ToString() );
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
