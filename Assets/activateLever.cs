using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLever : MonoBehaviour
{
    public bool activated = false;
    public GameObject lever;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lever)
            activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        activated = false;
    }
}
