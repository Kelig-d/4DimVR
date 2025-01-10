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

    [Range(2,10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public Transform handTransform;
    public float angleBetweenPart = 10;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
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
        
        //SpawnRadialPart();
        select = true;

    }

    private IEnumerator LoadNewDimension()
    {
        string oldscene = SceneManager.GetActiveScene().name;
        ObjectManager.instance.VoidObjects();
        // Charger la nouvelle scène
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        // Désactiver l'ancienne scène
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }
        OnDestroy();

        SceneManager.UnloadSceneAsync(oldscene);
    }
   
}
