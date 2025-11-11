using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{

    public class ItemCollactableBase : MonoBehaviour
    {
        public ItemType itemType;

        public SFXType sfxType;


        public string comparteTag = "Player";
        public ParticleSystem itemParticleSystem;
        //public float timeToHide = 3;
        public GameObject graphicItem;

        public Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;


        private void Awake()
        {
            //if (itemParticleSystem != null) itemParticleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(comparteTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }



        protected virtual void Collect()
        {
            PlaySFX();
            if(collider != null) collider.enabled = false;
            //Debug.Log("Collect");
            if(graphicItem != null) graphicItem.SetActive(false);
            //Invoke(nameof(HideObject), timeToHide);
            OnCollect();
            gameObject.SetActive(false);

        }

        //private void HideObject()
        //{
        //    gameObject.SetActive(false);
        //}


        protected virtual void OnCollect()
        {
            if (itemParticleSystem != null) itemParticleSystem.Play();
            if (audioSource != null && audioSource.clip != null)
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            ItemManager.Instance.AddByType(itemType);

        }
    }
}
