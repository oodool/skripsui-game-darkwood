using System.Collections;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // SpriteRenderer yang akan di-blink
    public float[] blinkPatterns; // Array untuk pola blink (durasi dalam detik)

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
            for (int i = 0; i < blinkPatterns.Length; i++)
            {
                // Tunggu sesuai dengan pola blink yang ditentukan
                yield return new WaitForSeconds(blinkPatterns[i]);

                // Toggle visibilitas (on/off)
                isVisible = !isVisible;
                spriteRenderer.enabled = isVisible;
            }
        }
    }
}
