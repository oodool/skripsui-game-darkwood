using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Xml.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SearchService;
using System.Linq;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        Animator anim;

        public string[] flashlightGestures;

        public RectTransform crosshair;     // Reference to the crosshair UI element

        private AudioSource audioSource;   // Assign the AudioSource from the inspector
        public AudioClip[] audioClips;    // Array of footstep sounds
         
        public float stepInterval = 0.5f;    // Time between steps
        public float minPitch = 0.9f;        // Minimum pitch variation
        public float maxPitch = 1.1f;        // Maximum pitch variation
        private float stepTimer = 0f;

        [SerializeField] private float speed = 10;
        [SerializeField] private float damping = 4;
        [SerializeField] private new Camera camera = default;
        public GameObject flashlight;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Vector2 _mouse;

        public ObjectDetector hand;

        public bool useHand;

        private bool isFlickering = false;

        private bool checkFlashlight = false;

        private void Awake()
        {   
            // Hide the default system cursor
            Cursor.visible = false;

            audioSource = gameObject.GetComponent<AudioSource>();
            flashlight.SetActive(false);
            _rigidbody = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            Debug.Log(PlayerPrefs.GetString("Control"));

            if (PlayerPrefs.GetString("Control") == "hand") useHand = true;
            if (PlayerPrefs.GetString("Control") == "mouse") useHand = false;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce((_direction * speed - _rigidbody.velocity) / Time.fixedDeltaTime / damping);
        }

        private void Update()
        {
            var worldPosition = useHand == true ? camera.ScreenToWorldPoint(hand.BoundingBoxesMidpoint()) : camera.ScreenToWorldPoint(_mouse);
            var playerPosition = transform.position;

            if(Time.timeScale != 0)
            {
                transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                worldPosition - playerPosition
            );
            }

            // Get the current mouse position
            var cursorPos = camera.WorldToScreenPoint(worldPosition);

            // Move the crosshair to the mouse position
            if (Time.timeScale == 1f) crosshair.position = cursorPos;

            anim.SetBool("isWalking", false);

            if (_direction != Vector2.zero)  // Check if moving 
            {
                anim.SetBool("isWalking", true);

                stepTimer -= Time.deltaTime;

                if (stepTimer <= 0f)
                {
                    PlayAudio(0, 0.8f);
                    stepTimer = stepInterval;  // Reset the timer
                }
            }

            if(Input.GetKeyDown(KeyCode.F) && !useHand)
            {
                Flashlight();
            }
            
            if (flashlightGestures.Contains(hand.ObjectLabel()) && useHand && !checkFlashlight)
            {
                Flashlight();
                checkFlashlight = true;
            } else if (!flashlightGestures.Contains(hand.ObjectLabel()) && useHand && checkFlashlight)
            {
                Flashlight();
                checkFlashlight = false;
            }

            if(flashlight.activeSelf)
            {
                var random = Random.Range(1, 20);
            }
        }

        void Flashlight()
        {
            flashlight.SetActive(!flashlight.activeSelf);
            PlayAudio(1, 0.3f);

            // If flashlight is on, start flickering
            if (flashlight.activeSelf && !isFlickering)
            {
                StartCoroutine(FlickerFlashlight());
            }
        }

        IEnumerator FlickerFlashlight()
        {
            isFlickering = true;

            while (flashlight.activeSelf)
            {
                // Randomly decide to flicker
                if (Random.value < 0.2f) // Adjust this value to change flicker frequency
                {
                    flashlight.SetActive(false);
                    yield return new WaitForSeconds(0.05f); // Flicker off duration
                    flashlight.SetActive(true);
                }

                // Wait for a short moment before checking again
                yield return new WaitForSeconds(Random.Range(0.05f, 3f)); // Adjust this for time between flickers
            }

            isFlickering = false;
        }

        public void OnMove(InputValue value)
        {
            _direction = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _mouse = value.Get<Vector2>();
        }

        void PlayAudio(int audioIndex, float volume)
        {
            // Randomize the pitchs
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClips[audioIndex]);   // Play the sound
        }
    }
}