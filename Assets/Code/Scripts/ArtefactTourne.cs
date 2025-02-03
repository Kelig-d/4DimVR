using System.Collections;
using Code.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtefactTourne : MonoBehaviour
{
    public GameObject Artefact;

    private bool teleported = false;
    
    bool rotatY = false;
    double rotateY = 0;

    public GameObject Cone0;
    public GameObject Activator0;
    public  Color C0;
    bool Activer0;

    public GameObject Cone1;
    public Color C1;
    public GameObject Activator1;
    bool Activer1;


    public GameObject Cone2;
    public Color C2;
    public GameObject Activator2;
    bool Activer2;



    public GameObject Cone3;
    public Color C3;
    public GameObject Activator3;
    bool Activer3;

    bool CheckActivation(GameObject cone, Color color, GameObject activator)
    {
        if (activator.gameObject.GetComponentInChildren<activateLever>().activated)
        {
            Debug.Log(activator.gameObject.GetComponentInChildren<activateLever>().activated);
            cone.GetComponent<Renderer>().material.color = color;
            return true;
        }

        return false;
    }

    void FixedUpdate()
    {
        rotateY += Time.deltaTime * 15;

        if (rotateY > 360.0f)
        {
            rotateY = 0.0f;
            rotatY = false;
        }
        Artefact.transform.localRotation = Quaternion.Euler(0, (float)rotateY, 0);

        Activer0 = CheckActivation(Cone0 , C0, Activator0);
        Activer1 = CheckActivation(Cone1 , C1, Activator1);
        Activer2 = CheckActivation(Cone2 , C2, Activator2);
        Activer3 = CheckActivation(Cone3 , C3, Activator3);

        if( Activer0 && Activer1 && Activer2 && Activer3 && teleported == false)
        {
            
            GlobalManager.Instance.tutorialDone = true;
            GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<TeleportationManager>().ChangeDimension("Berceau",new Vector3(53.05f, 1.482f, -13.981f));
            teleported = true;
        }
    }
}
