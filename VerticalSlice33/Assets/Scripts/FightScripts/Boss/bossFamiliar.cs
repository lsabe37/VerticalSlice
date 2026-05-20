using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFamiliar : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);

        transform.position = Vector3.Lerp(pointA.position, pointB.position, time);
    }
}
