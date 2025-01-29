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
            Debug.Log(GlobalManager.Instance.tutorialDone);
            SceneManager.LoadScene(GlobalManager.Instance.tutorialDone ? "Berceau" : "Garage0" );
            player.transform.position = new Vector3(1.5f,0f,6f);
        }
            
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayGame(collision.gameObject);
    }
    

}
