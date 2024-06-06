﻿using UnityEngine;
using Zenject;

namespace Mario.Zenject.GameObjectInstallers
{
    public class MonoComponentInstaller:Installer
    {
        private readonly Transform transform;

        public MonoComponentInstaller(Transform transform)
        {
            this.transform = transform;
        }
        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .WithId(EntityBindType.ViewTransform)
                .FromMethod(() => transform)
                .NonLazy();
        }
    }
}