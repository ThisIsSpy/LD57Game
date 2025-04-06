using Obstacles;
using Score;
using UnityEngine;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; set; } = 1f;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Camera mainCamera;
        [Header("Sound Stuff")]
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip itemPickupSFX;
        [SerializeField] private AudioClip obstacleExplosionSFX;
        public Rigidbody2D PlayerRB { get; private set; }
        private SpriteRenderer spriteRenderer;
        private ParticleSystem bubblesParticleSystem;
        private ScoreCounter scoreCounter;
        private PlayerHPDisplay playerHPDisplay;
        private Vector2 mousePosition;

        public void Construct(ScoreCounter scoreCounter, PlayerHPDisplay playerHPDisplay)
        {
            this.scoreCounter = scoreCounter;
            this.playerHPDisplay = playerHPDisplay;
        }

        void Start()
        {
            PlayerRB = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            bubblesParticleSystem = GetComponentInChildren<ParticleSystem>();
        }

        void Update()
        {
            RotateTowardsMouse();
            AdjustBubbles();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Treasure treasure))
            {
                scoreCounter.Score += treasure.ScorePrize;
                sfxPlayer.PlayOneShot(itemPickupSFX);
                treasure.DestroyTreasure();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out DamagingObstacle obstacle))
            {
                playerHPDisplay.Lives -= obstacle.Damage;
                sfxPlayer.PlayOneShot(obstacleExplosionSFX);
                obstacle.DestroyObstacle();
            }
        }

        private void RotateTowardsMouse()
        {
            mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.localPosition.z));
            Vector3 rotateDirection = (worldPosition - transform.localPosition).normalized;
            rotateDirection.z = 0;
            float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
            {
                spriteRenderer.flipY = true;
                bubblesParticleSystem.transform.localPosition = new(bubblesParticleSystem.transform.localPosition.x, Mathf.Abs(bubblesParticleSystem.transform.localPosition.y) * -1, bubblesParticleSystem.transform.localPosition.z);
            }
            else
            {
                spriteRenderer.flipY = false;
                bubblesParticleSystem.transform.localPosition = new(bubblesParticleSystem.transform.localPosition.x, Mathf.Abs(bubblesParticleSystem.transform.localPosition.y) * 1, bubblesParticleSystem.transform.localPosition.z);
            }
            
        }

        private void AdjustBubbles()
        {
            var main = bubblesParticleSystem.main;
            main.startSpeed = Mathf.Lerp(main.startSpeed.constant, Mathf.Abs(PlayerRB.velocity.x) * 0.75f, Time.deltaTime);
            if (transform.position.y >= 53 && !bubblesParticleSystem.isStopped) bubblesParticleSystem.Stop();
            else if (transform.position.y < 53 && bubblesParticleSystem.isStopped) bubblesParticleSystem.Play();
        }
    }
}
