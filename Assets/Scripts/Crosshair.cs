using UnityEngine;

public class CustomCrosshair : MonoBehaviour
{
    public RectTransform crosshair;     // Reference to the crosshair UI element

    void Start()
    {
        // Hide the default system cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the current mouse position
        Vector2 cursorPos = Input.mousePosition;

        // Move the crosshair to the mouse position
        if (Time.timeScale == 1f) crosshair.position = cursorPos;
    }
}
