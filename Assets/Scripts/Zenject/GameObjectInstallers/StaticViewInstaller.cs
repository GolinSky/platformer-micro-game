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

        private void OnValidate()
        {
            view = GetComponent<View>();
        }

        protected sealed override void ResolveView() {}

        protected override void BindView()
        {
            View = view;
            Container
                .BindInterfacesAndSelfTo(View.GetType())
                .FromInstance(View)
                .AsSingle();
        }
    }
}