using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int itemCollected = 0;
    public int targetItem;
    GameObject endDoor;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        endDoor = GameObject.FindWithTag("EndDoor");
    }

    public void incrementItem(string item)
    {
        /// update player's inventory
        UpdateInventory(item);

        itemCollected++;

        /// check if win condition is fully met
        if (checkItem())
        {
            winGame();
        }
    }

    void UpdateInventory(string item)
    {
        foreach (Transform child in inventory.transform)
        {
            if (child.name == item)
            {
                Image image = child.GetComponent<Image>();

                if (image != null)
                {
                    // Get the current color of the image
                    Color color = image.color;

                    // Set the alpha to the desired opacity (0 = fully transparent, 1 = fully opaque)
                    color.a = 1f;
                    image.color = color;
                }
            }
        }
    }

    bool checkItem()
    {
        if(itemCollected == targetItem)
        {
            return true;
        } else {
            return false;
        }
    }

    void winGame()
    {
        Destroy(endDoor);
    }
}
