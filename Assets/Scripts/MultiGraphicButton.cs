using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MultiGraphicButton : Button
{
    // Drag the additional graphics (Text and Icon) to this list
    [SerializeField] public Graphic[] additionalGraphics;

    // Override the 'OnPointerEnter' method
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);  // Call the original OnPointerEnter method

        // Change the color of additional graphics when hovered
        foreach (var graphic in additionalGraphics)
        {
            graphic.color = colors.highlightedColor;
        }
    }

    // Override the 'OnPointerExit' method
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);  // Call the original OnPointerExit method

        // Revert the color of additional graphics when not hovered
        foreach (var graphic in additionalGraphics)
        {
            graphic.color = colors.normalColor;
        }
    }
}
