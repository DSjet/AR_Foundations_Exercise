using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.5f;
    [SerializeField] private float blinkDuration = 0.1f;

    private Renderer objectRenderer;
    private bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        StartBlinking();
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopAllCoroutines();
            objectRenderer.enabled = true;
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            objectRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            objectRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
