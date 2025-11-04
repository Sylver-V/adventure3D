using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileBase>() != null) return;

        ItemCollactableBase i = other.GetComponent<ItemCollactableBase>();
        if (i != null && i.GetComponent<Magnetic>() == null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }

}
