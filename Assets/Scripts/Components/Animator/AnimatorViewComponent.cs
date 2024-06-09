using LightWeightFramework.Components.ViewComponents;
using Mario.Components.Health;
using Mario.Components.Movement;
using UnityEngine;
using Zenject;

namespace Mario.Components.Animator
{
    public class AnimatorViewComponent: ViewComponent<IMovementModelObserver>, ITickable, IInitializable, ILateDisposable
    {
        private const float FlipLimit = 0.01f;
        
        private static readonly int Grounded = UnityEngine.Animator.StringToHash("grounded");
        private static readonly int VelocityX = UnityEngine.Animator.StringToHash("velocityX");
        private static readonly int Hurt = UnityEngine.Animator.StringToHash("hurt");
        private static readonly int Dead = UnityEngine.Animator.StringToHash("dead");
        private static readonly int Victory = UnityEngine.Animator.StringToHash("victory");

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private UnityEngine.Animator animator;

        private IHealthModelObserver healthModelObserver;


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

        public void Initialize()
        {
            healthModelObserver = ModelObserver.GetModelObserver<IHealthModelObserver>();
            healthModelObserver.OnDied += PlayDieAnimation;
            healthModelObserver.OnRespawn += PlayRespawnAnimation;
        }
        
        public void LateDispose()
        {
            healthModelObserver.OnDied -= PlayDieAnimation;
            healthModelObserver.OnRespawn -= PlayRespawnAnimation;
        }
        
        private void PlayRespawnAnimation()
        {
            animator.SetBool(Dead, false);
        }

        private void PlayDieAnimation()
        {
            animator.SetTrigger(Hurt);
            animator.SetBool(Dead, true);
        }

        public void PlayWinAnimation()
        {
            animator.SetTrigger(Victory);
        }
    }
}