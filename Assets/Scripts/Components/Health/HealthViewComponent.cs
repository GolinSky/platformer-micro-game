using LightWeightFramework.Components.ViewComponents;
using Mario.Entities.Player;
using UnityEngine;
using Zenject;

namespace Mario.Components.Health
{
    public interface IHealthViewComponent 
    {
        Bounds Bounds { get; }
        void ApplyDamage(DamageType damageType);
        
        bool IsDead { get; }
        
        bool IsPlayer { get; }
        
    }

    public class HealthViewComponent : ViewComponent<IHealthModelObserver>, IHealthViewComponent
    {
        [SerializeField] private Collider2D collider; 
        
        [Inject]
        private IHealthCommand HealthCommand { get; }
        public Bounds Bounds => collider.bounds;
        
        
        public void ApplyDamage(DamageType damageType)
        {
            HealthCommand.ApplyDamage(damageType);
        }

        public bool IsDead => Model.IsDead;
        public bool IsPlayer => Model.EntityType == EntityType.Player;
    }
}