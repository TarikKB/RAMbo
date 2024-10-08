using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration) {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void StartShake(float duration, float magnitude) {
        StartCoroutine(Shake(duration, magnitude));
    }
}
