using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject center;
    public GameObject itemslot;
    public DropInventory module;

    public void reloaditem()
    {
        Destroy(itemslot);
        itemslot = Instantiate(Prefab);
        itemslot.transform.SetParent(transform);
        itemslot.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        itemslot.transform.position = center.transform.position;
 
        module = itemslot.gameObject.GetComponent<DropInventory>();
        module.inventorySlot = this;
        itemslot.SetActive(false);
    }

    public void Start()
    {
        reloaditem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.GetComponent<XRGrabInteractable>().isSelected) // id de layer grabable
        {
            if (!itemslot.active)
            {

                Debug.Log($"{other.gameObject.name} est entré dans le trigger !");


                itemslot.SetActive(true);

              
                module.inInventory = true;
                module.item = other.gameObject;
                module.item.SetActive(false);
            }
        }
    }
}
