using Mario.Components.Health;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            IHealthViewComponent healthViewComponent = collider.gameObject.GetComponent<IHealthViewComponent>();

            if(healthViewComponent == null) return;
            
            if (!healthViewComponent.IsDead)
            {
                healthViewComponent.ApplyDamage(DamageType.ReceiveDamage);
            }
        }
    }
}