using System;
using LightWeightFramework.Model;

namespace Mario.Components.Health
{
    public interface IHealthModelObserver: IModelObserver
    {
        event Action OnDied;
        event Action OnApplyDamage;
        bool IsDead { get;  }
    }
    
    [Serializable]
    public class HealthModel : InnerModel, IHealthModelObserver
    {
        public string Test;
        public event Action OnDied;
        public event Action OnApplyDamage;
        
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
        }
    }
}