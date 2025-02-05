using UnityEngine;

namespace Code.Scripts
{
    public class MainMenuCorect : MonoBehaviour
    {
    
        public void PlayGame(GameObject player)
        {

            if (GlobalManager.Instance.tutorialDone)
            {
                player.GetComponent<TeleportationManager>().ChangeDimension("Berceau",new Vector3(24.112f,1.757f,3.1f));
            }
            else
            {
                player.GetComponent<TeleportationManager>().ChangeDimension("Garage0",new Vector3(10,1.7f,10f));
            }
            
        }

        void OnCollisionEnter(Collision collision)
        {
            PlayGame(collision.gameObject.transform.parent.gameObject);
        }
    

    }
}
