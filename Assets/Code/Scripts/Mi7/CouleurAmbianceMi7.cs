using UnityEngine;

public class CouleurAmbianceMi7 : MonoBehaviour
{
    public Light directionalLight; // Référence à la lumière
    public float colorCycleSpeed = 0.5f; // Vitesse de transition entre les deux couleurs

    // Les deux couleurs spécifiées (converties en pourcentage 0-1 pour RGB)
    private Color color1 = new Color(72f / 255f, 30f / 255f, 64f / 255f); 
    private Color color2 = new Color(25f / 255f, 18f / 255f, 38f / 255f);

    private float t = 0f; // Interpolation entre les deux couleurs
    private bool goingToColor2 = true; // Direction de la transition

    private void Start()
    {
        // Si la lumière n'est pas assignée, cherche un composant Light automatiquement
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }

        // Vérifie que la lumière est bien assignée
        if (directionalLight == null)
        {
            Debug.LogError("Aucun composant Light trouvé sur cet objet !");
        }
    }

    private void Update()
    {
        if (directionalLight != null)
        {
            // Transition fluide entre color1 et color2
            if (goingToColor2)
            {
                t += Time.deltaTime * colorCycleSpeed; // Augmente la valeur d'interpolation
                if (t >= 1f)
                {
                    t = 1f; // Limite la valeur de t
                    goingToColor2 = false; // Change de direction
                }
            }
            else
            {
                t -= Time.deltaTime * colorCycleSpeed; // Diminue la valeur d'interpolation
                if (t <= 0f)
                {
                    t = 0f; // Limite la valeur de t
                    goingToColor2 = true; // Change de direction
                }
            }

            // Interpolation linéaire entre les deux couleurs
            directionalLight.color = Color.Lerp(color1, color2, t);
        }
    }
}
