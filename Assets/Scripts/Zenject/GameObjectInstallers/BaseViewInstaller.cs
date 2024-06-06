using LightWeightFramework.Components;
using LightWeightFramework.Components.Repository;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using Mario.Zenject.Extensions;
using UnityEngine;
using Zenject;

namespace Mario.Zenject.GameObjectInstallers
{
    public abstract class BaseViewInstaller<TController, TModel> : MonoInstaller
        where TController : Controller<TModel>
        where TModel : Model
    {
        [SerializeField] protected bool bindViewComponents;
        [SerializeField] protected bool bindMonoComponent;

        protected View View { get; set; }
        protected IRepository Repository { get; private set; }

        protected virtual string ModelPath { get; } = typeof(TModel).Name;


        [Inject]
        public void Constructor(IRepository repository)
        {
            Repository = repository;
        }

        public sealed override void InstallBindings()
        {
            BindParameters();
            BindModel();
            BindController();
            BindComponents();
            BindView();
            ResolveView();
            BindMonoComponents();
            BindViewComponents();
        }

        protected virtual void BindViewComponents()
        {
            if (bindViewComponents)
            {
                Container.Install<ViewComponentsInstaller>(new object[] { View });
            }
        }

        protected virtual void BindMonoComponents()
        {
            if (bindMonoComponent)
            {
                Container.Install<MonoComponentInstaller>(new object[] { View.Transform });
            }
        }

        protected virtual void BindController()
        {
            Container.BindInterfaces<TController>();
        }

        protected virtual void BindModel()
        {
            Container.BindModel<TModel>(Repository, ModelPath, OnModelCreated);
        }

        protected virtual void OnModelCreated() {}
        protected virtual void BindParameters() {}
        protected virtual void BindComponents() {}
        protected abstract void BindView();
        protected abstract void ResolveView();
    }
}