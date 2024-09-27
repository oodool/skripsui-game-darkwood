using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // Drag the black image here in the Inspector
    public float fadeDuration = 1.0f; // Duration of the fade

    private void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeToBlackAndLoadScene(sceneName));
    }

    private IEnumerator FadeToBlackAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // Fade to black
        SceneManager.LoadScene(sceneName); // Load new scene
    }

    private IEnumerator FadeFromBlack()
    {
        yield return StartCoroutine(Fade(1f, 0f)); // Fade from black to transparent
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the final alpha is set properly
        color.a = endAlpha;
        fadeImage.color = color;
    }
}
