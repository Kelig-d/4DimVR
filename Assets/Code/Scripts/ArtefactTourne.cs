using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtefactTourne : MonoBehaviour
{
    public GameObject Artefact;

    bool rotatY = false;
    double rotateY = 0;

    public GameObject Cone0;
    public GameObject Activator0;
    public bool up0;
    public  Color C0;
    bool Activer0;

    public GameObject Cone1;
    public Color C1;
    public GameObject Activator1;
    public bool up1;
    bool Activer1;


    public GameObject Cone2;
    public Color C2;
    public GameObject Activator2;
    public bool up2;
    bool Activer2;



    public GameObject Cone3;
    public Color C3;
    public GameObject Activator3;
    public bool up3;
    bool Activer3;




    void Start()
    {
         
        
    }

    // Update is called once per frame
    void Update()
    {
        FixedUpdate();
    }

    static bool setTP = true;
    private IEnumerator SetTp()
    {
        if (setTP)
        {
            GlobalManager.Instance.tutorialDone = true;
            print("top");
            setTP = false;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Berceau", LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                yield return null; // Attendre la prochaine frame
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(53.05f, 1.482f, -13.981f);
            SceneManager.UnloadSceneAsync("Garage0");
        }


    }

    bool CheckUpdateColor(GameObject test, Color colortot, bool up, GameObject ItemVerif)
    {
        if (up)
        {
            if(ItemVerif.transform.eulerAngles.x >270 && ItemVerif.transform.eulerAngles.x < 315)
            {
                test.GetComponent<Renderer>().material.color = colortot;
                return true;

            }
            else
            {
                test.GetComponent<Renderer>().material.color = Color.white;
                return false;

            }


        }
        else
        {
            if (ItemVerif.transform.eulerAngles.x > 30 && ItemVerif.transform.eulerAngles.x < 60)
            {
                test.GetComponent<Renderer>().material.color = colortot;
                return true;


            }
            else
            {
                test.GetComponent<Renderer>().material.color = Color.white;
                return false;


            }


        }
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

        Activer0 = CheckUpdateColor(Cone0 , C0, up0, Activator0);
        Activer1 = CheckUpdateColor(Cone1 , C1, up1, Activator1);
        Activer2 = CheckUpdateColor(Cone2 , C2, up2, Activator2);
        Activer3 = CheckUpdateColor(Cone3 , C3, up3, Activator3);

        if( Activer0 && Activator1 && Activer2 && Activer3)
        {
           StartCoroutine(SetTp());
        }
    }
}
