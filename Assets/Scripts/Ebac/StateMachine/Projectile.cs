using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public float timeToDestroy = 5f;
    public float timeToReset = 5f;
    public Vector3 dir;

    public string tagToLook = "Enemy";

    public Action OnHitTarget;

    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    //public void Awake()
    //{
    //Destroy(gameObject, timeToDestroy);
    //}



    public void StartProjectile()
    {
        Invoke(nameof(FinishUsage), timeToReset);
    }


    public void FinishUsage()
    {
        gameObject.SetActive(false);
        OnHitTarget = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToLook)
        {
            Destroy(collision.gameObject);
            OnHitTarget?.Invoke();
            //Destroy(gameObject);
            FinishUsage();
        }
    }

}
