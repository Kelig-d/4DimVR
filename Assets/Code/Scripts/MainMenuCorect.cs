using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCorect : MonoBehaviour
{

    public bool Berceau = false;

    public void PlayGame(GameObject player)
    {
        if (Berceau)
        {
            if (GlobalManager.Instance.tutorialDone)
            {
                SceneManager.LoadScene("Berceau");
                player.transform.position = new Vector3(24.112f,1.757f,3.1f);
            }
            else
            {
                SceneManager.LoadScene("Garage0");
                player.transform.position = new Vector3(1.5f,0f,6f);
            }
        }
            
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayGame(collision.gameObject);
    }
    

}
