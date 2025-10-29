using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            gunBase.StartShot();
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            Init();
            Debug.Log("Inimigo Surgiu");
        }


    }

}
