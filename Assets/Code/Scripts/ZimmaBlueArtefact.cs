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
        Debug.Log("subMeshCount : " + subMeshCount);

        int globalTriangleIndex = 0;

        // Parcourir chaque sous-mesh pour trouver l'indice global du triangle.
        for (int i = 0; i < subMeshCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);

            // Chaque sous-mesh a un certain nombre de triangles.
            int triangleCount = triangles.Length / 3;

            // Vérifier si le triangleIndex appartient à ce sous-mesh.
            if (triangleIndex >= globalTriangleIndex && triangleIndex < globalTriangleIndex + triangleCount)
            {
                return i; // Retourner l'indice du sous-mesh.
            }

            // Mettre à jour l'indice global du triangle pour le prochain sous-mesh.
            globalTriangleIndex += triangleCount;
        }

        return -1; // Aucun sous-mesh trouvé.
    }

    public void Shoot(ActivateEventArgs arg){
        
        LineRenderer spawProjectile = Instantiate(projectile);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(spawnPoint.position, spawnPoint.forward,out hit, layerMask);
        Vector3 endPoint = Vector3.zero;
        if(hasHit){
            //EffetZimmaBlue target = hit.transform.GetComponent<EffetZimmaBlue>();
            ZimmaBlueJustColor target2 = hit.transform.GetComponent<ZimmaBlueJustColor>();
            /*if (target != null)
            {
                Shader targetMaterial = hit.transform.GetComponent<Shader>();

                target.ChangeColor(spawProjectile.startColor, targetMaterial);
                endPoint = hit.point;
                //Debug.Log("cible atteinte speciale");
            }else*/
            if(target2 != null){

                MeshCollider meshCollider = hit.collider.GetComponent<MeshCollider>();
                MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();

                Mesh mesh = meshCollider.sharedMesh;

                // Obtenir l'indice du triangle touché.
                int triangleIndex = hit.triangleIndex;

                // Identifier le sous-mesh correspondant.
                int subMeshIndex = GetSubMeshIndex(mesh, triangleIndex);

                target2.ChangeColor(spawProjectile.startColor, subMeshIndex);
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
