using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    SceneTransition fade;

    public GameObject hand;
    public GameObject mouse;

    void Start()
    {
        fade = GetComponent<SceneTransition>();

        if(PlayerPrefs.GetString("control") == "hand")
        {
            hand.SetActive(true);
        } else {
            mouse.SetActive(true);
        }
    }

    public void Next(string level)
    {
        fade.TransitionToScene(level);
    }
}
