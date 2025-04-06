using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    public class Treasure : MonoBehaviour
    {
        [field: SerializeField] public int ScorePrize { get; private set; }

        public void DestroyTreasure()
        {
            Destroy(gameObject);
        }
    }
}
