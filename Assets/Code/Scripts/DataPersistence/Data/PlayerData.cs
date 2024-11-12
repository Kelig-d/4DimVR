
using UnityEngine;

namespace Code.Scripts.DataPersistence.Data
{
    [System.Serializable]
    public class PlayerData : GameData
    {

        public string playerName;
        public float health;
        public float[] position;
        public bool rightHanded;
        
        
        public PlayerData(string playerName, float health, Vector3 position, bool rightHanded)
        {
            isPlayerData = true;
            this.playerName = playerName;
            this.health = health;
            this.position = new float[3] { position.x, position.y, position.z };
            this.rightHanded = rightHanded;
        }

        
    }
}