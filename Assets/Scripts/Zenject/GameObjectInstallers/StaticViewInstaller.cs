using LightWeightFramework.Components;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Zenject.GameObjectInstallers
{
    public abstract class StaticViewInstaller<TController, TModel> : BaseViewInstaller<TController, TModel>
        where TController : Controller<TModel>
        where TModel : Model
    {
        [SerializeField] private View view;
        
        protected sealed override void ResolveView() {}
        protected override void OnBeforeInjection()
        {
            base.OnBeforeInjection();
            View = view;
        }

        protected override void BindView()
        {
            Container
                .BindInterfacesAndSelfTo(View.GetType())
                .FromInstance(View)
                .AsSingle();
        }
    }
}