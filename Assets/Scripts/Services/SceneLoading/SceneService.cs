using LightWeightFramework.Components.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Services.SceneLoading
{
    public interface ISceneService : IService
    {
        void LoadScene(SceneType sceneType);
    }
    
    public class SceneService: Service, ISceneService, ILoadingCommand
    {
        private readonly ISceneModelObserver _sceneModelObserver;

        public SceneService(ISceneModelObserver sceneModelObserver)
        {
            _sceneModelObserver = sceneModelObserver;
            Application.backgroundLoadingPriority = ThreadPriority.High;// need to be to load async stuff faster
        }

        public void LoadScene(SceneType sceneType)
        {
            SceneManager.LoadSceneAsync(_sceneModelObserver.GetSceneName(sceneType));
        }

        public void LoadCoreScene()
        {
            LoadScene(SceneType.Core);
        }
    }
}