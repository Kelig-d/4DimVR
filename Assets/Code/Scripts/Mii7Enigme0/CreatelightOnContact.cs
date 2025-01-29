using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatelightOnContact : MonoBehaviour
{
    private GameObject _item;
    public GameObject BouleAFacette;
    private Mi7Light mi7Light;

    private void Start()
    {
        _item = gameObject;
    }

    void OnCollisionEnter(Collision collision)
    {
        // R�cup�rer la vitesse relative au moment de l'impact
        Vector3 relativeVelocity = collision.relativeVelocity;

        // Calculer la magnitude (vitesse scalaire)
        float impactSpeed = relativeVelocity.magnitude;


        BouleAFacette.transform.position = _item.transform.position;

        GameObject init =  Instantiate(BouleAFacette);


        mi7Light = init.GetComponent<Mi7Light>();
        mi7Light.updateVitesse(impactSpeed);

    }
}