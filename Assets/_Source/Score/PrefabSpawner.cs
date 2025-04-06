using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Score
{
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField] private PrefabListSO prefabList;

        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            int randomIndex = (int)Randomizer.RandomFloat(0, prefabList.Prefabs.Count);
            GameObject prefab = Instantiate(prefabList.Prefabs[randomIndex], transform.position, Quaternion.identity);
            List<PrefabSpawner> childrenSpawners = prefab.GetComponentsInChildren<PrefabSpawner>().ToList();
            if(childrenSpawners.Count > 0)
            {
                foreach(var child in childrenSpawners)
                {
                    child.Spawn();
                }
            }
            Destroy(gameObject);
        }
    }
}