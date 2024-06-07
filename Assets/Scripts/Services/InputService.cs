using LightWeightFramework.Components.Service;
using Mario.Utility;
using UnityEngine;

namespace Mario.Services
{
    public interface IInputService: IService
    {
        Vector2 Direction { get; }
        bool HasDirectionInput { get; }
    }
    
    public class InputService : Service, IInputService
    { 
        public Vector2 Direction { get; private set; }
        public bool HasDirectionInput => !Direction.IsEqual(Vector2.zero);

        public void UpdateInputDirection(Vector2 direction)
        {
            Direction = direction;
        }
    }
}