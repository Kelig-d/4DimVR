using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MessagereiBtn : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public GameObject Touche;
    public Color colorBase;
    public Color colorNotif;

    public AudioClip song;
    public AudioSource tel;

    bool Active= true;
    int time = 0;


    // Start is called before the first frame update
    void Start()
    {   
        // Récupérer le composant XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null && Active)
        {
            // S'abonner aux événements
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }

    }

    // Méthode appelée lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        Active = false;
        tel.clip = song;
        tel.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            time += 1;

            if (time % 80 == 0)
            {
                if(Touche.GetComponent<Renderer>().material.color== colorBase) { Touche.GetComponent<Renderer>().material.color= colorNotif; }
                else { Touche.GetComponent<Renderer>().material.color=colorBase; }  
            }

        }

    }
}
