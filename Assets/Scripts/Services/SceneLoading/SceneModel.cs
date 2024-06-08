using UnityEngine;
using LightWeightFramework.Model;
using Mario.Utility;
using Utilities.ScriptUtils.EditorSerialization;

namespace Mario.Services.SceneLoading
{
    public interface ISceneModelObserver : IModelObserver
    {
        string GetSceneName(SceneType sceneType);
    }

    [CreateAssetMenu(fileName = "SceneModel", menuName = "Model/SceneModel")]
    public class SceneModel : Model, ISceneModelObserver
    {
        [SerializeField] private DictionaryWrapper<SceneType, SceneReference> _scenesWrapper;

        public string GetSceneName(SceneType sceneType)
        {
            return _scenesWrapper.Dictionary[sceneType].SceneName;
        } 
    }
}