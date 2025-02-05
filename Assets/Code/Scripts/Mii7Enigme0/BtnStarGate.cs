using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit;

public class BtnStarGate : MonoBehaviour
{

    public StarGateKeyEnigme starGateKeyEnigme;
    private XRGrabInteractable grabInteractable;
    private bool select;
    private Color color;


    void Start()
    {
        select = false;
        // Récupérer le composant XRGrabInteractable
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



    private void OnGrab(SelectEnterEventArgs args)
    {
        if ( !select)
        {
            select=true;
            starGateKeyEnigme.MessageAddCode(color);
            GetComponent<Renderer>().material.color = Color.white;

        }
        else
        {
            select= false;
            starGateKeyEnigme.MessageRemoveCode(color);
            GetComponent<Renderer>().material.color = color;

        }
    }

    public void SetColor(Color value)
    {
        color = value;
        GetComponent<Renderer>().material.color = color;
    }

    public void Reset()
    {
        select = false;
        GetComponent<Renderer>().material.color = color;
    }
}
