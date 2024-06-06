using LightWeightFramework.Components;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Zenject.GameObjectInstallers
{
    public abstract class DynamicViewInstaller<TController, TModel, TView> : BaseViewInstaller<TController, TModel>
        where TController : Controller<TModel>
        where TModel : Model
        where TView : View
    {
        protected virtual Transform ViewTransformParent => transform;
        protected virtual string ViewPath { get; } = typeof(TView).Name;
        
        protected override void ResolveView()
        {
            View = Container.Resolve<TView>();
        }

        protected override void BindView()
        {
            Container
                .BindInterfacesAndSelfTo<TView>()
                .FromComponentInNewPrefab(Repository.Load<GameObject>(ViewPath))
                .UnderTransform(ViewTransformParent)
                .AsSingle();
        }
    }
}