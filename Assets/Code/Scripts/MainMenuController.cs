using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the main menu interactions and scene transitions.
/// It provides methods to play the game, quit the game, or navigate to specific game scenes like the garage, ZimmaBlue, and Ennemi.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene in the build settings.
    /// This method is called to start the game by loading the scene with the next index in the build settings.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Triggered when a collision occurs.
    /// Calls the <see cref="PlayGame"/> method to start the game upon collision.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    void OnCollisionEnter(Collision collision)
    {
        PlayGame();
    }

    /// <summary>
    /// Quits the game.
    /// Prints a message to the console and then exits the application.
    /// </summary>
    public void QuitGame()
    {
        print("Quit Game!");
        Application.Quit();
    }

    /// <summary>
    /// Loads the "Garage0" scene.
    /// This method is called to navigate to the garage scene when selected.
    /// </summary>
    public void PlayGarage()
    {
        SceneManager.LoadScene("Garage0");
    }

    /// <summary>
    /// Loads the "ZimmaBlue" scene.
    /// This method is called to navigate to the ZimmaBlue scene when selected.
    /// </summary>
    public void PlayZimmaBlue()
    {
        SceneManager.LoadScene("ZimmaBlue");
    }

    /// <summary>
    /// Loads the "Ennemi" scene.
    /// This method is called to navigate to the Ennemi scene when selected.
    /// </summary>
    public void PlayEnnemi()
    {
        SceneManager.LoadScene("Ennemi");
    }
}