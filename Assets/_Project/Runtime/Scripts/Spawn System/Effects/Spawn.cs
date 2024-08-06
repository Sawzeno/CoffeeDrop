using DG.Tweening;
using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class Spawn : MonoBehaviour{
        [SerializeField] GameObject SpawnVFX;
        [SerializeField] float AnimationDuration = 1f;
        void Start(){
            transform.localScale=   Vector3.zero;
            transform.DOScale(Vector3.one, AnimationDuration).SetEase(Ease.OutBack);
            if(SpawnVFX != null){
                Instantiate(SpawnVFX, transform.position, Quaternion.identity);
            }
            GetComponent<AudioSource>().Play();
        }
    }
}
