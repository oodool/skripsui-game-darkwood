using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip _microphoneClip;
    private string _microphoneName;
    public Controller Player;

    private void Start()
    {
        MicrophoneToAudioClip(0);
    }

    private void OnEnable()
    {
        MicrophoneSelect.OnMicrophoneChoiceChanged += ChangeMicrophoneSource;
    }

    private void ChangeMicrophoneSource(int perangkatTerpilih)
    {
        MicrophoneToAudioClip(perangkatTerpilih);
    }

    private void OnDisable()
    {
        MicrophoneSelect.OnMicrophoneChoiceChanged -= ChangeMicrophoneSource;
    }
    private void MicrophoneToAudioClip(int microphoneIndex) //mengubah suara dari mic menjadi audio clip dengan merekam suara dari mic
    {
        /*foreach(var name in Microphone.devices)
        {
            Debug.Log(name);
        }*/ // digunakan untuk deteksi mic yang terinstall di perangkat

        _microphoneName = Microphone.devices[microphoneIndex];
        _microphoneClip = Microphone.Start(_microphoneName, true, 20, AudioSettings.outputSampleRate);
    }
    
    public float GetLoudnessFromMicrophone() //mengambil suara yang sudah diubah dari mic menjadi audio clip dengan function
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(_microphoneName), _microphoneClip);

    }
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0) return 0;
        
        float [] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
        
        float totalLoudness = 0;

        foreach(var sample in waveData)
        {
            totalLoudness += Mathf.Abs(sample);
        }

        return totalLoudness / sampleWindow;
    }
 
}
