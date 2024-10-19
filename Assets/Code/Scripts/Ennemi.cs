using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

  public class Ennemi : MonoBehaviour
{
  private NavMeshAgent ennemi;

  [SerializeField]
  private Transform player;

  private float Distance; 
  public float distancePoursuite = 10;
    // Start is called before the first frame update
    void Start()
    {
        ennemi = gameObject.GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(player.position, transform.position);
        Debug.LogWarning("Distance : "+Distance);
        // ennemi loin, pas dans le perimetre 
        /*
        if(Distance > distancePoursuite){
          // a voir si l'ennemi doit boouger où rester immobile 
        }
        */
        // ennemi dans le perimetre
        if (Distance < distancePoursuite){
          ennemi.destination = player.position;
        }
    }

}
