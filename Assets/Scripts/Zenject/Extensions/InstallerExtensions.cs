using System;
using LightWeightFramework.Components.Repository;
using LightWeightFramework.Model;
using UnityEngine;
using Zenject;

namespace Mario.Zenject.Extensions
{
    public static class InstallerExtensions
    {
        public static DiContainer BindInterfaces<TEntity>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<TEntity>()
                .AsSingle();
            return container;
        }

        public static DiContainer BindInterfacesNonLazy<TEntity>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<TEntity>()
                .AsSingle()
                .NonLazy();
            return container;
        }

        public static DiContainer BindComponentModel<TModel>(this DiContainer container, Model rootModel)
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .AsSingle()
                .WithArguments(rootModel);

            return container;
        }

        public static DiContainer BindModel<TModel>(
            this DiContainer container,
            IRepository repository,
            string path = null,
            Action onCreatedAction = null)
            where TModel : Model
        {
            if (path == null)
            {
                path = ConstructName<TModel>();
            }

            container.BindModel<TModel>(repository.Load<TModel>(path), onCreatedAction);
            return container;
        }


        private static void BindModel<TModel>(
            this DiContainer container,
            ScriptableObject scriptableObject,
            Action action)
            where TModel : Model
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .FromNewScriptableObject(scriptableObject)
                .AsSingle()
                .OnInstantiated((InjectContext context, object @object) =>
                {
                    HandleModel<TModel>(context, @object);
                    action?.Invoke();
                });
        }

        private static void HandleModel<TModel>(InjectContext context, object @object)
        {
            Model model = (Model)@object;
            foreach (IModel currentModel in model.CurrentModels)
            {
                context.Container.Inject(currentModel);
            }
        }

        public static DiContainer BindEntity<TEntity>(this DiContainer container, TEntity entity)
        {
            container
                .BindInstance(@entity)
                .AsSingle();
            return container;
        }

        private static string ConstructName<T>()
        {
            return typeof(T).Name;
        }
    }
}