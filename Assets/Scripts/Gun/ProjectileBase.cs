using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;

    public float timeToDestroy = 1f;

    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;


    private bool hasHit = false;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(var t in tagsToHit)
        {
            if(collision.transform.tag == t)
            {
                if (hasHit) return;
                hasHit = true;

                var damageble = collision.transform.GetComponent<IDamageble>();

                if (damageble != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageble.Damage(damageAmount, dir);
            }
            break;
        }

        }
        Destroy(gameObject);
    }
}
