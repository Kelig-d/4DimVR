using UnityEngine;

public class LumiereAmbianceMi7_2 : MonoBehaviour
{
    // Limites pour x et z
    public float minX = -200f;
    public float maxX = 120f;
    public float minZ = -620f;
    public float maxZ = 100f;

    // Limites pour y
    public float minY = 138f;
    public float maxY = 315f;

    // Vitesse de déplacement en Y
    public float ySpeed = 5f;

    // Temps d'attente avant de changer de position aléatoire (x, z)
    public float randomMoveInterval = 2f;

    private float targetY; // Valeur cible pour Y
    private Vector3 targetPosition; // Nouvelle position cible
    private bool goingUp = true; // Direction pour Y
    private float nextMoveTime; // Temps avant de changer de position

    void Start()
    {
        // Initialisation de la position cible
        targetPosition = transform.position;
        targetY = transform.position.y;
    }

    void Update()
    {
        // Gérer le mouvement régulier en Y
        UpdateYMovement();

        // Gérer le déplacement aléatoire en X et Z
        if (Time.time > nextMoveTime)
        {
            UpdateRandomPosition();
            nextMoveTime = Time.time + randomMoveInterval; // Planifier le prochain changement
        }

        // Déplacer l'objet vers la position cible (avec interpolation fluide)
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetY, targetPosition.z), Time.deltaTime * 2f);
    }

    private void UpdateYMovement()
    {
        // Déplacer Y entre les limites de manière régulière
        if (goingUp)
        {
            targetY += ySpeed * Time.deltaTime;
            if (targetY >= maxY)
            {
                targetY = maxY;
                goingUp = false;
            }
        }
        else
        {
            targetY -= ySpeed * Time.deltaTime;
            if (targetY <= minY)
            {
                targetY = minY;
                goingUp = true;
            }
        }
    }

    private void UpdateRandomPosition()
    {
        // Choisir une nouvelle position aléatoire pour X et Z
        targetPosition.x = Random.Range(minX, maxX);
        targetPosition.z = Random.Range(minZ, maxZ);
    }
}
