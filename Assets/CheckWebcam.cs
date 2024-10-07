using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWebcam : MonoBehaviour
{
    void Start()
    {
        CheckForWebcam();
    }

    void CheckForWebcam()
    {
        // Get the list of available webcam devices
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 0)
        {
            Debug.Log("Webcam(s) found:");

            // Loop through available webcams and print their names
            foreach (WebCamDevice device in devices)
            {
                Debug.Log("Webcam name: " + device.name);
            }

            // Example: use the first available webcam
            string webcamName = devices[0].name;
            Debug.Log("Using webcam: " + webcamName);

            // Optionally, you can now initialize the webcam
            // WebCamTexture webcamTexture = new WebCamTexture(webcamName);
            // webcamTexture.Play();
        }
        else
        {
            Debug.Log("No webcams found.");
        }
    }
}
