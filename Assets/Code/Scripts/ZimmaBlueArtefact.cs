using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

using UnityEngine;

public class ZimmaBlueArtefact : MonoBehaviour
{
    //public GameObject projectile;
    public LineRenderer projectile;
    public Transform spawnPoint;
    public float fireDistance = 10;
    public float projectileShow = 0.5f;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Shoot);
    }

    void Update()
    {

        
    }

    public void Shoot(ActivateEventArgs arg){
        // if GameObject
        /*
        GameObject spawProjectile = Instantiate(projectile);
        spawProjectile.transform.position = spawnPoint.position;
        spawProjectile.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireDistance;
        Destroy(spawProjectile.gameObject,5.0f);
        */

        // if LineRenderer
        RaycastHit hit;
        bool hasHit = Physics.Raycast(spawnPoint.position, spawnPoint.forward,out hit, layerMask);
        Vector3 endPoint = Vector3.zero;
        if(hasHit){
            EffetZimmaBlue target = hit.transform.GetComponent<EffetZimmaBlue>();
            if (target != null){
                target.ChangeColor();
                endPoint = hit.point;
                Debug.Log("AHH");
            }else{
                endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
                Debug.Log("DDS");
            }
        } else {
            endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
            Debug.Log("DDS");
        }

        LineRenderer spawProjectile = Instantiate(projectile);
        spawProjectile.positionCount = 2;
        spawProjectile.SetPosition(0,spawnPoint.position);
        spawProjectile.SetPosition(1,endPoint);
        Destroy(spawProjectile.gameObject,projectileShow);
    }

    
}
