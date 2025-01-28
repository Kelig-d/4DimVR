using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public GameObject flotingposition;
    GameObject stored = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // id de layer grabable
        {
            if (stored == null && !other.GetComponent<XRGrabInteractable>().isSelected)
            {
                other.GetComponent<XRGrabInteractable>().enabled = false; // Lache de force l'objet 
                other.GetComponent<XRGrabInteractable>().enabled = true; // Permet de pouvoir re ratraper l'objet

                stored = other.gameObject; // on enregistre l'objet pour la comparaison 
                other.transform.SetParent(transform); // On le met en fils pour qu'il puisse bouger facilement ! 
                transform.localScale /= 3;

                // Reset toute les déplacement ! 
                var rb = other.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                
                DropInventory module = other.gameObject.GetComponent<DropInventory>() ?? other.AddComponent<DropInventory>();
                module.inInventory = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet qui sort du trigger est bien "stored"
        /*if (stored == other.gameObject)
        {
            

            // Vérifie si l'objet est actuellement grab
            if (other.GetComponent<XRGrabInteractable>().isSelected)
            {
                other.GetComponent<XRGrabInteractable>().enabled = false; // Lache de force l'objet 
                other.transform.SetParent(null,true);
                var rb = other.GetComponent<Rigidbody>();
                rb.useGravity = true;
                other.GetComponent<XRGrabInteractable>().enabled = true;
                 // Permet de pouvoir re ratraper l'objet

                // Réinitialise l'échelle de l'objet
                // other.transform.localScale = Vector3;



                // Détache l'objet de son parent
          
                // other.transform.SetParent(null); Je ne comprend pas pk mais on peut pas le fair dedans !

                stored = null;
            }
            else
            {
                other.transform.SetParent(transform); // On le met en fils pour qu'il puisse bouger facilement ! 
                other.transform.position = flotingposition.transform.position; // Centre l'objet
                Debug.Log("EXIT: Objet recentré");
            }
        }*/

        if (!other.GetComponent<XRGrabInteractable>().isSelected)
        {
            other.transform.position = flotingposition.transform.position; // Centre l'objet
        }
        else
        {
            stored = null;
        }
    }

}
