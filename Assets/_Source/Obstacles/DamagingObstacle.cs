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

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
        }

        public void DestroyObstacle()
        {
            StartCoroutine(DestructionCoroutine());
        }

        private IEnumerator DestructionCoroutine()
        {
            spriteRenderer.sprite = explosionSprite;
            collider2d.enabled = false;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}