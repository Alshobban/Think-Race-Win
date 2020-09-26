using System;
using UnityEngine;

namespace Utilities
{
    public class SceneData<T> : MonoBehaviour
    {
        public static SceneData<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Can't find an instance of SceneData!");
                }

                return _instance;
            }
        }

        private static SceneData<T> _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError($"Found second instance of {typeof(T).Name}!", this);
            }

            _instance = this;
        }
    }
}