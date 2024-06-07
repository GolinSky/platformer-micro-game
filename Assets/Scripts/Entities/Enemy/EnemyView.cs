using Mario.Components.Health;
using Mario.Entities.Base;
using UnityEngine;

namespace Mario.Entities.Enemy
{
    public class EnemyView: View<IEnemyModelObserver, IEnemyCommand>
    {
        [SerializeField] private HealthViewComponent healthViewComponent;
        [SerializeField] private Collider2D collider;
        protected override void OnInitialize()
        {
            Model.OnFallDown += FallDown;
        }
        
        protected override void OnDispose()
        {
            Model.OnFallDown -= FallDown;
        }
        
        private void FallDown()
        {
            collider.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(healthViewComponent.IsDead) return;
            
            IHealthViewComponent enemyHealth = other.gameObject.GetComponent<IHealthViewComponent>();
            if (enemyHealth != null)
            {
                if(enemyHealth.IsDead) return;
                
                bool isTakingDamage = enemyHealth.Bounds.center.y >= healthViewComponent.Bounds.max.y;
                if (isTakingDamage)
                {
                    healthViewComponent.ApplyDamage(DamageType.ReceiveDamage);
                }
                enemyHealth.ApplyDamage(isTakingDamage ? DamageType.ApplyDamage: DamageType.ReceiveDamage);
            }
        }
    }
}