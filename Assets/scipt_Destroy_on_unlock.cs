using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    // Cette méthode est appelée lorsque la porte entre en collision avec un autre objet
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet avec lequel on entre en collision a le tag "key"
        if (other.CompareTag("Key"))
        {
            // Désactive la porte et la clé en les détruisant
            Destroy(gameObject); // La porte
            Destroy(other.gameObject); // La clé
        }
    }
}
