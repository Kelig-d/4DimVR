 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PorteTeleportation : MonoBehaviour
{
    public GameObject PointOfTp;

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = PointOfTp.transform.position;
        collision.gameObject.transform.rotation = PointOfTp.transform.rotation;
    }
}
