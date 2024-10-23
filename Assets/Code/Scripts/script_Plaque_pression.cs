using UnityEngine;

public class PlaqueTrigger : MonoBehaviour
{
    public GameObject sphere1; // Sph�re 1 affect�e
    public GameObject sphere2; // Sph�re 2 affect�e

    private Renderer rendererSphere1;
    private Renderer rendererSphere2;

    public GameObject joueur;

    private bool isYellow1;
    private bool isYellow2;

    private bool isActivated = false; // Indique si la plaque est d�j� activ�e

    private Color Yellow;
    private Color Black;


    void Start()
    {
        rendererSphere1 = sphere1.GetComponent<Renderer>();
        rendererSphere2 = sphere2.GetComponent<Renderer>();

        // Cr�e les couleurs correctement (valeurs entre 0 et 1)
        Yellow = Color.yellow; // Jaune
        Black = Color.black; // Noir
    }

    // Cette m�thode est appel�e quand un objet entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre est bien le joueur et que la plaque n'est pas d�j� activ�e
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Marquer la plaque comme activ�e
            isYellow1 = rendererSphere1.material.color == Yellow;
            isYellow2 = rendererSphere2.material.color == Yellow;
            ChangeSpheresColor();
        }
    }

    // M�thode appel�e quand le joueur quitte la plaque pour r�activer le trigger si n�cessaire
    private void OnTriggerExit(Collider other)
    {
        // Quand le joueur quitte la plaque, r�initialiser la plaque
        if (other.CompareTag("Player"))
        {
            isActivated = false;
        }
    }

    // Cette m�thode alterne la couleur des sph�res
    void ChangeSpheresColor()
    {
        // Alterner la couleur de la premi�re sph�re
        isYellow1 = !isYellow1;
        rendererSphere1.material.color = isYellow1 ? Yellow : Black;

        // Alterner la couleur de la deuxi�me sph�re
        isYellow2 = !isYellow2;
        rendererSphere2.material.color = isYellow2 ? Yellow : Black;

        // V�rifie si toutes les sph�res sont jaunes
        GameManager.Instance.CheckAllSpheres();
    }
}
