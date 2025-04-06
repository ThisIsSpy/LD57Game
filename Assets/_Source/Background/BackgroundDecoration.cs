using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundDecoration : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = new Sprite[2];
        [SerializeField] private float animationSpeed;

        [Header("Stuff for moving decorations")]
        [SerializeField] private int minTimeBetweenMovement;
        [SerializeField] private int maxTimeBetweenMovement;
        [SerializeField] private float chanceToMove;
        [SerializeField] private float movementSpeed;
        [SerializeField] private bool randomizeSizeAndLayerPosition;

        private SpriteRenderer spriteRenderer;
        private ParticleSystem particles;
        private int spriteIndex;
        private Rigidbody2D rb;
        private int xDirectionMult;
        private int yDirectionMult;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[0];
            spriteIndex = 0;
            if (randomizeSizeAndLayerPosition)
            {
                spriteRenderer.sortingOrder = (int)Randomizer.RandomFloat(-5,-1);
                if (spriteRenderer.sortingOrder == -3) spriteRenderer.sortingOrder = -2;
                float rndSize = Randomizer.RandomFloat(2, 5) / 10;
                transform.localScale = new(rndSize, rndSize, 0);
            }
            StartCoroutine(AnimationCoroutine());
            if (TryGetComponent(out Rigidbody2D foundRB))
            {
                rb = foundRB;
                xDirectionMult = -1;
                particles = GetComponentInChildren<ParticleSystem>();
                StartCoroutine(MovingCoroutine());
            }
        }

        private IEnumerator AnimationCoroutine()
        {
            yield return new WaitForSeconds(animationSpeed);
            spriteIndex++;
            if (spriteIndex >= sprites.Length) spriteIndex = 0;
            spriteRenderer.sprite = sprites[spriteIndex];
            StartCoroutine(AnimationCoroutine());
            
        }

        private IEnumerator MovingCoroutine()
        {
            yield return new WaitForSeconds(Randomizer.RandomFloat(minTimeBetweenMovement, maxTimeBetweenMovement+1));
            if (Randomizer.Determiner(chanceToMove))
            {
                int yDirForce = 0;
                if (Randomizer.Determiner(50))
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    if (particles != null) particles.transform.localPosition = new(particles.transform.localPosition.x * -1, particles.transform.localPosition.y, particles.transform.localPosition.z);
                    xDirectionMult *= -1;
                    rb.velocity = Vector3.zero;
                }
                if (Randomizer.Determiner(50))
                {
                    yDirForce = 1;
                    if (Randomizer.Determiner(50))
                    {
                        yDirectionMult *= -1;
                        rb.velocity = Vector3.zero;
                    }
                }
                rb.AddForce(new(xDirectionMult * movementSpeed, yDirectionMult * movementSpeed * yDirForce));
                if (particles != null) particles.Play();
            }
            StartCoroutine(MovingCoroutine());
        }
    }
}