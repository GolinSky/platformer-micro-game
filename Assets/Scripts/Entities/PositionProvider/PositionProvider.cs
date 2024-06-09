using UnityEngine;

namespace Mario.Entities.PositionProvider
{
    public class PositionProvider: MonoBehaviour
    {
        [SerializeField] private Transform victoryTransform;
        
        public Vector3 VictoryPoint => victoryTransform.position;
    }
}