using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArtefactdeTp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Coeur;
    public GameObject Cercle;
    
    public GameObject CaillouART0;
    public GameObject CaillouCER0;
    public GameObject Grab0;

    public GameObject CaillouART1;
    public GameObject CaillouCER1;
    public GameObject Grab1;

    public GameObject CaillouART2;
    public GameObject CaillouCER2;
    public GameObject Grab2;

    public GameObject CaillouART3;
    public GameObject CaillouCER3;
    public GameObject Grab3;


    private XRGrabInteractable grabInteractable;

    private bool FragBerceau;
    private bool FragZima;
    private bool FragChronos;
    private bool FragMi;


    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        
        FragBerceau = false;
        FragZima = false;
        FragMi = false;
        FragChronos = false;
        
        
        
        if (grabInteractable != null)
        {
            // S'abonner aux événements
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }

    }

    public void GetFrag(string name)
    {

        switch(name)
        {
            case "berceau":
                FragBerceau = true;
                break;

            case "mi7":
                FragMi = true;
                break;

            case "chronos":
                FragChronos = true;
                break;

            case "zima":
                FragZima = true;
                break;


        }

    }

    // Méthode appelée lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} a été saisi !");
        Cercle.SetActive(true);

        if ( FragBerceau )
        {
            CaillouART0.SetActive(true);
            CaillouCER0.SetActive(true);
            Grab0.SetActive(true);
        }

        if ( FragChronos )
        {
            CaillouART1.SetActive(true);
            CaillouCER1.SetActive(true);
            Grab1.SetActive(true);

        }

        if ( FragMi )
        {
            CaillouART2.SetActive(true);
            CaillouCER2.SetActive(true);
            Grab2.SetActive(true);

        }

        if ( FragZima )
        {
            CaillouART3.SetActive(true);
            CaillouCER3.SetActive(true);
            Grab3.SetActive(true);

        }



    }

    // Méthode appelée lorsque l'objet est relâché
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} a été relâché !");
        Cercle.SetActive(false);

        CaillouART0.SetActive(false);
        CaillouCER0.SetActive(false);
        Grab0.SetActive(false);

        CaillouART1.SetActive(false);
        CaillouCER1.SetActive(false);
        Grab1.SetActive(false);


        CaillouART2.SetActive(false);
        CaillouCER2.SetActive(false);
        Grab2.SetActive(false);

        CaillouART3.SetActive(false);
        CaillouCER3.SetActive(false);
        Grab3.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
