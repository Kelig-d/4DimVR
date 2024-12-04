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

    // M�thode appel�e lorsque l'objet est rel�ch�
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} a �t� rel�ch� !");
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
