using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public GameObject flotingposition;
    GameObject stored = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.GetComponent<XRGrabInteractable>().isSelected) // id de layer grabable
        {
            bool check = (other.gameObject.GetComponent<DropInventory>() == null || !other.gameObject.GetComponent<DropInventory>().inInventory);
            if (stored == null  && check)
            {
                Debug.Log($"{other.gameObject.name} est entré dans le trigger !");

                DropInventory module = other.gameObject.GetComponent<DropInventory>() ?? other.AddComponent<DropInventory>();
                module.inInventory = true;
                module.newItem =Instantiate(PrefabUtility.GetCorrespondingObjectFromOriginalSource(other));
                module.newItem.SetActive(false);
                module.referenceObject = other.gameObject;

                other.GetComponent<XRGrabInteractable>().enabled = false; // Lache de force l'objet 
                other.GetComponent<XRGrabInteractable>().enabled = true; // Permet de pouvoir re ratraper l'objet

                stored = other.gameObject; // on enregistre l'objet pour la comparaison 
                other.transform.SetParent(transform); // On le met en fils pour qu'il puisse bouger facilement ! 
                transform.localScale /= 5;
                

                // Reset toute les déplacement ! 
                var rb = other.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                other.transform.position = flotingposition.transform.position;


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stored = null;
        // Vérifie si l'objet qui sort du trigger est bien "stored"

        /*if (!other.GetComponent<XRGrabInteractable>().isSelected)
        {
            other.transform.position = flotingposition.transform.position; // Centre l'objet
        }
        else
        {
            
        }*/
    }

}
