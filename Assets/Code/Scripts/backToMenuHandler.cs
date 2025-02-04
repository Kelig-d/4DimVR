using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Code.Scripts
{
    public class BackToMenuHandler : MonoBehaviour
    {
        public InputActionReference spawnButton;
    
        // Start is called before the first frame update
        void Start()
        {
            spawnButton.action.started += ButtonWasPressed;
        }
    
        public void ButtonWasPressed(InputAction.CallbackContext context)
        {
            if(SceneManager.GetActiveScene().name =="MenuScene") gameObject.GetComponentInChildren<XROrigin>().gameObject.transform.position = Vector3.zero;
            else gameObject.GetComponent<TeleportationManager>().ChangeDimension("MenuScene",Vector3.zero);
        }
    
    }
}
