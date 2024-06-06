using UnityEngine;
using LightWeightFramework.Model;

namespace Entities.Player
{
    public interface IPlayerModelObserver : IModelObserver
    {

    }

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/PlayerModel")]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        
    }
}