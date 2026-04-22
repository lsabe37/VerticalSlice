using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private void Start()
    {
        Locator.Instance.gameManager.shootGun += fire;
        Locator.Instance.customerManager.wrong += fire;
        Locator.Instance.customerManager.spiceTest += fire;
    }

    private void fire()
    {
        StartCoroutine(Shake(0.3f, 0.4f));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalLocalPosition + new Vector3(x, y, -5);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPosition;
    }
}
