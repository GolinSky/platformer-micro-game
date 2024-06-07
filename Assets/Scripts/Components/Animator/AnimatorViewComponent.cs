using LightWeightFramework.Components.ViewComponents;
using Mario.Components.Movement;
using UnityEngine;
using Zenject;

namespace Mario.Components.Animator
{
    public class AnimatorViewComponent: ViewComponent<IMovementModelObserver>, ITickable
    {
        private const float FlipLimit = 0.01f;
        private static readonly int Grounded = UnityEngine.Animator.StringToHash("grounded");
        private static readonly int VelocityX = UnityEngine.Animator.StringToHash("velocityX");
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private UnityEngine.Animator animator;
      

        public void Tick()
        {
            if (Model.Direction.x > FlipLimit)
            {
                spriteRenderer.flipX = false;
            }
            else if (Model.Direction.x < -FlipLimit)
            {
                spriteRenderer.flipX = true;
            }

            animator.SetBool(Grounded, Model.IsGrounded);
            animator.SetFloat(VelocityX, Mathf.Abs(Model.Velocity.x) / Model.MaxHorizontalSpeed);
        }
    }
}