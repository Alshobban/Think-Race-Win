using desExt.Runtime.StaticScriptableObjects;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Network Settings")]
    public class NetworkSettings : StaticScriptableObject<NetworkSettings>
    {
        [field: SerializeField]
        public string ServerRegion { get; private set; }
    }
}