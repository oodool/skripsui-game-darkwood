using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;             // The audio source component
    public AudioClip[] ambientSounds;     // Array of sound clips (e.g., crow, leaves, whispers)
    public float minTimeBetweenSounds = 5f;     // Minimum time between sounds
    public float maxTimeBetweenSounds = 20f;    // Maximum time between sounds
    public float minPitch = 0.85f;               // Pitch variation (optional)
    public float maxPitch = 1.15f;

    public float fadeDuration = 1f;             // Duration of the fade-in effect
    private float targetVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Initialize the AudioSource first
        targetVolume = audioSource.volume;          // Store the original volume
        audioSource.volume = 0;                     // Start with 0 volume
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            // Wait for a random amount of time before playing the next sound
            float waitTime = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
            yield return new WaitForSeconds(waitTime);

            if (!audioSource.isPlaying)
            {
                // Randomly select a sound from the array
                int randomIndex = Random.Range(0, ambientSounds.Length);
                AudioClip randomClip = ambientSounds[randomIndex];
                Debug.Log(randomClip + " is playing");

                // Randomize the pitch for variety
                audioSource.pitch = Random.Range(minPitch, maxPitch);

                // Set the selected ambient sound as the clip and start playing
                audioSource.clip = randomClip;
                audioSource.Play();  // Use Play instead of PlayOneShot

                // Fade-in logic
                float startVolume = 0f;
                float currentTime = 0f;

                while (currentTime < fadeDuration)
                {
                    currentTime += Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / fadeDuration);
                    yield return null;
                }

                // Set to the target volume after fade-in
                audioSource.volume = targetVolume;
            }
        }
    }
}
