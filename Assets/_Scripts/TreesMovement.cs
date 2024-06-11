using UnityEngine;

public class TreesMovement : MonoBehaviour
{
    public float shakeIntensity = 0.005f;
    public float shakeSpeed = 1f;

    private Vector3 originalScale;
    public float speed = 3.3f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime * Eternals.speedBoostValue);
        float scaleX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2f - 1f;
        float scaleY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2f - 1f;
        Vector3 shakeScale = new Vector3(scaleX, scaleY, 0) * shakeIntensity;
        transform.localScale = originalScale + shakeScale;
        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }

       
    }
}
