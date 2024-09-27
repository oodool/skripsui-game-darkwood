using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Reference to the TextMeshPro description text UI element (shared by both buttons)
    [SerializeField] private TMP_Text descriptionText;

    // Unique description text for each button (set this individually for each button in the Inspector)
    [SerializeField] private string hoverDescription;

    // When the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the description text to the unique hoverDescription for this button
        descriptionText.text = hoverDescription;
        descriptionText.gameObject.SetActive(true); // Make sure it's visible
    }

    // When the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide or clear the description text when the pointer exits the button
        descriptionText.gameObject.SetActive(false);
    }
}
