using UnityEngine;

public class CarShake : MonoBehaviour
{
    public float shakeIntensity = 0.05f;  // Adjust the shake intensity as needed
    public float shakeSpeed = 10f;        // Adjust the speed of shaking

    private Vector3 originalScale;

    void Start()
    {
        // Store the original scale of the car
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Apply a small random scale offset to the car's scale
        float scaleX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2f - 1f;
        float scaleY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2f - 1f;
        Vector3 shakeScale = new Vector3(scaleX, scaleY, 0) * shakeIntensity;
        transform.localScale = originalScale + shakeScale;
    }
}
