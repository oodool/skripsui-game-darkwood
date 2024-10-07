using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SocketClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    public Text emotionLabel;         // Reference to UI Text for displaying emotion label
    public Image emotionImage;        // Reference to UI Image for displaying emotion image
    public Sprite neutralSprite;      // Sprite for neutral emotion
    public Sprite negativeSprite;     // Sprite for negative emotion
    public Sprite positiveSprite;     // Sprite for positive emotion
    public EnemyChase enemy;          // Reference to the EnemyChase script managing enemy movement
    public float fearSpeedMultiplier = 2.0f; // Speed multiplier when the player is scared
    public float fearDuration = 5.0f; // Duration of speed increase in seconds

    private float normalEnemySpeed;   // Normal enemy speed
    private bool isCoroutineRunning = false; // Track if the coroutine is running

    void Start()
    {
        ConnectToServer();

        // Store the normal enemy speed to return to when the player is not afraid
        normalEnemySpeed = enemy.moveSpeed;
        Debug.Log("Normal enemy speed: " + normalEnemySpeed);
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient("localhost", 12345);  // Change to server IP if needed
            stream = client.GetStream();
            Debug.Log("Connected to Python server.");
        }
        catch (SocketException ex)
        {
            Debug.LogError($"Socket Exception: {ex.Message}");
        }
    }

    void Update()
    {
        if (stream != null && stream.DataAvailable)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Update emotion label in UI
            UpdateEmotionLabel(data);
            Debug.Log("Received data: " + data);
        }
    }

void UpdateEmotionLabel(string data)
{
    string[] emotions = data.Trim(new char[] { '[', ']', '\"' }).Split(',');

    if (emotions.Length > 0)
    {
        emotionLabel.text = "Emosi Player : " + string.Join(", ", emotions); 
        Debug.Log("Detected emotions: " + string.Join(", ", emotions));

        Sprite currentEmotionSprite = neutralSprite;
        bool isSmiling = Array.Exists(emotions, emotion => emotion.Trim() == "Senang");

        // Check for "Fear" emotions (negative) and change sprite
        if (Array.Exists(emotions, emotion => emotion.Trim() == "Takut" || emotion.Trim() == "Marah" ||
            emotion.Trim() == "Jijik" || emotion.Trim() == "Sedih" || emotion.Trim() == "Kaget"))
        {
            currentEmotionSprite = negativeSprite;

            // Mulai kembali timer jika pemain tidak senyum lagi
            if (enemy.isStoppedBySmile)  // Cek apakah timer dihentikan oleh senyum
            {
                enemy.StartTimer();  // Mulai kembali timer
            }

            if (!isCoroutineRunning)
            {
                StartCoroutine(IncreaseEnemySpeedTemporarily());
            }
        }
        // Check for "Happy" emotion (positive) and change sprite
        else if (isSmiling)
        {
            currentEmotionSprite = positiveSprite;

            // Hentikan timer jika senyum terdeteksi
            if (!enemy.isStoppedBySmile)  // Pastikan timer belum dihentikan
            {
                enemy.StopTimer();
            }
        }
        else
        {
            // No special emotion or neutral case (resetting conditions)
            currentEmotionSprite = neutralSprite;

            // Mulai kembali timer jika pemain berhenti tersenyum
            if (enemy.isStoppedBySmile)  
            {
                enemy.StartTimer();
            }
        }

        // Update emotion image in the UI immediately
        emotionImage.sprite = currentEmotionSprite;
    }
    else
    {
        // No emotion detected, resume normal behavior
        emotionLabel.text = "No Emotion Detected";
        emotionImage.sprite = neutralSprite;

        // Mulai kembali timer jika tidak ada emosi yang terdeteksi
        if (enemy.isStoppedBySmile)  
        {
            enemy.StartTimer();
        }
    }
}

    IEnumerator IncreaseEnemySpeedTemporarily()
    {
        isCoroutineRunning = true;

        // Increase enemy speed
        enemy.moveSpeed = normalEnemySpeed * fearSpeedMultiplier;
        Debug.Log("Fear detected (Takut), increasing enemy speed to: " + enemy.moveSpeed);

        // Wait for duration of fear
        yield return new WaitForSeconds(fearDuration);

        // Reset to normal speed after duration
        enemy.moveSpeed = normalEnemySpeed;
        Debug.Log("Resetting enemy speed to: " + enemy.moveSpeed);

        isCoroutineRunning = false;
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}
