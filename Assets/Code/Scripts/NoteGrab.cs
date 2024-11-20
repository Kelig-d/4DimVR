using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoteGrab : MonoBehaviour
{
    public TextMeshPro txt;
    public Canvas canvas;
    private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        // Récupérer le composant XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

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

    // Méthode appelée lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} a été saisi !");
        txt.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    // Méthode appelée lorsque l'objet est relâché
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} a été relâché !");
        txt.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
