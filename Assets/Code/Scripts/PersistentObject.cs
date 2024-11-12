using UnityEngine;

namespace Code.Scripts
{
    public class PersistentObject : MonoBehaviour
    {
        public string objectName; // Nom unique de l'objet (id)
        
        private void Awake()
        {
            objectName = gameObject.name;
            if (!string.IsNullOrEmpty(objectName))
            {
                ObjectManager.instance.RegisterObject(objectName, gameObject);
                
            }
        }
    }
}