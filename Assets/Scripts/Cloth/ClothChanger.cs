using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

namespace Cloth
{

    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D)mesh.sharedMaterials[0].GetTexture(shaderIdName);
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup setup, float duration)
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, setup.text);
            StartCoroutine(ResetAfterDuration(duration));
        }

        public void ResetTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, _defaultTexture);
        }

        private IEnumerator ResetAfterDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            ResetTexture();
        }

    }
}