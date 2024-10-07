using System.Collections;
using UnityEngine;

public class EnemyChaseHappy : MonoBehaviour
{
    public Transform player;  // Reference to the player's position
    public float chaseRadius = 5f;  // Radius in meters for the enemy to detect the player
    public float moveSpeed = 2f;  // Normal enemy movement speed
    private bool isChasing = false;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // Start chasing if the player is within the chase radius
        if (distanceToPlayer < chaseRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // If the enemy is chasing, move towards the player
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // Method to stop chasing temporarily
    public IEnumerator StopChasingTemporarily(float duration)
    {
        isChasing = false; // Stop chasing
        Debug.Log("Happy detected, enemy stopped chasing.");

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        isChasing = true; // Resume chasing
        Debug.Log("Enemy resumed chasing.");
    }
}
