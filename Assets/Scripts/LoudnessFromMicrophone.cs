using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoudnessFromMicrophone : MonoBehaviour
{
    public Image audioBar;
    public AudioLoudnessDetector detector;
    public Slider sensitivitySlider;
    public float minimumSensibility = 100;
    public float maximumSensibility = 1000;
    public float currentLoudnessSensibility = 500;
    public float threshold = 0.1f;


    public GameObject screamText;
    private void Start()
    {
        if (sensitivitySlider == null) return;

        sensitivitySlider.value = .5f;
        SetLoudnessSensibility(sensitivitySlider.value);
    }


    // Update is called once per frame
    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * currentLoudnessSensibility;
        if (loudness < threshold) loudness = 0.01f;

        audioBar.fillAmount = loudness;

        if (loudness > .5f && !screamText.activeInHierarchy)screamText.SetActive(true);
        if (loudness <= .5f && screamText.activeInHierarchy) screamText.SetActive(false);
        Debug.Log(loudness);
    }

    public void SetLoudnessSensibility(float t)
    {
        currentLoudnessSensibility = Mathf.Lerp(minimumSensibility, maximumSensibility, t);
    }
}
