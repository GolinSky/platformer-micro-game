using UnityEngine;
using LightWeightFramework.Model;

namespace Mario.Entities.Input
{
    public interface IInputModelObserver : IModelObserver
    {

    }

    [CreateAssetMenu(fileName = "InputModel", menuName = "Model/InputModel")]
    public class InputModel : Model, IInputModelObserver
    {
        
    }
}