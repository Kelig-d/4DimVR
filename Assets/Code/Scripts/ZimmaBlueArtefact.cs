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
        
        LineRenderer spawProjectile = Instantiate(projectile);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(spawnPoint.position, spawnPoint.forward,out hit, layerMask);
        Vector3 endPoint = Vector3.zero;
        if(hasHit){
            EffetZimmaBlue target = hit.transform.GetComponent<EffetZimmaBlue>();
            if (target != null){
                target.ChangeColor(spawProjectile.startColor);
                endPoint = hit.point;
                Debug.Log("cible atteinte");
            }else{
                endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
                Debug.Log("cible non atteinte");
            }
        } else {
            endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
            Debug.Log("cible non atteinte");
        }
        spawProjectile.positionCount = 2;
        spawProjectile.SetPosition(0,spawnPoint.position);
        spawProjectile.SetPosition(1,endPoint);
        Destroy(spawProjectile.gameObject,projectileShow);
    }
    
}
