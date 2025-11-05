using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColort;

    private Tween _currTween;


    private void OnValidate()
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

    }

    private void Awake()
    {
        if (meshRenderer != null)
            defaultColort = meshRenderer.sharedMaterial.GetColor("_EmissionColor");

        if (skinnedMeshRenderer != null)
            defaultColort = skinnedMeshRenderer.sharedMaterial.GetColor("_EmissionColor");
    }


    //private void Start()
    //{
    //    defaultColort = meshRenderer.material.GetColor("_EmissionColor");
    //}

    [NaughtyAttributes.Button]
    public void Flash()
    {
        //if (!Application.isPlaying) return;

        //if (_currTween != null && _currTween.IsActive())
        //    _currTween.Kill();

        //_currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);


        if (_currTween != null && _currTween.IsActive())
            _currTween.Kill();


        if (meshRenderer != null)
        {
            meshRenderer.sharedMaterial.DOColor(color, "_EmissionColor", duration)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() => meshRenderer.sharedMaterial.SetColor("_EmissionColor", defaultColort));
        }

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.sharedMaterial.DOColor(color, "_EmissionColor", duration)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() => skinnedMeshRenderer.sharedMaterial.SetColor("_EmissionColor", defaultColort));
        }


    }

}
