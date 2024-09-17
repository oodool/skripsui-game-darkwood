using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private AudioSource audioSource;   // Assign the AudioSource from the inspector
        public AudioClip[] audioClips;    // Array of footstep sounds
        public float stepInterval = 0.5f;    // Time between steps
        public float minPitch = 0.9f;        // Minimum pitch variation
        public float maxPitch = 1.1f;        // Maximum pitch variation

        private Controller controller;  // Assuming you're using a CharacterController
        private float stepTimer = 0f;

        [SerializeField] private float speed = 10;
        [SerializeField] private float damping = 4;
        [SerializeField] private new Camera camera = default;
        public GameObject flashlight;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Vector2 _mouse;

        private void Awake()
        {   
            audioSource = gameObject.GetComponent<AudioSource>();
            flashlight.SetActive(false);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce((_direction * speed - _rigidbody.velocity) / Time.fixedDeltaTime / damping);
        }

        private void Update()
        {
            var mousePosition = camera.ScreenToWorldPoint(_mouse);
            var playerPosition = transform.position;

            transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                mousePosition - playerPosition
            );

            if(Input.GetKeyDown(KeyCode.F))
            {
                Flashlight();
            }

            if (_direction != Vector2.zero)  // Check if moving
            {
                stepTimer -= Time.deltaTime;

                if (stepTimer <= 0f)
                {
                    PlayAudio(0, 0.8f);
                    stepTimer = stepInterval;  // Reset the timer
                }
            }
        }

        void Flashlight()
        {
            flashlight.SetActive(!flashlight.activeSelf);
            PlayAudio(1, 0.3f);
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