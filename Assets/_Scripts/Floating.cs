using UnityEngine;

public class Floating : MonoBehaviour
{
    public float amplitude = 1f; 
    public float frequency = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newX = startPos.x + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(newX, transform.position.y, startPos.z);
    }


}
