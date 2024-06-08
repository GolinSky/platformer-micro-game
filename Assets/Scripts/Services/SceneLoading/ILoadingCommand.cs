using LightWeightFramework.Command;

namespace Mario.Services.SceneLoading
{
    public interface ILoadingCommand:ICommand
    {
        void LoadCoreScene();
    }
}