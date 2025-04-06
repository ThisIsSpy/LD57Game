using Obstacles;
using Score;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; set; } = 1f;
        [SerializeField] private Sprite deathSprite;
        [SerializeField] private Camera mainCamera;
        [Header("Sound Stuff")]
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip itemPickupSFX;
        [SerializeField] private AudioClip obstacleExplosionSFX;
        public Rigidbody2D PlayerRB { get; private set; }
        public bool CanBeControlled { get; private set; }
        public event Action PlayerGotHurt;
        private SpriteRenderer spriteRenderer;
        private ParticleSystem bubblesParticleSystem;
        private ScoreCounter scoreCounter;
        private PlayerHPDisplay playerHPDisplay;
        private Vector2 mousePosition;

        public void Construct(ScoreCounter scoreCounter, PlayerHPDisplay playerHPDisplay)
        {
            this.scoreCounter = scoreCounter;
            this.playerHPDisplay = playerHPDisplay;

            this.playerHPDisplay.ZeroLivesLeft += PlayerDeath;
        }

        void Start()
        {
            PlayerRB = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            bubblesParticleSystem = GetComponentInChildren<ParticleSystem>();
            CanBeControlled = true;
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
                if(playerHPDisplay.Lives > 0) PlayerRB.AddRelativeForce(new(-obstacle.Force, 0), ForceMode2D.Impulse);
                obstacle.DestroyObstacle();
                StartCoroutine(DisableControlsCoroutine());
                if (playerHPDisplay.Lives > 0) StartCoroutine(PlayerHurtCoroutine());
            }
        }

        void OnDestroy()
        {
            playerHPDisplay.ZeroLivesLeft -= PlayerDeath;
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
            else if (transform.position.y < 53 && bubblesParticleSystem.isStopped && playerHPDisplay.Lives > 0) bubblesParticleSystem.Play();
        }

        public void PlayerDeath()
        {
            StartCoroutine(PlayerDeathCoroutine());
        }

        private IEnumerator DisableControlsCoroutine()
        {
            CanBeControlled = false;
            yield return new WaitForSeconds(1.2f);
            CanBeControlled = true;
        }

        private IEnumerator PlayerDeathCoroutine()
        {
            CanBeControlled = false;
            bubblesParticleSystem.Stop();
            Color currentColor = spriteRenderer.color;
            while(spriteRenderer.color.a > 0)
            {
                float a = spriteRenderer.color.a - 0.15f;
                spriteRenderer.color = new(currentColor.r, currentColor.g, currentColor.b, a);
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("GameOver");
        }

        private IEnumerator PlayerHurtCoroutine()
        {
            PlayerGotHurt?.Invoke();
            Color currentColor = spriteRenderer.color;
            for(int i = 0; i < 6; i++)
            {
                if (i % 2 == 0) spriteRenderer.color = new(currentColor.r, currentColor.g, currentColor.b, 0.15f);
                else spriteRenderer.color = new(currentColor.r, currentColor.g, currentColor.b, 1f);
                yield return new WaitForSeconds(0.2f);
            }
            spriteRenderer.color = currentColor;
        }
    }
}
