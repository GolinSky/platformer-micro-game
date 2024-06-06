using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Entities.Player
{
    public interface IPlayerModelObserver : IModelObserver
    {

    }

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/PlayerModel")]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        
    }
}