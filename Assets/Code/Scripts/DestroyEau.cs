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
        if (collision.gameObject.CompareTag("sol")) // V�rifie si �a touche le sol
        {
            Destroy(gameObject); // D�truit la goutte
        }
    }
}
