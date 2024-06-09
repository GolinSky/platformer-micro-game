using Mario.Components.Animator;
using Mario.Entities.Base;
using UnityEngine;

namespace Mario.Entities.Player
{
    public class PlayerView : View<IPlayerModelObserver>
    {
        [SerializeField] protected AnimatorViewComponent animatorViewComponent;
        
        protected override void OnInitialize()
        {
            Model.OnWin += HandleWin;
        }

        protected override void OnDispose()
        {
            Model.OnWin -= HandleWin;
        }
        
        private void HandleWin()
        {
            animatorViewComponent.PlayWinAnimation();
        }
    }
}