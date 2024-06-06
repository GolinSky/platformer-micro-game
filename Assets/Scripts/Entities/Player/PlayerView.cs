using Mario.Entities.Base;

namespace Mario.Entities.Player
{
    public class PlayerView : View<IPlayerModelObserver>
    {
        protected override void OnInitialize()
        {
            transform.position = Model.StartPosition;
        }

        protected override void OnDispose()
        {
        }
    }
}