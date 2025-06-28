using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float scaleAmount = 0.2f; // Seberapa besar perubahan scale
    public float speed = 2f;         // Kecepatan membesar-mengecil
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.PingPong(Time.time * speed, scaleAmount);
        transform.localScale = originalScale * scale;
    }
}
