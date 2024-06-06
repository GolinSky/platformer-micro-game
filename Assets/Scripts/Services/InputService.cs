using LightWeightFramework.Components.Service;
using UnityEngine;

namespace Mario.Services
{
    public interface IInputService: IService
    {
        Vector2 Direction { get; }
    }
    
    public class InputService : Service, IInputService
    { 
        public Vector2 Direction { get; private set; }

        public void UpdateInputDirection(Vector2 direction)
        {
            Direction = direction;
        }
    }
}