using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCamera : MonoBehaviour
{
    public Transform Player;
    public Transform Boss;
    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomLimiter = 0.8f;
    public float smoothSpeed = 0.5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!Player || !Boss) return;

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = (Player.position + Boss.position) / 2f;
        Vector3 desiredPosition = centerPoint;

        desiredPosition.y += 5f;
        desiredPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    void Zoom()
    {
        float distance = Vector3.Distance(Player.position, Boss.position);
        float desiredZoom = distance * zoomLimiter;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(desiredZoom, minZoom, maxZoom), smoothSpeed * Time.deltaTime);
    }
}
