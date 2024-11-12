using Code.Scripts;
using Code.Scripts.DataPersistence.Data;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Classes
{
    public class Player : MonoBehaviour
    {   [Save]     
        public string playerName = "Player";
        [Save]
        public float health { get; internal set; } = 100f;    
        [Save]
        public bool rightHanded = true;
        [Save]
        protected Vector3 xOriginPosition;
        
        public float speed = 2f;
        public float damage = 1f;
        private bool dead = false;
        public XROrigin xOrigin;
        public GameObject healthBar;
        private HealthBar healthBarScript;
    
        private void Start()
        {
            // Charger les données sauvegardées dès le démarrage de l'application
            SaveManager.Instance.LoadPlayer(this);
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
                xOrigin.transform.position = new Vector3(10, 15, 10);
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
            xOrigin.transform.position = new Vector3(2, 5, 2);
            health = 100;
            dead = false;
            healthBarScript.UpdateHealth(health);
        }

        private void OnApplicationQuit()
        {
            PlayerData data = new PlayerData(playerName, health,xOrigin.transform.position, rightHanded);
            SaveManager.Instance.saveData(data);
        }


    }
}
