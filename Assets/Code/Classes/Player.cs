using Unity.XR.CoreUtils;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Player";

    public float health { get; private set; } = 100f;    
    public float speed = 2f;
    public float damage = 1f;
    public bool rightHanded = true;
    private bool dead = false;
    public XROrigin xOrigin;
    public GameObject healthBar;
    private HealthBar healthBarScript;

    public bool InMenu = true;  
    public Vector3 SpawnPoint = new Vector3(0,0,0);
    public Vector3 SpawnPointMenu = new Vector3(0,0,0);
    
    private void Start()
    {
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }
        if (xOrigin.transform.position.y < -10)
        {
            xOrigin.transform.position = this.SpawnPoint;
        }

        if (dead)
        {
            Revive();

        }
    }

    public void TakeDamage(float tookDamage)
    {
        health -= tookDamage;
        healthBarScript.UpdateHealth(health);
    }

    private void Revive()
    {
        xOrigin.transform.position = this.SpawnPoint;
        health = 100;
        dead = false;
        healthBarScript.UpdateHealth(health);
    }
    
    public void ChangeSpawnPoint(GameObject newSpawnPoint, bool Menu)
    {
        this.InMenu = Menu;
        if (Menu)
        {
            this.SpawnPointMenu = newSpawnPoint.transform.position;
        }
        else
        {
            this.SpawnPoint = newSpawnPoint.transform.position;

        }
    }
    
}
