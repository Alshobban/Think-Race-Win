using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Car
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> carPrefabs;

        [SerializeField]
        private Transform modelParent;

        public void SpawnRandomCar()
        {
            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)], Vector3.zero, Quaternion.identity).transform
                .SetParent(modelParent, false);
        }
    }
}