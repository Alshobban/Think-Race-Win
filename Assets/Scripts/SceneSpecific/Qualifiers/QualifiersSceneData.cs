using UnityEngine;
using Utilities;

namespace SceneSpecific.Qualifiers
{
    public class QualifiersSceneData : SceneData<QualifiersSceneData>
    {
        [field: SerializeField]
        public QualifiersController QualifiersController { get; private set; }

        [field: SerializeField]
        public QualifiersUiController QualifiersUiController { get; private set; }
    }
}