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
    
    private int currentSelectedRadialPart = -1;

    private List<string> TextMenue;
    private bool select;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.action.started += ButtonWasPressed;
    }
    void OnDestroy()
    {
        if (spawnButton != null && spawnButton.action != null)
        {
            spawnButton.action.started -= ButtonWasPressed;
        }
    }
    
    public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        if (this == null) return; // Vérifie si l'objet existe toujours
        StartCoroutine(LoadNewDimension());
        select = true;
    }

    private IEnumerator LoadNewDimension()
    {
        string oldscene = SceneManager.GetActiveScene().name;
        ObjectManager.instance.VoidObjects();

        // Charger la nouvelle scène
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);

        // Attendre que la scène soit chargée
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }
        
        // Désabonner les événements pour éviter les erreurs
        OnDestroy();

        // Désactiver et décharger l'ancienne scène
        SceneManager.UnloadSceneAsync(oldscene);
    }
   
}
