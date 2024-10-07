using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject credits;
    public GameObject play;
    public GameObject camWarning;
    SceneTransition fade;

    void Start()
    {
        fade = gameObject.GetComponent<SceneTransition>();
    }

    public void Play()
    {
        if (credits.activeSelf) ToggleCredits();
        if (settings.activeSelf) ToggleSettings();
        play.SetActive(!play.activeSelf);
    }

    public void ToggleSettings()
    {
        if (credits.activeSelf) ToggleCredits();
        if (play.activeSelf) Play();
        settings.SetActive(!settings.activeSelf);
    }

    public void ToggleCredits()
    {
        if (settings.activeSelf) ToggleSettings();
        if (play.activeSelf) Play();
        credits.SetActive(!credits.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetControl(string control)
    {
        PlayerPrefs.SetString("Control", control);
        if (control == "hand")
        {
            if(CheckForWebcam())
            {
                fade.TransitionToScene("tutorial");
            } else {
                FlashWarning();
            }
        } else {
            fade.TransitionToScene("tutorial");
        }
    }

    bool CheckForWebcam()
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

            return true;
        }
        else
        {
            Debug.Log("No webcams found.");
            return false;
        }
    }

    void FlashWarning()
    {
        camWarning.SetActive(true);

        StartCoroutine(WaitForFlash());
    }

    IEnumerator WaitForFlash()
    {
        yield return new WaitForSeconds(1f); // Adjust this for time between flickers
        camWarning.SetActive(false);
    }
}
