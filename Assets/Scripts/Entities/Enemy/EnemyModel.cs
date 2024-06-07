using System;
using UnityEngine;
using LightWeightFramework.Model;
using Mario.Components.Health;
using Mario.Components.Movement;

namespace Mario.Entities.Enemy
{
    public interface IEnemyModelObserver : IModelObserver
    {
        event Action OnFallDown;
    }

    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Model/EnemyModel")]
    public class EnemyModel : Model, IEnemyModelObserver
    {
        public event Action OnFallDown;

        [SerializeField] private MovementModel movementModel;
        [SerializeField] private HealthModel healthModel;
        
        protected override void Awake()
        {
            base.Awake();
            AddInnerModels(movementModel, healthModel);
        }

        public void InvokeFallBack()
        {
            OnFallDown?.Invoke();
        }

    }
}