using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PorteTeleportation : MonoBehaviour
{
    public GameObject otherPortal;

    void OnCollisionEnter(Collision collision)
    {
        if (otherPortal.transform.parent.transform.localScale != transform.parent.transform.localScale)
        {
            collision.gameObject.transform.parent.transform.localScale *= otherPortal.transform.parent.transform.localScale.x / transform.parent.transform.localScale.x;
            Time.timeScale = 0;
        }
        collision.gameObject.transform.position = new Vector3(
            otherPortal.transform.position.x + Mathf.Clamp(otherPortal.transform.parent.rotation.y,0,1.5f),
            otherPortal.transform.position.y-1.5f,
            otherPortal.transform.position.z+ Mathf.Clamp(otherPortal.transform.parent.rotation.y,1.5f,0));
        collision.gameObject.transform.rotation = new Quaternion(0, otherPortal.transform.rotation.y, 0, otherPortal.transform.rotation.w);
    }
}
