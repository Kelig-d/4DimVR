using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatelightOnContact : MonoBehaviour
{
    public GameObject item;
    public GameObject BouleAFacette;
    private Mi7Light mi7Light;
    void OnCollisionEnter(Collision collision)
    {
        // Récupérer la vitesse relative au moment de l'impact
        Vector3 relativeVelocity = collision.relativeVelocity;

        // Calculer la magnitude (vitesse scalaire)
        float impactSpeed = relativeVelocity.magnitude;


        BouleAFacette.transform.position = item.transform.position;

        GameObject init =  Instantiate(BouleAFacette);


        mi7Light = init.GetComponent<Mi7Light>();
        mi7Light.updateVitesse(impactSpeed);

    }
}