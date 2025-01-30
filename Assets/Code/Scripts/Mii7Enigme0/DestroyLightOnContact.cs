using UnityEngine;

namespace Code.Scripts.Mii7Enigme0
{
    public class DestroyLightOnContact : MonoBehaviour
    {

        public new Light light;

        private void OnTriggerEnter(Collider other) {this.light.transform.localScale = Vector3.zero;}
    }
}
