using LightWeightFramework.Components.Components;
using LightWeightFramework.Model;

namespace Mario.Components.Base
{
    public class Component<TModel> : Component
        where TModel: InnerModel
    {
        protected TModel Model { get; }
        protected IModel RootModel { get; }

        public Component(IModel rootModel)
        {
            RootModel = rootModel;
            Model = rootModel.GetModel<TModel>();
        }
    }
}