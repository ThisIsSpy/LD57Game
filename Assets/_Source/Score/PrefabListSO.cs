using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    [CreateAssetMenu(fileName = "PrefabListSO", menuName = "SO/New Prefab List SO", order = 0)]
    public class PrefabListSO : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> Prefabs { get; private set; }
    }   
}