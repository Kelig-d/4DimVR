using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZoneZimaBlue : MonoBehaviour
{
    public GameObject Ghost;

    public Vector3 spawner1;
    public Vector3 spawner2;
    public Vector3 spawner3;
    public Vector3 spawner4;
    public Vector3 spawner5;
    public Vector3 spawner6;
    public Vector3 spawner7;
    public Vector3 spawner8;
    public Vector3 spawner9;
    public Vector3 spawner10;
    List<Vector3> tableauDeVecteur;

    public int nbEnnemies = 0;
    public int nbMaxEnnemies;
    public int nbObjectColored = 0;
    public float timeToWaitAtTheStart;
    public float timeToWaitRespawn;
    private int tempsDiminutionNbMaxEnnemis;


    void Start()
    {
        tableauDeVecteur = new List<Vector3> { spawner1, spawner2, spawner3, spawner4, spawner5, spawner6, spawner7, spawner8, spawner9, spawner10 };
        tempsDiminutionNbMaxEnnemis = 60; // variable qui défini le temps auquel le nombre max d'ennemis va diminué

        // Supprimer les spawners non configurés
        for (int i = tableauDeVecteur.Count - 1; i >= 0; i--)
        {
            Vector3 spawner = tableauDeVecteur[i];
            if (spawner == Vector3.zero)
            {
                tableauDeVecteur.RemoveAt(i);
            }
        }

        // Démarrer la coroutine de spawn des ennemis avec un délai initial
        StartCoroutine(SpawnerEnnemisContinu());
    }

    IEnumerator SpawnerEnnemisContinu()
    {
        // Attendre avant de commencer le premier spawn
        yield return new WaitForSeconds(timeToWaitAtTheStart);

        // Tant que le nombre d'ennemis est inférieur au maximum
        while (true)
        {
            Debug.Log(nbEnnemies+"/" + nbMaxEnnemies);
            // Vérifier si nous pouvons encore faire apparaître des ennemis
            if (nbEnnemies < nbMaxEnnemies)
            {
                // Appeler la méthode pour faire apparaître un ennemi
                SpawnerUnEnnemi();
                
                // Attendre le délai entre les spawns
                yield return new WaitForSeconds(timeToWaitRespawn);
            }
            else
            {
                // Si on atteint le maximum d'ennemis, on attend un peu avant de revérifier
                yield return new WaitForSeconds(1f);
                //Debug.Log(tempsDiminutionNbMaxEnnemis);
                if(tempsDiminutionNbMaxEnnemis <= 0)
                {
                    tempsDiminutionNbMaxEnnemis = 60;
                    if(nbMaxEnnemies > 0){
                        nbMaxEnnemies -= 1;
                    }
                }else{
                    tempsDiminutionNbMaxEnnemis -= 1;
                }
            }
        }
    }

    void SpawnerUnEnnemi()
    {
        // Choisir un point de spawn aléatoire
        int choixAleatoireDuSpawner = Random.Range(0, tableauDeVecteur.Count);

        // Créer l'ennemi à la position choisie
        GameObject instantiated = Instantiate(Ghost);
        instantiated.transform.position = tableauDeVecteur[choixAleatoireDuSpawner];

        // Passer la référence du spawner à l'ennemi pour le décrément de nbEnnemies
        EnnemiZimaBlue ennemiScript = instantiated.GetComponent<EnnemiZimaBlue>();
        ennemiScript.spawnerzone = this;

        // Incrémenter le compteur d'ennemis
        nbEnnemies++;
    }
}