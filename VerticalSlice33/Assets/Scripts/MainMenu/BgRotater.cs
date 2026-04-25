using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgRotater : MonoBehaviour
{
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
