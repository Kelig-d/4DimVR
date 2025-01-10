using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCorect : MonoBehaviour
{

    public bool Berceau = false;

    public void PlayGame()
    {
        if ( Berceau)
            SceneManager.LoadScene("Berceau");
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayGame();
    }

}
