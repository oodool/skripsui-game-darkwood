using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;  // Reference to the player's position
    public float chaseRadius = 5f;  // Detection radius for the enemy to chase the player
    public float moveSpeed = 4f;  // Normal movement speed of the enemy
    public float afraidSpeedMultiplier = 2f; // Speed multiplier when the player is scared
    public bool isPlayerAfraid = false;  // Status indicating if the player is afraid

    private bool isChasing = false;
    private bool isStopped = false; // Flag to check if the enemy is stopped

    public bool isStoppedBySmile = false;


    void Update()
    {
        if (isStoppedBySmile) return;  // Jangan lanjutkan chase jika waktu berhenti karena senyum

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRadius && !isChasing)
        {
            isChasing = true;
        }
        else if (distanceToPlayer >= chaseRadius && isChasing)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    // Tambahkan metode baru untuk menghentikan timer
    public void StopTimer()
    {
        isStoppedBySmile = true;
        Debug.Log("Timer stopped due to smile.");
    }

    // Tambahkan metode baru untuk memulai kembali timer
    public void StartTimer()
    {
        isStoppedBySmile = false;
        Debug.Log("Timer resumed.");
    }

    void ChasePlayer()
    {
        // Get direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Determine speed: if the player is afraid, increase enemy speed
        float currentSpeed = moveSpeed;
        if (isPlayerAfraid)
        {
            currentSpeed *= afraidSpeedMultiplier;  // Increase speed if the player is afraid
        }

        // Move enemy towards the player at the determined speed
        transform.position = Vector2.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
    }

    // Method to set chase radius to zero with a duration timer
    public void StopChasing(float duration)
    {
        if (!isStopped) // Check if the enemy is already stopped
        {
            isStopped = true; // Set the stopped flag
            chaseRadius = 0f;  // Set chase radius to zero
            isChasing = false;  // Ensure enemy stops chasing
            Debug.Log("Happy detected, enemy stopped chasing.");

            // Start coroutine to reset chase radius after duration
            StartCoroutine(ResetChaseRadiusAfterDelay(duration));
        }
    }

    // Coroutine to reset chase radius after a delay
    private IEnumerator ResetChaseRadiusAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        chaseRadius = 5f; // Reset to original chase radius or whatever value you want
        Debug.Log("Chase radius reset after " + duration + " seconds.");

        isStopped = false; // Reset stopped flag
    }

    // Draw the detection radius in the Unity editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
