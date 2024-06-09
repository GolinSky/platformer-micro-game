using Mario.Components.Health;
using Mario.Services;
using Platformer.Gameplay;
using UnityEngine;
using Zenject;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        [Inject]
        private ICoreGameCommand CoreGameCommand { get; }
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
            }

            IHealthViewComponent healthViewComponent = collider.gameObject.GetComponent<IHealthViewComponent>();
            if (!healthViewComponent.IsDead && healthViewComponent.IsPlayer)
            {
                CoreGameCommand.WinGame();
            }
        }
    }
}