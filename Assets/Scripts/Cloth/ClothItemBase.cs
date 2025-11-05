using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 2f;

        public string compareTag = "Player";

        private bool _collected = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            if (_collected) return;
            _collected = true;

            Debug.Log("Collect");

            var setup = ClothManager.Instance.GetSetupByType(clothType);
            Player.Instance.ChangeTexture(setup, duration);

            HideObject();

        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
