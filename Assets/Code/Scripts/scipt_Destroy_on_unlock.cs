using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    /// <summary>
    /// This method is called when another collider enters the trigger zone of the door.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object that collided with the door has the tag "Key"
        if (other.CompareTag("Key"))
        {
            // Destroys both the door and the key when the collision occurs
            Destroy(gameObject); // Destroy the door
            Destroy(other.gameObject); // Destroy the key
        }
    }
}