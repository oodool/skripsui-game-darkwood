using System.Collections;
using UnityEngine;
using TMPro; // Untuk TextMeshPro, jika menggunakan UI Text ganti dengan UnityEngine.UI

public class BackgroundBlinkingEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // SpriteRenderer yang akan di-blink
    public TextMeshProUGUI textComponent; // Komponen TextMeshPro untuk teks
    public Color blinkColor = Color.red; // Warna font saat blink
    public Color normalColor = Color.white; // Warna font normal
    public float minBlinkTime = 0.1f; // Waktu blink minimum (detik)
    public float maxBlinkTime = 0.5f; // Waktu blink maksimum (detik)

    private bool isVisible = true; // Status apakah sprite terlihat

    void Start()
    {
        // Memulai blink effect
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        while (true)
        {
            // Waktu acak antara blink
            float blinkDuration = Random.Range(minBlinkTime, maxBlinkTime);

            // Tunggu blinkDuration sebelum mengganti visibilitas
            yield return new WaitForSeconds(blinkDuration);

            // Toggle visibilitas (on/off) dan ubah warna teks
            isVisible = !isVisible;
            spriteRenderer.enabled = isVisible;

            // Jika sprite tidak terlihat, ubah warna teks ke blinkColor
            if (isVisible)
            {
                textComponent.color = normalColor; // Kembali ke warna normal
            }
            else
            {
                textComponent.color = blinkColor; // Ubah ke warna blink
            }
        }
    }
}
