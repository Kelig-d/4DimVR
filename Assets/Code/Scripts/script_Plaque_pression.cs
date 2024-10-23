using UnityEngine;

public class PlaqueTrigger : MonoBehaviour
{
    public GameObject sphere1; // Sphère 1 affectée
    public GameObject sphere2; // Sphère 2 affectée

    private Renderer rendererSphere1;
    private Renderer rendererSphere2;

    public GameObject joueur;

    private bool isYellow1;
    private bool isYellow2;

    private bool isActivated = false; // Indique si la plaque est déjà activée

    private Color Yellow;
    private Color Black;


    void Start()
    {
        rendererSphere1 = sphere1.GetComponent<Renderer>();
        rendererSphere2 = sphere2.GetComponent<Renderer>();

        // Crée les couleurs correctement (valeurs entre 0 et 1)
        Yellow = Color.yellow; // Jaune
        Black = Color.black; // Noir
    }

    // Cette méthode est appelée quand un objet entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre est bien le joueur et que la plaque n'est pas déjà activée
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Marquer la plaque comme activée
            isYellow1 = rendererSphere1.material.color == Yellow;
            isYellow2 = rendererSphere2.material.color == Yellow;
            ChangeSpheresColor();
        }
    }

    // Méthode appelée quand le joueur quitte la plaque pour réactiver le trigger si nécessaire
    private void OnTriggerExit(Collider other)
    {
        // Quand le joueur quitte la plaque, réinitialiser la plaque
        if (other.CompareTag("Player"))
        {
            isActivated = false;
        }
    }

    // Cette méthode alterne la couleur des sphères
    void ChangeSpheresColor()
    {
        // Alterner la couleur de la première sphère
        isYellow1 = !isYellow1;
        rendererSphere1.material.color = isYellow1 ? Yellow : Black;

        // Alterner la couleur de la deuxième sphère
        isYellow2 = !isYellow2;
        rendererSphere2.material.color = isYellow2 ? Yellow : Black;

        // Vérifie si toutes les sphères sont jaunes
        GameManager.Instance.CheckAllSpheres();
    }
}
