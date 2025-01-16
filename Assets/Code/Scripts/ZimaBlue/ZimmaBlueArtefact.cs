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

    private int GetSubMeshIndex(Mesh mesh, int triangleIndex)
    {
        int subMeshCount = mesh.subMeshCount;
        //Debug.Log("subMeshCount : " + subMeshCount);

        int globalTriangleIndex = 0;

        // Parcourir chaque sous-mesh pour trouver l'indice global du triangle.
        for (int i = 0; i < subMeshCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);

            // Chaque sous-mesh a un certain nombre de triangles.
            int triangleCount = triangles.Length / 3;

            // V�rifier si le triangleIndex appartient � ce sous-mesh.
            if (triangleIndex >= globalTriangleIndex && triangleIndex < globalTriangleIndex + triangleCount)
            {
                return i; // Retourner l'indice du sous-mesh.
            }

            // Mettre � jour l'indice global du triangle pour le prochain sous-mesh.
            globalTriangleIndex += triangleCount;
        }

        return -1; // Aucun sous-mesh trouv�.
    }

    public void Shoot(ActivateEventArgs arg){
        
        LineRenderer spawProjectile = Instantiate(projectile);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(spawnPoint.position, spawnPoint.forward,out hit, layerMask);
        Vector3 endPoint = Vector3.zero;
        if(hasHit){
            ZimmaBlueColor target = hit.transform.GetComponent<ZimmaBlueColor>();
            if(target != null){
                MeshCollider meshCollider = hit.collider.GetComponent<MeshCollider>();
                MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();

                Mesh mesh = meshCollider.sharedMesh;

                // Obtenir l'indice du triangle touch�.
                int triangleIndex = hit.triangleIndex;

                // Identifier le sous-mesh correspondant.
                int subMeshIndex = GetSubMeshIndex(mesh, triangleIndex);

                target.ChangeColor(spawProjectile.startColor, subMeshIndex);
                endPoint = hit.point;
                //Debug.Log("cible atteinte");
            }
            else{
                endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
                //Debug.Log("cible non atteinte");
            }
        } else {
            endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
            //Debug.Log("cible non atteinte");
        }
        spawProjectile.positionCount = 2;
        spawProjectile.SetPosition(0,spawnPoint.position);
        spawProjectile.SetPosition(1,endPoint);
        Destroy(spawProjectile.gameObject,projectileShow);
    }
    
}
