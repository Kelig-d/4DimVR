using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ponéglyphe : MonoBehaviour
{
    /// <summary>
    /// The activators (GameObjects) that the script checks for activation states.
    /// </summary>
    public GameObject Activator0;
    public GameObject Activator1;
    public GameObject Activator2;
    public GameObject Activator3;
    public GameObject Activator4;
    public GameObject Activator5;
    public GameObject Activator6;
    public GameObject Activator7;
    public GameObject Activator8;
    public GameObject Activator9;

    /// <summary>
    /// Activation states for each of the activators.
    /// </summary>
    private bool Active0;
    private bool Active1;
    private bool Active2;
    private bool Active3;
    private bool Active4;
    private bool Active5;
    private bool Active6;
    private bool Active7;
    private bool Active8;
    private bool Active9;

    /// <summary>
    /// The target activation states (1 for active, 0 for inactive) for each activator.
    /// </summary>
    private int[] res = { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };

    // Start is called before the first frame update
    void Start()
    {
        // Initialization logic (currently empty)
    }

    // Update is called once per frame
    void Update()
    {
        // Check the activation state of each activator and compare it with the target states
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

        // If all activators are in the correct state, destroy the object
        if (Active0 && Active1 && Active2 && Active3 && Active4 && Active5 && Active6 && Active7 && Active8 && Active9)
        {
            Destroy(gameObject); // Destroy the object
            Debug.Log("Objet détruit"); // Log destruction message
        }
    }

    /// <summary>
    /// Checks the activation state of a given activator GameObject based on its rotation.
    /// </summary>
    /// <param name="Activator">The GameObject whose activation state needs to be checked.</param>
    /// <returns>1 if the activator is in the active state, 0 if inactive, or 2 for an undefined state.</returns>
    int checkActivation(GameObject Activator)
    {
        // If the activator's rotation is between 270 and 315 degrees, it is considered active
        if (Activator.transform.eulerAngles.x > 270 && Activator.transform.eulerAngles.x < 315)
        {
            return 1;
        }
        else
        {
            // If the activator's rotation is between 30 and 60 degrees, it is considered inactive
            if (Activator.transform.eulerAngles.x > 30 && Activator.transform.eulerAngles.x < 60)
            {
                return 0;
            }
            else
            {
                // Any other state is considered undefined (state 2)
                return 2;
            }
        }
    }
}
