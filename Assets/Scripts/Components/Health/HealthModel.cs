using System;
using LightWeightFramework.Model;
using Mario.Entities.Player;
using Zenject;

namespace Mario.Components.Health
{
    public interface IHealthModelObserver: IModelObserver
    {
        event Action OnDied;
        event Action OnApplyDamage;
        event Action OnRespawn;
        
        EntityType EntityType { get; }
        int RespawnAmount { get; }
        bool IsDead { get;  }
    }
    
    [Serializable]
    public class HealthModel : InnerModel, IHealthModelObserver
    {
        public event Action OnDied;
        public event Action OnApplyDamage;
        public event Action OnRespawn;

        [Inject]
        public EntityType EntityType { get; }
        public int RespawnAmount { get; private set; }
        public bool IsDead { get; private set; }

        public void InvokeDieCallback()
        {
            RespawnAmount++;
            IsDead = true;
            OnDied?.Invoke();
        }

        public void InvokeApplyingDamage()
        {
            OnApplyDamage?.Invoke();
        }

        public void Reborn()
        {
            IsDead = false;
            OnRespawn?.Invoke();
        }
    }
}