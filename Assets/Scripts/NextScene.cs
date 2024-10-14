using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    SceneTransition fade;

    void Start()
    {
        fade = GetComponent<SceneTransition>();
    }

    public void Next(string level)
    {
        fade.TransitionToScene(level);
    }
}
