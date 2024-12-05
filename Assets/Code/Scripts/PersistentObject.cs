using UnityEngine;

namespace Code.Scripts
{
    /// <summary>
    /// A script that marks a GameObject as persistent across scenes and registers it in the ObjectManager.
    /// The GameObject's name is used as a unique identifier (ID).
    /// </summary>
    public class PersistentObject : MonoBehaviour
    {
        /// <summary>
        /// The unique name (ID) of the object, typically the GameObject's name.
        /// </summary>
        public string objectName;

        /// <summary>
        /// Called when the object is initialized in the scene.
        /// Registers the object in the ObjectManager with its unique name.
        /// Makes the object persistent across scenes by calling the ObjectManager.
        /// </summary>
        private void Awake()
        {
            // Assign the objectName from the GameObject's name
            objectName = gameObject.name;

            // If objectName is valid, register the object in ObjectManager
            if (!string.IsNullOrEmpty(objectName))
            {
                // Register the GameObject with its unique name
                ObjectManager.instance.RegisterObject(objectName, gameObject);
            }
        }
    }
}