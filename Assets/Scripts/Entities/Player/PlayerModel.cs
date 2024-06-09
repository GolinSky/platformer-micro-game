using System;
using LightWeightFramework.Model;
using Mario.Components.Audio;
using Mario.Components.Health;
using Mario.Components.Movement;
using UnityEngine;

namespace Mario.Entities.Player
{
    public interface IPlayerModelObserver : IModelObserver
    {
        event Action OnWin;
        float DistanceFromSpawnPoint { get; }
    }

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/PlayerModel")]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        public event Action OnWin;

        [SerializeField] private MovementModel movementModel;
        [SerializeField] private HealthModel healthModel;
        [SerializeField] private PlayerAudioModel playerAudioModel;
        
        [field:SerializeField]  public float RebornDelay { get; private set; }
        [field: SerializeField] public float BounceForce { get; private set; }
        [field: SerializeField] public float SpeedUpDuration { get; private set; }
        

        protected override void Awake()
        {
            base.Awake();
            AddInnerModels(movementModel, healthModel, playerAudioModel);
        }

        public float DistanceFromSpawnPoint =>
            Vector3.Distance(movementModel.StartPosition, movementModel.CurrentPosition);

        public void InvokeWinEvent()
        {
            OnWin?.Invoke();
        }
    }
    
}