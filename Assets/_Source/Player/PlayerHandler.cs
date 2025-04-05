using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; set; } = 1f;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Camera mainCamera;
        private SpriteRenderer spriteRenderer;
        private Vector2 mousePosition;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            RotateTowardsMouse();
        }

        private void RotateTowardsMouse()
        {
            mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.z));
            Vector3 rotateDirection = (worldPosition - transform.position).normalized;
            rotateDirection.z = 0;
            float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) spriteRenderer.flipY = true;
            else spriteRenderer.flipY = false;
        }
    }
}
