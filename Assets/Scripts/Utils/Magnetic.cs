using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .3f;
    public float coinSpeed = 5f;
    public float maxSpeed = 20f;
    public float acceleration = 10f;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (distance > dist)
        {
            coinSpeed = Mathf.Lerp(coinSpeed, maxSpeed, Time.deltaTime * acceleration);
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }

}
