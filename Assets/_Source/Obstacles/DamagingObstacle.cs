using UnityEngine;

namespace Obstacles
{
    public class DamagingObstacle : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; } = 1;

        public void DestroyObstacle()
        {
            Destroy(gameObject);
        }
    }
}