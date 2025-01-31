using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class backToMenuHandler : MonoBehaviour
{
    public InputActionReference spawnButton;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.action.started += ButtonWasPressed;
    }
    
    public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<TeleportationManager>().ChangeDimension("MenuScene");
    }
    
}
