using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

  public class Ennemi : MonoBehaviour
{
  [SerializeField]
  private NavMeshAgent ennemi;

  [SerializeField]
  private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ennemi.SetDestination(player.position);
    }
}
