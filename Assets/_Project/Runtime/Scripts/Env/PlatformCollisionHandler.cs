using UnityEngine;

namespace Game
{
    //when we collide with a platform we want the platform to become the parent of  the Player
    public class PlatformCollisionHandler : MonoBehaviour
    {
        Transform platform ;

        void OnCollisionEnter(Collision other){
            // only if the  contact is from above 
            if(other.gameObject.CompareTag("MovingPlatform")){
                ContactPoint contact = other.GetContact(0);
                if(contact.normal.y < 0.5f) return;
                platform = other.transform;
                transform.SetParent(platform);
            }
        }
        void OnCollisionExit(Collision other){
            if(other.gameObject.CompareTag("MovingPlatform")){
                transform.SetParent(null);
                platform = null;
                
            }
        }

    }
}
