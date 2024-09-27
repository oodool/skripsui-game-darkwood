using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public GameManager manager;
    public string itemName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            manager.incrementItem(itemName);
            Destroy(gameObject);
        }
    }
}
