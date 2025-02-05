using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEau : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("sol")) // Vérifie si ça touche le sol
        {
            Destroy(gameObject); // Détruit la goutte
        }
    }
}
