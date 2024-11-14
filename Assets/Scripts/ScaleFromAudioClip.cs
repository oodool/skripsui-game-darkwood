using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromAudioClip : MonoBehaviour
{

    public AudioSource source;
    public Vector3 minScale, maxScale;
    public AudioLoudnessDetector detector;

    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;


    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromAudioClip(source.timeSamples, source.clip);
        Debug.Log(loudness);
        if (loudness < threshold) loudness = 0;


         transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
