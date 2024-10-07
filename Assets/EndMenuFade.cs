using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuFade : MonoBehaviour
{
    SceneTransition fade;
    public float pause;
    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        pause -= Time.deltaTime;

        if (pause <= 0f)
        {
            fade.TransitionToScene("main menu");
        }
    }
}
