using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musuh : MonoBehaviour
{
    Animator anim;
    public Transform player;   // Reference to the player's Transform
    public float speed = 2.0f; // Speed at which the enemy will move toward the player

    bool moveTrue = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isMoving", false);

        if(moveTrue)
        {
            anim.SetBool("isMoving", true);
            // Calculate direction from enemy to player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Calculate the angle in degrees that the enemy should face toward the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation to the enemy so it faces the player
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "flashlight") moveTrue = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "flashlight") moveTrue = false;
    }
}
