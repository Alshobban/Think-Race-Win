using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class SceneData<T> : MonoBehaviour where T : SceneData<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var foundInstances = FindObjectsOfType<T>();

                    if (foundInstances.Length > 1)
                    {
                        var instanceNames =
                            foundInstances.Select(t => t.gameObject.name).Aggregate((a, b) => a + ", " + b);
                        Debug.LogError($"Found {foundInstances.Length} instances of {typeof(T).Name}! {instanceNames}");
                    }
                    else if (foundInstances.Length == 0)
                    {
                        throw new Exception("Can't find an instance of SceneData!");
                    }
                    else
                    {
                        _instance = foundInstances.First();
                    }
                }

                return _instance;
            }
        }

        private static T _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }
    }
}