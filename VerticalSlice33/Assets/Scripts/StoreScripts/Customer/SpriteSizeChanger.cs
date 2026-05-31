using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSizeChanger : MonoBehaviour
{
    public float baseScale = 1f;
    public float scaleRange = 0.01f;
    public float speed = 1f;

    private void Update()
    {
        float sinValue = Mathf.Sin(Time.time * speed);
        float currentScale = baseScale + (sinValue * scaleRange);

        transform.localScale = new Vector3(currentScale, currentScale, 1f);
    }
}
