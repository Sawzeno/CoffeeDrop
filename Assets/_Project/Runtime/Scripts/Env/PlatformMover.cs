using UnityEngine;
using DG.Tweening;
using System;

namespace Game
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] Vector3 MoveTo = Vector3.zero;
        [SerializeField] float MoveTime =   1f;
        [SerializeField] Ease ease = Ease.InOutQuad;
        Vector3 StartPosition;

        void Start(){
            StartPosition = transform.position;
            Move();
        }

         void Move()
        {
            transform.DOMove(StartPosition + MoveTo, MoveTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
