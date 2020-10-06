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

        public void SpawnCar(int number)
        {
            if (number >= 0 && number < carPrefabs.Count)
            {
                Instantiate(carPrefabs[number], Vector3.zero, Quaternion.identity).transform
                    .SetParent(modelParent, false);
            }
        }

        public void SpawnRandomCar()
        {
            SpawnCar(Random.Range(0, carPrefabs.Count));
        }
    }
}