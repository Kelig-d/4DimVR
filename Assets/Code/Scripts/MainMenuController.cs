using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the main menu functionality, including loading different scenes and quitting the game.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Starts the game by loading the next scene in the build settings.
    /// </summary>
    public void PlayGame()
    {
        // Load the next scene based on the current active scene's build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits the game and prints a message to the console.
    /// </summary>
    public void QuitGame()
    {
        // Log a message to the console
        print("Quit Game!");

        // Quit the application (only works in a built version, not in the editor)
        Application.Quit();
    }

    /// <summary>
    /// Loads the "Garage0" scene.
    /// </summary>
    public void PlayGarage()
    {
        // Load the Garage0 scene
        SceneManager.LoadScene("Garage0");
    }

    /// <summary>
    /// Loads the "ZimmaBlue" scene.
    /// </summary>
    public void PlayZimmaBlue()
    {
        // Load the ZimmaBlue scene
        SceneManager.LoadScene("ZimmaBlue");
    }

    /// <summary>
    /// Loads the "Ennemi" scene.
    /// </summary>
    public void PlayEnnemi()
    {
        // Load the Ennemi scene
        SceneManager.LoadScene("Ennemi");
    }
}