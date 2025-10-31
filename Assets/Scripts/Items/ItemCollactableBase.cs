using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{

    public string comparteTag = "Player";
    public ParticleSystem particleSystem;
    //public float timeToHide = 3;
    //public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(comparteTag))
        {
            Collect();
        }
    }



    protected virtual void Collect()
    {
        //Debug.Log("Collect");
        //if(graphicItem != null) graphicItem.SetActive(false);
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
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null && audioSource.clip != null)
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

    }
}
