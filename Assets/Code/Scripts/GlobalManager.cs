using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    private bool _tutorialDone;
    public bool tutorialDone
    {
        get => _tutorialDone;
        set
        {
            Debug.Log("tutorialDone changé: " + value);
            _tutorialDone = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Empêche d’avoir plusieurs instances
            return;
        }
    }
}