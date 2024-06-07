using LightWeightFramework.Model;
using Mario.Components.Health;
using Mario.Components.Movement;
using UnityEngine;

namespace Mario.Entities.Player
{
    public interface IPlayerModelObserver : IModelObserver
    {
     
    }

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/PlayerModel")]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        [SerializeField] private MovementModel movementModel;
        [SerializeField] private HealthModel healthModel;
        
        protected override void Awake()
        {
            base.Awake();
            AddInnerModels(movementModel, healthModel);
        }
    }
}