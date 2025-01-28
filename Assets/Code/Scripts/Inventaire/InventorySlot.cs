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
        if (other.gameObject.layer == 3 && !other.GetComponent<XRGrabInteractable>().isSelected) // id de layer grabable
        {
            bool check = (other.gameObject.GetComponent<DropInventory>() == null || !other.gameObject.GetComponent<DropInventory>().inInventory);
            if (stored == null  && check)
            {
                this.GetComponent<SphereCollider>().enabled = false;
                this.GetComponent<SphereCollider>().enabled = true;
                other.GetComponent<XRGrabInteractable>().enabled = false; // Lache de force l'objet 
                other.GetComponent<XRGrabInteractable>().enabled = true; // Permet de pouvoir re ratraper l'objet

                stored = other.gameObject; // on enregistre l'objet pour la comparaison 
                other.transform.SetParent(transform); // On le met en fils pour qu'il puisse bouger facilement ! 
                transform.localScale /= 5;


                // Reset toute les déplacement ! 
                var rb = other.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
                
                DropInventory module = other.gameObject.GetComponent<DropInventory>() ?? other.AddComponent<DropInventory>();
                module.inInventory = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet qui sort du trigger est bien "stored"

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
