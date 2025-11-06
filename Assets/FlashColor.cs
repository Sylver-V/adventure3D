//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class FlashColor : MonoBehaviour
//{
//    public MeshRenderer meshRenderer;
//    public SkinnedMeshRenderer skinnedMeshRenderer;

//    [Header("Setup")]
//    public Color color = Color.red;
//    public float duration = .1f;

//    public string colorParameter = "_EmissionColor";

//    private Color defaultColort;

//    private Tween _currTween;


//    private void OnValidate()
//    {
//        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
//        if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

//    }

//    private void Awake()
//    {
//        if (meshRenderer != null)
//            defaultColort = meshRenderer.sharedMaterial.GetColor(colorParameter);

//        if (skinnedMeshRenderer != null)
//            defaultColort = skinnedMeshRenderer.sharedMaterial.GetColor(colorParameter);
//    }


//    //private void Start()
//    //{
//    //    defaultColort = meshRenderer.material.GetColor("_EmissionColor");
//    //}

//    [NaughtyAttributes.Button]
//    public void Flash()
//    {
//        //if (!Application.isPlaying) return;

//        //if (_currTween != null && _currTween.IsActive())
//        //    _currTween.Kill();

//        //_currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);


//        if (_currTween != null && _currTween.IsActive())
//            _currTween.Kill();


//        if (meshRenderer != null)
//        {
//            meshRenderer.sharedMaterial.DOColor(color, colorParameter, duration)
//                .SetLoops(2, LoopType.Yoyo)
//                .OnKill(() => meshRenderer.sharedMaterial.SetColor(colorParameter, defaultColort));
//        }

//        if (skinnedMeshRenderer != null)
//        {
//            skinnedMeshRenderer.sharedMaterial.DOColor(color, colorParameter, duration)
//                .SetLoops(2, LoopType.Yoyo)
//                .OnKill(() => skinnedMeshRenderer.sharedMaterial.SetColor(colorParameter, defaultColort));
//        }


//    }

//}


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
    public string colorParameter = "_EmissionColor";

    private Color defaultColor;
    private Coroutine currentFlash;

    private void Awake()
    {
        if (meshRenderer != null)
            defaultColor = meshRenderer.sharedMaterial.GetColor(colorParameter);

        if (skinnedMeshRenderer != null)
            defaultColor = skinnedMeshRenderer.sharedMaterial.GetColor(colorParameter);
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (currentFlash != null)
            StopCoroutine(currentFlash);

        currentFlash = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.PingPong(elapsed * 2f / duration, 1f);
            Color lerped = Color.Lerp(defaultColor, color, t);

            if (meshRenderer != null)
            {
                meshRenderer.GetPropertyBlock(block);
                block.SetColor(colorParameter, lerped);
                meshRenderer.SetPropertyBlock(block);
            }

            if (skinnedMeshRenderer != null)
            {
                skinnedMeshRenderer.GetPropertyBlock(block);
                block.SetColor(colorParameter, lerped);
                skinnedMeshRenderer.SetPropertyBlock(block);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset to default
        if (meshRenderer != null)
        {
            meshRenderer.GetPropertyBlock(block);
            block.SetColor(colorParameter, defaultColor);
            meshRenderer.SetPropertyBlock(block);
        }

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.GetPropertyBlock(block);
            block.SetColor(colorParameter, defaultColor);
            skinnedMeshRenderer.SetPropertyBlock(block);
        }

        currentFlash = null;
    }
}

