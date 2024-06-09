using Mario.Components.Health;
using Mario.Services;
using UnityEngine;
using Zenject;

namespace Platformer.Mechanics
{
    
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        [Inject]
        private ICoreGameCommand CoreGameCommand { get; }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            IHealthViewComponent healthViewComponent = collider.gameObject.GetComponent<IHealthViewComponent>();
            if (!healthViewComponent.IsDead && healthViewComponent.IsPlayer)
            {
                CoreGameCommand.WinGame();
            }
        }
    }
}