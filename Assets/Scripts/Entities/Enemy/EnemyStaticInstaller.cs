﻿using Mario.Components.Audio;
using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Entities.Player;
using Mario.Zenject.Extensions;
using Mario.Zenject.GameObjectInstallers;
using Platformer.Mechanics;
using UnityEngine;

namespace Mario.Entities.Enemy
{
    public class EnemyStaticInstaller : StaticViewInstaller<EnemyController, EnemyModel>
    {
        [SerializeField] private PatrolPath patrolPath;
        protected override void BindParameters()
        {
            base.BindParameters();
            Container.BindEntity(View.Transform.position); // use view position as start one - instead of dynamic start position data
            Container.BindEntity(EntityType.Enemy);
            if (patrolPath != null)
            {
                Container.BindEntity(patrolPath);
            }
        }

        protected override void BindComponents()
        {
            base.BindComponents();
            Container
                .BindInterfaces<HealthComponent>()
                .BindInterfaces<MovementComponent>()
                .BindInterfaces<EnemyAudioComponent>();

        }
    }
}