using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject credits;
    public GameObject play;
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
        fade.TransitionToScene("tutorial");
    }
}
