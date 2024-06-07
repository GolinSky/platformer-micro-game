using System;
using LightWeightFramework.Command;
using LightWeightFramework.Model;
using Mario.Components.Base;

namespace Mario.Components.Health
{
    public interface IHealthCommand: ICommand
    {
        void ApplyDamage(DamageType damageType);
        void Reborn();
    }
    
    public class HealthComponent : Component<HealthModel>, IHealthCommand
    {
        public HealthComponent(IModel rootModel) : base(rootModel)
        {
        }

        public void ApplyDamage(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.ReceiveDamage:
                {
                    Model.InvokeDieCallback();
                    break;
                }
                case DamageType.ApplyDamage:
                {
                    Model.InvokeApplyingDamage();
                    break;
                }
         
            }
        }

        public void Reborn()
        {
            Model.Reborn();
        }
    }
}