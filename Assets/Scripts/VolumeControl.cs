using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;  // Reference to the AudioMixer
    public Slider musicSlider;     // Slider for music volume
    public Slider sfxSlider;       // Slider for SFX volume

    private void Start()
    {
        // Set the sliders' initial values (optional)
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    // Function to adjust the music volume
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Save volume setting
    }

    // Function to adjust the SFX volume
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);  // Save volume setting
    }
}
