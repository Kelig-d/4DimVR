using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RadialSelection : MonoBehaviour
{
    public InputActionReference spawnButton;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.action.started += ButtonWasPressed;
    }
    
    public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        StartCoroutine(LoadNewDimension());


    }

    private IEnumerator LoadNewDimension()
    {
        string oldscene = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        // Désactiver l'ancienne scène
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }

        SceneManager.UnloadSceneAsync(oldscene);
    }
    
}
