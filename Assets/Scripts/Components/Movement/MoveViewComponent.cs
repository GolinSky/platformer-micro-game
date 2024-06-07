using LightWeightFramework.Components.ViewComponents;

namespace Mario.Components.Movement
{
    public class MoveViewComponent : ViewComponent<IMovementModelObserver>
    {
        protected override void OnInit()
        {
            base.OnInit();
            transform.position = Model.StartPosition;
        }
    }
}