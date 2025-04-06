using Player;
using System.Collections;
using UnityEngine;

namespace Obstacles
{
    public class DamagingObstacle : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public float Force { get; private set; } = 1.5f;
        [SerializeField] private Sprite explosionSprite;
        private SpriteRenderer spriteRenderer;
        private Collider2D collider2d;
        private PlayerHandler playerHandler;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
            playerHandler = FindObjectOfType<PlayerHandler>();
            playerHandler.PlayerGotHurt += DisableCollision;
        }

        void OnDestroy()
        {
            playerHandler.PlayerGotHurt -= DisableCollision;
        }

        public void DestroyObstacle()
        {
            StartCoroutine(DestructionCoroutine());
        }

        public void DisableCollision()
        {
            StartCoroutine(CollisionDisabledCoroutine());
        }

        private IEnumerator DestructionCoroutine()
        {
            spriteRenderer.sprite = explosionSprite;
            collider2d.enabled = false;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        private IEnumerator CollisionDisabledCoroutine()
        {
            collider2d.enabled = false;
            yield return new WaitForSeconds(1.2f);
            collider2d.enabled = true;
        }
    }
}