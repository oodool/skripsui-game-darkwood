using System.Collections;
using UnityEngine;
using TMPro; // Pastikan Anda menggunakan TextMeshPro

public class ButtonColorChanger : MonoBehaviour
{
    public TextMeshProUGUI[] textComponents; // Array untuk menyimpan komponen TextMeshPro
    public float[] blinkPatterns; // Array untuk pola blink (durasi dalam detik)

    private void Start()
    {
        // Memulai blink effect
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            for (int i = 0; i < blinkPatterns.Length; i++)
            {
                // Tunggu sesuai dengan pola blink yang ditentukan
                yield return new WaitForSeconds(blinkPatterns[i]);

                // Ubah visibilitas untuk setiap teks
                foreach (var textComponent in textComponents)
                {
                    textComponent.enabled = !textComponent.enabled; // Toggle visibilitas
                }
            }
        }
    }
}
