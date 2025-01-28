using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropInventory : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public bool inInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);

        }
    }
    private void OnGrab(SelectEnterEventArgs args)
    {
   
    }
        private void OnRelease(SelectExitEventArgs args) 
    { 
        if(inInventory)
        {
            this.gameObject.transform.SetParent(null, true);
            this.transform.localScale *= 3;
            inInventory = false;

            var rb = this.GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
}
