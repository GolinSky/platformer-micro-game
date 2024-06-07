using LightWeightFramework.Model;
using Mario.Components.Base;

namespace Mario.Components.Movement
{
    public class MovementComponent: Component<MovementModel>
    {
        public MovementComponent(Model rootModel) : base(rootModel)
        {
        }
    }
}