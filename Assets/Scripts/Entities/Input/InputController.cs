using LightWeightFramework.Command;
using LightWeightFramework.Controller;
using Mario.Services;
using UnityEngine;

namespace Mario.Entities.Input
{
    public interface IInputCommand: ICommand
    {
        void UpdateDirection(Vector2 direction);
    }
    
    public class InputController : Controller<InputModel>, IInputCommand
    {
        private readonly InputService inputService;

        public InputController(InputModel model, InputService inputService) : base(model)
        {
            this.inputService = inputService;
        }

        public void UpdateDirection(Vector2 direction)
        {
            inputService.UpdateInputDirection(direction);
        }
    }
}