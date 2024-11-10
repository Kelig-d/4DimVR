using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatelightOnContact : MonoBehaviour
{
    public GameObject item;
    public GameObject BouleAFacette;

    void OnCollisionEnter(Collision collision)
    {
        BouleAFacette.transform.position = item.transform.position;
        Instantiate(BouleAFacette);
    }
}