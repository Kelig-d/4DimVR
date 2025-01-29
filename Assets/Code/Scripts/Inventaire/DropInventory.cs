using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;

public class DropInventory : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public bool inInventory = false;
    public InventorySlot inventorySlot;
    
    public GameObject item = null;
    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);

        }
    }

        private void OnRelease(SelectExitEventArgs args) 
    { 
        if(inInventory)
        {
            item.SetActive(true);
            item.transform.position = transform.position;
            inventorySlot.reloaditem();
           /* this.gameObject.transform.SetParent(null, true);
            this.transform.localScale *= 5;
            inInventory = false;

            var rb = this.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;*/
        }
    }
}
