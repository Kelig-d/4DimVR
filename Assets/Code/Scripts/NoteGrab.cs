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
        // R�cup�rer le composant XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // S'abonner aux �v�nements
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }

    }

    // M�thode appel�e lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} a �t� saisi !");
        txt.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    // M�thode appel�e lorsque l'objet est rel�ch�
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} a �t� rel�ch� !");
        txt.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
