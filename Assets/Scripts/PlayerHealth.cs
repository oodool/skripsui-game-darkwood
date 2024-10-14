using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    public AudioSource audioSource;  // AudioSource untuk memutar suara serangan
    public AudioClip hitClip;        // AudioClip untuk suara saat terkena serangan
    public AudioClip lowHpClip;      // Suara untuk low HP
    public AudioClip fastBeatClip;   // Suara untuk fast beat
    private bool isLowHpPlaying = false;
    private bool isFastBeatPlaying = false;

    private float originalIntensity;
    private float flashIntensity = 1f; // Intensity for the flash
    private float flashDuration = 2f; // Duration for the flash
    private float flashTimer;

    // Screen shake variables
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlin;
    public float durationShake = 1f;
    private float timerShake;
    public float normalShake = 1f;
    public float targetShake = 3f;

    // Additional AudioSource for low HP and fast beat sounds
    public AudioSource lowHpAudioSource;
    public AudioSource fastBeatAudioSource;

    public SpriteRenderer bloodSplatterImage; // Reference to the UI Image component
    public Sprite[] bloodSplatterSprites; // Array to hold blood splatter sprites
    public float splatterDuration = 1f;
    public float fadeSpeed = 2f;
    private Color currentColor;

    void Start()
    {
        currentColor = bloodSplatterImage.color;
        // Initialize health
        currentHealth = maxHealth;

        // Get the Volume component and the Vignette effect
        Volume volume = FindObjectOfType<Volume>();
        if (volume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustments found!");
        }
        else
        {
            Debug.LogError("Color Adjustments not found in Post-Processing Volume!");
        }

        if (volume.profile.TryGet(out vignette))
        {
            // Store the original vignette intensity
            originalIntensity = vignette.intensity.value;
        }

        // Initialize Cinemachine components
        if (virtualCamera != null)
        {
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    void Update()
    {
        // Simulate taking damage (this should be replaced with your damage logic)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }

        // Handle the flashing effect
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(flashIntensity, originalIntensity, (flashDuration - flashTimer) / flashDuration);
        }
        else
        {
            vignette.intensity.value = originalIntensity; // Reset to original intensity
        }

        if (timerShake > 0)
        {
            timerShake -= Time.deltaTime;

            perlin.m_AmplitudeGain = Mathf.Lerp(targetShake, normalShake, (durationShake - timerShake) / durationShake);
            perlin.m_FrequencyGain = Mathf.Lerp(targetShake, normalShake, (durationShake - timerShake) / durationShake);
        }
        else
        {
            perlin.m_AmplitudeGain = normalShake;
            perlin.m_FrequencyGain = normalShake; // Reset to original intensity
        }

        // Handle Low HP and Fast Beat sound transitions
        if (currentHealth < 20 && !isFastBeatPlaying)
        {
            // Stop low HP sound and start fast beat sound
            StopLowHpSound();
            PlayFastBeatSound();
        }
        else if (currentHealth >= 20 && currentHealth < 50 && !isLowHpPlaying)
        {
            // Stop fast beat sound and play low HP sound
            StopFastBeatSound();
            PlayLowHpSound();
        }
        else if (currentHealth >= 50)
        {
            StopLowHpSound();
        }

        // Handle Fast Beat sound
        if (currentHealth < 20 && !isFastBeatPlaying)
        {
            PlayFastBeatSound();
        }
        else if (currentHealth >= 20 && isFastBeatPlaying)
        {
            StopFastBeatSound();
        }

        /// health regen
        if(currentHealth <= maxHealth) currentHealth += 0.05f;
        
        // Calculate the health percentage (0 to 1)
        float healthPercentage = currentHealth / maxHealth;

        // Adjust the screen redness and contrast based on health
        AdjustScreenEffects(healthPercentage);
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Trigger the vignette flash
        FlashVignette();

        // Trigger the screen shake
        ShakeCamera();

        TriggerBloodSplatter();
    }

    // Call this function when the player takes damage
    void TriggerBloodSplatter()
    {
        StopAllCoroutines(); // Stop any previous fade coroutines

        // Randomly choose a blood splatter sprite
        Sprite randomSplatter = bloodSplatterSprites[Random.Range(0, bloodSplatterSprites.Length)];
        bloodSplatterImage.sprite = randomSplatter;

        // Set the blood splatter to full opacity and start the fade out
        var fadeColor = currentColor;
        fadeColor.a = 0.8f;
        bloodSplatterImage.color = fadeColor;
        StartCoroutine(FadeOutBloodSplatter());
    }

    private IEnumerator FadeOutBloodSplatter()
    {
        yield return new WaitForSeconds(splatterDuration); // Wait before fading

        // Gradually fade out the image
        while (bloodSplatterImage.color.a > 0)
        {
            Color tempColor = bloodSplatterImage.color;
            tempColor.a -= Time.deltaTime * fadeSpeed;
            bloodSplatterImage.color = tempColor;
            yield return null;
        }

        // After fading, fully hide the image
        var fadeColor = currentColor;
        fadeColor.a = 0f;
        bloodSplatterImage.color = fadeColor;
    }

    void AdjustScreenEffects(float healthPercentage)
    {
        Debug.Log("tes");
        // Adjust color grading based on player's health
        colorAdjustments.colorFilter.value = Color.Lerp(Color.red, Color.white, healthPercentage); // Lower health = redder screen
        colorAdjustments.contrast.value = Mathf.Lerp(100f, 0f, healthPercentage); // Lower health = higher contrast
    }

    void FlashVignette()
    {
        flashTimer = flashDuration; // Reset the flash timer
        vignette.intensity.value = flashIntensity; // Set to flash intensity immediately
    }

    private void ShakeCamera()
    {
        timerShake = durationShake;
        
        perlin.m_AmplitudeGain = targetShake;
        perlin.m_FrequencyGain = targetShake;
    }

    private void PlayLowHpSound()
    {
        if (lowHpClip != null && lowHpAudioSource != null)
        {
            lowHpAudioSource.clip = lowHpClip;
            lowHpAudioSource.loop = true; // Loop the sound
            lowHpAudioSource.Play();
            isLowHpPlaying = true;
        }
    }

    private void StopLowHpSound()
    {
        if (lowHpAudioSource != null && lowHpAudioSource.isPlaying)
        {
            lowHpAudioSource.Stop();
            isLowHpPlaying = false;
        }
    }

    private void PlayFastBeatSound()
    {
        if (fastBeatClip != null && fastBeatAudioSource != null)
        {
            fastBeatAudioSource.clip = fastBeatClip;
            fastBeatAudioSource.loop = true; // Loop the sound
            fastBeatAudioSource.Play();
            isFastBeatPlaying = true;
        }
    }

    private void StopFastBeatSound()
    {
        if (fastBeatAudioSource != null && fastBeatAudioSource.isPlaying)
        {
            fastBeatAudioSource.Stop();
            isFastBeatPlaying = false;
            fastBeatAudioSource.pitch = 1f; // Reset pitch to normal
        }
    }
}
