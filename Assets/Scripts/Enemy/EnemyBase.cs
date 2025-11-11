using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using Animation;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageble
    {
        [SerializeField] private float _currentLife;

        public Collider colliderEnemy;
        public FlashColor flashColor;
        public ParticleSystem particleSystemEnemy;

        public float startLife = 10f;
        public bool lookAtPlayer = false;
        public float destroyDelay = 3f;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvents;

        [Header("Sounds")]
        public SFXType sfxHitType = SFXType.SlimeHit;


        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }


        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();

            if(startWithBornAnimation)
             BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKIll();
        }

        protected virtual void OnKIll()
        {
            if (colliderEnemy != null) colliderEnemy.enabled = false;
            Destroy(gameObject, destroyDelay);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvents?.Invoke();

            var patrol = GetComponent<EnemyPatrol>();
            if (patrol != null)
                patrol.StopPatrol();

        }

        public virtual void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystemEnemy != null) particleSystemEnemy.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= f;

            if (sfxHitType != SFXType.NONE)
            {
                SFXPool.Instance.Play(sfxHitType);
            }

            if (_currentLife <= 0) 
            {
                Kill();
            }
        }



        #region ANIMATION

        private void BornAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion


        //debug

        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
            
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }
    }
}
