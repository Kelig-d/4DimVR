using UnityEngine;
using System.Collections;

public class PortalEffect : MonoBehaviour
{
    public GameObject portal; // Référence au portail (particules ou prefab)
    public GameObject monster; // Référence au monstre
    public Animator portalAnimator; // Référence à l’Animator pour le portail
    public Animator monsterAnimator; // Référence à l’Animator pour le monstre (pour dissolve ou autres animations)
    public float delayBeforeMonster = 1.0f; // Temps avant l'apparition du monstre
    public float scaleAnimationDuration = 0.5f; // Durée de l'animation d'échelle

    private Material monsterMaterial; // Référence au matériau du monstre pour le dissolve effect
    private bool isDissolving = false; // Pour vérifier si le dissolve est en cours
    private float dissolveProgress = 1.0f; // Progrès de l'effet dissolve (1 = invisible, 0 = visible)

    void Start()
    {
        // Désactiver le monstre au début
        monster.SetActive(false);

        // Récupérer le matériau du monstre pour l'effet de dissolve
        if (monster.TryGetComponent<Renderer>(out Renderer renderer))
        {
            monsterMaterial = renderer.material;
            if (monsterMaterial.HasProperty("_DissolveAmount"))
            {
                monsterMaterial.SetFloat("_DissolveAmount", 1.0f); // Commence complètement "dissous"
            }
        }

        // Activer le portail et démarrer l'animation
        portal.SetActive(true);
        if (portalAnimator != null)
        {
            portalAnimator.SetTrigger("Open"); // Animation d'ouverture du portail
        }

        // Lancer l’apparition du monstre après un délai
        Invoke("SpawnMonster", delayBeforeMonster);
    }

    void Update()
    {
        // Si l'effet dissolve est actif, faire la transition
        if (isDissolving)
        {
            dissolveProgress = Mathf.MoveTowards(dissolveProgress, 0.0f, Time.deltaTime * 1.5f); // Transition douce
            monsterMaterial.SetFloat("_DissolveAmount", dissolveProgress);

            if (dissolveProgress <= 0.0f)
            {
                isDissolving = false; // Terminé
            }
        }
    }

    void SpawnMonster()
    {
        // Activer le monstre
        monster.SetActive(true);

        // Lancer l'animation de scale avec une coroutine
        StartCoroutine(ScaleOverTime(monster, Vector3.one, scaleAnimationDuration));

        // Lancer l'effet de dissolve
        if (monsterMaterial != null && monsterMaterial.HasProperty("_DissolveAmount"))
        {
            isDissolving = true;
            dissolveProgress = 1.0f; // Commencer complètement "dissous"
        }

        // Lancer l'animation du monstre (si nécessaire)
        if (monsterAnimator != null)
        {
            monsterAnimator.SetTrigger("Appear");
        }
    }

    IEnumerator ScaleOverTime(GameObject obj, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = obj.transform.localScale; // Échelle initiale
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolation linéaire entre l'échelle initiale et la cible
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre le prochain frame
        }

        // Assurer que l'échelle finale est bien atteinte
        obj.transform.localScale = targetScale;
    }
}
