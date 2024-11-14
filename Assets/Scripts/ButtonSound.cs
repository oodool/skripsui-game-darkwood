using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource hoverAudioSource;
    public AudioSource clickAudioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    // Dipanggil saat mouse hover di atas tombol
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverAudioSource.PlayOneShot(hoverClip);
    }

    // Dipanggil saat tombol di-klik
    public void OnPointerClick(PointerEventData eventData)
    {
        clickAudioSource.PlayOneShot(clickClip);
    }
}
