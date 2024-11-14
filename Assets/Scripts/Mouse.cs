using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public ObjectDetector hand;
    public GameObject player;
    private Vector2 mouse;
    private Controller playerController;
    public float sens;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        mouse = UnityEngine.InputSystem.Mouse.current.position.ReadValue();

        var worldPosition = playerController.useHand ? Camera.main.ScreenToWorldPoint(hand.BoundingBoxesMidpoint()) : Camera.main.ScreenToWorldPoint(mouse);
        transform.position = Vector2.Lerp(player.transform.position, worldPosition, sens);
    }

    ////markiplier
}
