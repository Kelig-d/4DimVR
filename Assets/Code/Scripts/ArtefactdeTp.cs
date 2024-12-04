using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ArtefactdeTp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Coeur;
    public GameObject Cercle;
    
    public GameObject CaillouARTMi7;
    public GameObject CaillouCERMi7;
    public GameObject GrabMi7;

    public GameObject CaillouARTZima;
    public GameObject CaillouCERZima;
    public GameObject GrabZima;

    public GameObject CaillouARTCp;
    public GameObject CaillouCERCp;
    public GameObject GrabCp;

    public GameObject CaillouARTBerceau;
    public GameObject CaillouCERBearcea;
    public GameObject GrabBerceau;
    


    private XRGrabInteractable grabInteractable;

    private bool FragBerceau;
    private bool FragZima;
    private bool FragChronos;
    private bool FragMi;


    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        Cercle.SetActive(false);

        FragBerceau = false;
        FragZima = false;
        FragMi = false;
        FragChronos = false;
        CaillouARTMi7.SetActive(false);
        CaillouCERMi7.SetActive(false);
        GrabMi7.SetActive(false);
        CaillouARTZima.SetActive(false);
        CaillouCERZima.SetActive(false);
        GrabZima.SetActive(false);
        CaillouARTCp.SetActive(false);
        CaillouCERCp.SetActive(false);
        GrabCp.SetActive(false);
        CaillouARTBerceau.SetActive(false);
        CaillouCERBearcea.SetActive(false);
        GrabBerceau.SetActive(false);
        
        
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
        Cercle.SetActive(true);

        if ( FragBerceau )
        {
            CaillouARTMi7.SetActive(true);
            CaillouCERMi7.SetActive(true);
            GrabMi7.SetActive(true);
        }

        if ( FragChronos )
        {
            CaillouARTZima.SetActive(true);
            CaillouCERZima.SetActive(true);
            GrabZima.SetActive(true);

        }

        if ( FragMi )
        {
            CaillouARTCp.SetActive(true);
            CaillouCERCp.SetActive(true);
            GrabCp.SetActive(true);

        }

        if ( FragZima )
        {
            CaillouARTBerceau.SetActive(true);
            CaillouCERBearcea.SetActive(true);
            GrabBerceau.SetActive(true);

        }



    }

    // M�thode appel�e lorsque l'objet est rel�ch�
    private void OnRelease(SelectExitEventArgs args)
    {
        Cercle.SetActive(false);

        CaillouARTMi7.SetActive(false);
        CaillouCERMi7.SetActive(false);
        GrabMi7.SetActive(false);

        CaillouARTZima.SetActive(false);
        CaillouCERZima.SetActive(false);
        GrabZima.SetActive(false);


        CaillouARTCp.SetActive(false);
        CaillouCERCp.SetActive(false);
        GrabCp.SetActive(false);

        CaillouARTBerceau.SetActive(false);
        CaillouCERBearcea.SetActive(false);
        GrabBerceau.SetActive(false);


    }


    public void AddDimension(string dimensionName)
    {
       switch(dimensionName)
        {
            case "Berceau":
                FragBerceau = true;
                break;

            case "ZimmaBlue":
                FragZima = true;
                break;

            case "Mi7":
                FragMi = true;
                break;

            case "ChronoPhagos":
                FragChronos = true;
                break;
        }
    }

    public void ChangeDimension(string dimensionName)
    {
        StartCoroutine(LoadNewDimension(dimensionName));
    }
    
    private IEnumerator LoadNewDimension(string dimensionName)
    {
        Debug.Log("Loading new dimension...");
        string currentWorldKey = SceneManager.GetActiveScene().name;

        // Charger la nouvelle scène de manière asynchrone
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dimensionName, LoadSceneMode.Additive);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }
        // Désactiver l'ancienne scène
        SceneManager.UnloadSceneAsync(currentWorldKey);
        Debug.Log("Loaded new dimension !");
    }
}
