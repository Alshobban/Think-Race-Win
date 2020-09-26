using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public abstract class ScrollListController<T> : MonoBehaviour
    {
        public event Action<GameObject> LineAdded;

        [SerializeField]
        private GameObject listLinePrefab;

        [SerializeField]
        private Transform contentTransform;

        protected readonly Dictionary<T, GameObject> Objects = new Dictionary<T, GameObject>();

        protected abstract string GetLineText(T sourceObject);

        public void AddLine(T newObject)
        {
            var newPlayerLine = Instantiate(listLinePrefab, Vector3.zero, Quaternion.identity, contentTransform);

            newPlayerLine.GetComponentInChildren<TextMeshProUGUI>()?.SetText(GetLineText(newObject));

            Objects.Add(newObject, newPlayerLine);

            LineAdded?.Invoke(newPlayerLine);
        }

        public void RemoveLine(T objectToRemove)
        {
            if (Objects.ContainsKey(objectToRemove))
            {
                Destroy(Objects[objectToRemove]);
                Objects.Remove(objectToRemove);
            }
            else
            {
                Debug.LogError(
                    $"Object {objectToRemove.ToString()} is not registered in the list!");
            }
        }
    }
}