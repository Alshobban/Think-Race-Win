using Cinemachine;
using UnityEngine;
using Utilities;

namespace SceneSpecific.Game
{
    public class GameSceneData : SceneData<GameSceneData>
    {
        [field: SerializeField]
        public CinemachineVirtualCamera CinemachineVirtualCamera { get; private set; }

        [SerializeField]
        private Transform[] startPositions;

        private int _startPositionIndex;

        public Transform GetVacantStartPosition()
        {
            return startPositions[_startPositionIndex++ % startPositions.Length];
        }
    }
}