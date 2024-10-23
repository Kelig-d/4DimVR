using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public string playerName = "Player";
    
    public float health = 100f;
    public float speed = 2f;
    public float damage = 1f;
    public bool rightHanded = true;
    private bool dead = false;
    public XROrigin xOrigin;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }
        if (xOrigin.transform.position.y < -10)
        {
            xOrigin.transform.position = new Vector3(10, 15, 10);
        }
    }
    
    
}
