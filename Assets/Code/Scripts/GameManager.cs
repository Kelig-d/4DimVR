using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] spheres; // Référence à toutes les sphères
    public GameObject cube; // Cube à changer en rouge

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Vérifie si toutes les sphères sont jaunes
    public void CheckAllSpheres()
    {
        foreach (GameObject sphere in spheres)
        {
            if (sphere.GetComponent<Renderer>().material.color != Color.yellow)
            {
                return; // Si une sphère n'est pas jaune, on arrête la vérification
            }
        }

        // Si toutes les sphères sont jaunes, on change la couleur du cube en rouge
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}
