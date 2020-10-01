using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public abstract class ScrollListController<T> : MonoBehaviour where T : class
    {
        public event Action<GameObject> LineAdded;

        protected readonly Dictionary<GameObject, T> Objects = new Dictionary<GameObject, T>();

        protected abstract string GetLineText(T sourceObject);

        [SerializeField]
        private GameObject listLinePrefab;

        [SerializeField]
        private Transform contentTransform;

        public void AddLine(T newObject)
        {
            var newPlayerLine = Instantiate(listLinePrefab, Vector3.zero, Quaternion.identity, contentTransform);

            newPlayerLine.GetComponentInChildren<TextMeshProUGUI>()?.SetText(GetLineText(newObject));

            Objects.Add(newPlayerLine, newObject);

            LineAdded?.Invoke(newPlayerLine);
        }

        public void RemoveAll()
        {
            var values = Objects.Values.ToArray();
            foreach (var objectsKey in values)
            {
                RemoveLine(objectsKey);
            }
        }

        public void RemoveLine(T objectToRemove)
        {
            if (Objects.ContainsValue(objectToRemove))
            {
                var key = Objects.Keys.First(t => Objects[t] == objectToRemove);
                Destroy(key);
                Objects.Remove(key);
            }
            else
            {
                Debug.LogError(
                    $"Object {objectToRemove} is not registered in the list!");
            }
        }
    }
}