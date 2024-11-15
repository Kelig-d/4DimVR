using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    // Cette m�thode est appel�e lorsque la porte entre en collision avec un autre objet
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet avec lequel on entre en collision a le tag "key"
        if (other.CompareTag("Key"))
        {
            // D�sactive la porte et la cl� en les d�truisant
            Destroy(gameObject); // La porte
            Destroy(other.gameObject); // La cl�
        }
    }
}
