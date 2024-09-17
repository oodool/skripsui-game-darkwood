using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;             // The audio source component
    public AudioClip[] ambientSounds;           // Array of sound clips (e.g., crow, leaves, whispers)
    public float minTimeBetweenSounds = 5f;     // Minimum time between sounds
    public float maxTimeBetweenSounds = 20f;    // Maximum time between sounds
    public float minPitch = 0.9f;               // Pitch variation (optional)
    public float maxPitch = 1.1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

                // Randomize the pitch for variety
                audioSource.pitch = Random.Range(minPitch, maxPitch);

                // Play the selected ambient sound
                audioSource.PlayOneShot(randomClip);
            }
        }
    }
}
