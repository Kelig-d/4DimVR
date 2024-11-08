using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] spheres; // R�f�rence � toutes les sph�res
    public GameObject cube; // Cube � changer en rouge

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // V�rifie si toutes les sph�res sont jaunes
    public void CheckAllSpheres()
    {
        foreach (GameObject sphere in spheres)
        {
            if (sphere.GetComponent<Renderer>().material.color != Color.yellow)
            {
                return; // Si une sph�re n'est pas jaune, on arr�te la v�rification
            }
        }

        // Si toutes les sph�res sont jaunes, on change la couleur du cube en rouge
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}
