using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayGame();
    }


    public void QuitGame()
    {
        print("Quit Game!");
        Application.Quit();
    }

    public void PlayGarage()
    {
        
        SceneManager.LoadScene("Garage0");
    }

    public void PlayZimmaBlue()
    {
        SceneManager.LoadScene("ZimmaBlue");
    }

    public void PlayEnnemi()
    {
        SceneManager.LoadScene("Ennemi");
    }

}
