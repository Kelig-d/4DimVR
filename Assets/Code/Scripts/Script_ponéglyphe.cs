using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ponéglyphe : MonoBehaviour
{
    public GameObject Activator0;
    private bool Active0;
    public GameObject Activator1;
    private bool Active1;
    public GameObject Activator2;
    private bool Active2;
    public GameObject Activator3;
    private bool Active3;
    public GameObject Activator4;
    private bool Active4;
    public GameObject Activator5;
    private bool Active5;
    public GameObject Activator6;
    private bool Active6;
    public GameObject Activator7;
    private bool Active7;
    public GameObject Activator8;
    private bool Active8;
    public GameObject Activator9;
    private bool Active9;

    private int[] res = { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Active0 = checkActivation(Activator0) == res[0];
        Active1 = checkActivation(Activator1) == res[1];
        Active2 = checkActivation(Activator2) == res[2];
        Active3 = checkActivation(Activator3) == res[3];
        Active4 = checkActivation(Activator4) == res[4];
        Active5 = checkActivation(Activator5) == res[5];
        Active6 = checkActivation(Activator6) == res[6];
        Active7 = checkActivation(Activator7) == res[7];
        Active8 = checkActivation(Activator8) == res[8];
        Active9 = checkActivation(Activator9) == res[9];

        if (Active0 && Active1 && Active2 && Active3 && Active4 && Active5 && Active6 && Active7 && Active8 && Active9)
        {
            Destroy(gameObject);
            Debug.Log("Objet détruit");
        }
    }

    int checkActivation(GameObject Activator)
    {
        if (Activator.transform.eulerAngles.x > 270 && Activator.transform.eulerAngles.x < 315)
        {
            return 1;

        }
        else { 
            if (Activator.transform.eulerAngles.x > 30 && Activator.transform.eulerAngles.x < 60) {
                return 0;
            }
            else
            {
                return 2;
            }
        }
    }


}
