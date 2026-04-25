using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject clickEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            worldPosition.z = 0f;

            Instantiate(clickEffect, worldPosition, Quaternion.identity);
        }
    }
}
