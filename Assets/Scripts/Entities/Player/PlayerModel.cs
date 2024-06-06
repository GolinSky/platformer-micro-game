using LightWeightFramework.Model;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Player
{
    public interface IPlayerModelObserver : IModelObserver
    {
        Vector3 StartPosition { get; }
    }

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/PlayerModel")]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        [Inject]
        public Vector3 StartPosition { get; }
    }
}