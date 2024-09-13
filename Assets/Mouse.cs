using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public GameObject player;
    private Vector2 mouse;
    public float sens;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        var worldPosition = Camera.main.ScreenToWorldPoint(mouse);
        transform.position = Vector2.Lerp(player.transform.position, worldPosition, sens);
    }

    ////markiplier
}
