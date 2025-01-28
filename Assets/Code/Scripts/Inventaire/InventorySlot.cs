using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public GameObject flotingposition;
    GameObject stored = null;
    Vector3 size = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // id de layer grabable
        {
            if (stored == null && other.GetComponent<XRGrabInteractable>().isSelected)
            {
                other.GetComponent<XRGrabInteractable>().enabled = false;
                Debug.Log($"{other.gameObject.name} est entré dans le trigger !");

                stored = other.gameObject;

                stored.transform.SetParent(transform);
                stored.isStatic = true;

                var rb = stored.GetComponent<Rigidbody>();
                var vvv = stored.GetComponent<XRGrabInteractable>();
  
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                stored.transform.position = flotingposition.transform.position;

                size = stored.transform.localScale ;
                stored.isStatic = false;
                other.GetComponent<XRGrabInteractable>().enabled = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (stored == other.gameObject)
        {
            Debug.Log($"{other.gameObject.name} est sortie dans le trigger !");
            stored.transform.localScale = size;
            stored.transform.SetParent(null);

            stored = null;

            
        }

    }

}
