using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundDecoration : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = new Sprite[2];
        [SerializeField] private float animationSpeed;
        private SpriteRenderer spriteRenderer;
        private int spriteIndex;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[0];
            spriteIndex = 0;
            StartCoroutine(AnimationCoroutine());
        }

        private IEnumerator AnimationCoroutine()
        {
            yield return new WaitForSeconds(animationSpeed);
            spriteIndex++;
            if (spriteIndex >= sprites.Length) spriteIndex = 0;
            spriteRenderer.sprite = sprites[spriteIndex];
            StartCoroutine(AnimationCoroutine());
            
        }
    }
}