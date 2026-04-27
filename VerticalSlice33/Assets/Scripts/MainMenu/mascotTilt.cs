using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mascotTilt : MonoBehaviour
{
    public float tiltAngle = 15f;
    public float speed = 2f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float tilt = Mathf.Sin(Time.time * speed) * tiltAngle;
        rectTransform.localRotation = Quaternion.Euler(0, 0, tilt);
    }
}
